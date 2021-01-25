using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class BotHealthBar : MonoBehaviour
{
    public TMP_Text healthText;
    public int botHealth;

    private void Start()
    {
        healthText = GetComponentInChildren<TMP_Text>();
    }
    void Update()
    {
        healthText.text = botHealth.ToString();
    }
    public void UpdateHealth(int health)
    {
        botHealth = health;
        healthText.gameObject.SetActive(false);
        healthText.text = botHealth.ToString();
        healthText.gameObject.SetActive(true);
    }
}
