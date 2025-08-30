using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    [Header("Enemy Type")]
    public bool isBoss;

    public int expReward = 3;

    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;

    public int currentHealth;
    public int maxHealth;


    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void changeHealth(int amount)
    {
        SoundManager.Instance.PlaySound2D("HitEnemy");
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            OnMonsterDefeated(expReward);

            if (isBoss)
            {
                GameState.isGameOver = true;
                StartCoroutine(TriggerWinSequence());
            }

            Destroy(gameObject);
        }
    }
    
    private IEnumerator TriggerWinSequence()
{
    // Optional: Show win UI
        WinUIManager.Instance.ShowWinScreen(); // You’ll need to create this

        yield return null;
}

}
