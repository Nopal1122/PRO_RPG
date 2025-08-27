using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public StatsUI statsUI;
    public CanvasGroup statsCanvas;

    public bool statsOpen = false;

    private void Start()
    {
        UpdateAllStats();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
            if (statsOpen)
            {
                Time.timeScale = 1;
                UpdateAllStats();
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;
                statsOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                UpdateAllStats();
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;
                statsOpen = true;
            }
    }

    public void ToggleStatsPanel()
    {
        if (statsOpen)
        {
            UpdateAllStats();
            statsCanvas.alpha = 0;
            statsOpen = false;
        }
        else
        {
            UpdateAllStats();
            statsCanvas.alpha = 1;
            statsOpen = true;
        }
    
    statsCanvas.interactable = statsOpen;
    statsCanvas.blocksRaycasts = statsOpen;

}


    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatsManager.Instance.damage;
    }
    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatsManager.Instance.speed;
    }

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
    }
}
