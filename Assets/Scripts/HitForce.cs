using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitForce : MonoBehaviour
{
    private PlayerController hitPlayer;
    private PlayerController player;
    private PlayerController otherPlayer;
    private float collisionForce;
    public UnityEvent OnHit;

    public float HitForceMagnitude
    {
        get
        {
            return collisionForce;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        hitPlayer = collision.collider.GetComponentInParent<PlayerController>(); //xr rig
        otherPlayer = collision.collider.GetComponent<PlayerController>();

        Debug.Log("hit player " + hitPlayer);
        Debug.Log("bot? " + otherPlayer);
        
        if (hitPlayer && hitPlayer != player)
        {
            if (collision.relativeVelocity.magnitude > 0.0001)
            {
                MasterController.instance.VibrateController();
                collisionForce = collision.relativeVelocity.magnitude/10;
                OnHit?.Invoke();
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        hitPlayer = null;
    }
    public void SendHitForceMagnitude() 
    {
        GameManager.instance.DoDamage((int)collisionForce, hitPlayer);
    }

    void Start()
    {
        OnHit.AddListener(SendHitForceMagnitude);
        player = GetComponentInParent<PlayerController>();
    }
}
