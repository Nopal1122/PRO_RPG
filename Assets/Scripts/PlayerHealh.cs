using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHealh : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healthText;
    public Animator healtTextAnim;


    private void Start()
    {
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }
    public void ChangeHealth(int amount)
    {

        currentHealth += amount;
        healtTextAnim.Play("TextUpdate");
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}