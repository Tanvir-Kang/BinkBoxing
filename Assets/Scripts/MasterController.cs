using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MasterController : MonoBehaviour
{
    public static MasterController instance = null;
    public XRRig Xrrig;

    public XRRayInteractor rightTeleportInterator;
    public XRRayInteractor leftTeleportInterator;

    private XRInteractorLineVisual rightLineVisual;
    private XRInteractorLineVisual leftLineVisual;

    private InputDevice leftHand;
    private InputDevice rightHand;

    private bool prevRightClicked = false;
    private bool prevLeftClicked = false;

    private XRController rightController;
    private XRController leftController;

    [SerializeField]
    private float movementSpeed = 0.5f;

    float amplitude = 0.5f;
    float duration = 0.25f;

    void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        InputDevices.deviceConnected += RegisterDevices;
    }
    private void OnDisable()
    {
        InputDevices.deviceConnected -= RegisterDevices;
    }
    private void RegisterDevices(InputDevice connectDevice)
    {
        if (connectDevice.isValid)
        {
            if ((connectDevice.characteristics & InputDeviceCharacteristics.HeldInHand) == InputDeviceCharacteristics.HeldInHand)
            {
                if((connectDevice.characteristics & InputDeviceCharacteristics.Left) == InputDeviceCharacteristics.Left)
                {
                    leftHand = connectDevice;
                } 
                else if((connectDevice.characteristics & InputDeviceCharacteristics.Right) == InputDeviceCharacteristics.Right)
                {
                    rightHand = connectDevice;
                }
            }
        }
    }
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        rightLineVisual = rightTeleportInterator.GetComponent<XRInteractorLineVisual>();
        rightLineVisual.enabled = false;

        leftLineVisual = leftTeleportInterator.GetComponent<XRInteractorLineVisual>();
        leftLineVisual.enabled = false;

        rightController = rightTeleportInterator.GetComponent<XRController>();
        leftController = leftTeleportInterator.GetComponent<XRController>();
    }
    private void RightTeleport() {

        Vector2 joystickAxis;
        rightHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystickAxis);
        rightLineVisual.enabled = joystickAxis.y > 0.5;

        prevRightClicked = true;
       if ( joystickAxis.y != 0 && prevRightClicked)
        {
            var xAxis = joystickAxis.x * movementSpeed * Time.deltaTime;

            var yAxis = joystickAxis.y * movementSpeed * Time.deltaTime;


            MovePlayer(yAxis);
           
        }
    }

    private void MovePlayer(float yAxis)
    {
        float vAxis = Camera.main.transform.position.z; // left and right
        float hAxis = Camera.main.transform.position.x; // backward and forward

        Vector3 input = Quaternion.Euler(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z) * new Vector3(hAxis, 0.0f, vAxis);
        transform.position += input * yAxis;
    }

    public void VibrateController()
    {
        rightController.SendHapticImpulse(amplitude, duration);
        leftController.SendHapticImpulse(amplitude, duration);
    }

    void Update()
    {
        
        RightTeleport();
    }
}
