using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float expGrowthMultipler = 1.2f; //Add 20% more EXP to level each time
    public Slider expSlider;
    public TMP_Text currentLevelText;
    public TMP_Text statPointsText;

    //Stat points
    public int statPoints;
    public int statPointsPerLevel = 5;


    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            GainExperience(2);
        }
    }

    private void OnEnable()
    {
        Enemy_Health.OnMonsterDefeated += GainExperience;
    }
    private void OnDisable()
    {
        Enemy_Health.OnMonsterDefeated -= GainExperience;
    }

    public void GainExperience(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }

    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultipler);

        //stats
        statPoints += statPointsPerLevel;
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Level: " + level;

        if (statPointsText != null)
        {
            statPointsText.text = "Available Points: " + statPoints;
        }
    }
    
}
