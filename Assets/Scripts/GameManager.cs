using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    public PlayerController player;
    [SerializeField]
    public PlayerController bot;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
 
    public void DoDamage(int amount, PlayerController player)
    {
        player.TakeDamage(amount);
    }


    public void EndGame() 
    {
      //  Time.timeScale = 0;
    }
}
