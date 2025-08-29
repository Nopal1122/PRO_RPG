using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealh : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    public TMP_Text healthText;
    public Animator healtTextAnim;


    private void Start()
    {
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + "/" + StatsManager.Instance.maxHealth;
    }
    public void ChangeHealth(int amount)
    {

        StatsManager.Instance.currentHealth += amount;
        healtTextAnim.Play("TextUpdate");
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + "/" + StatsManager.Instance.maxHealth;
        if (StatsManager.Instance.currentHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
    }
}