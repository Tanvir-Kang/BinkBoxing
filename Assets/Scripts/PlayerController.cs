using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int count=0;
    public int health = 100;
    public BotHealthBar healthBar;

   // public void OnCollisionEnter(Collision collision)
    //{
    //    PlayerController strikingPlayer = collision.gameObject.GetComponentInParent<PlayerController>(); //players vr rig
    //    PlayerController hitPlayer = GetComponent<PlayerController>(); // dummy

    //    Debug.Log("Striking player " + strikingPlayer.name + "Speed " + collision.relativeVelocity.magnitude);
    //    Debug.Log("Hit player " + hitPlayer.name);

    //    if (hitPlayer)
    //    {
    //        if (collision.relativeVelocity.magnitude > 0.0001)
    //        {
    //            GameManager.instance.DoDamage((int)collision.relativeVelocity.magnitude, hitPlayer);
    //        }
    //    }
    //}

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health < 0)
        {
            health = 0;
        }
        if (IsDead)
        {
            GameManager.instance.EndGame();
        }
    }
    private bool IsDead
    {
        get
        {
           return health== 0;
        }
    }

    private void Update()
    {
        healthBar.UpdateHealth(health);
    }


}
