using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

    private void Awake()
    {
        if (Instance != null)
        {
            CleanUpAndDestory();
            return;
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }
    }

    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void CleanUpAndDestory()
    {
        foreach (GameObject obj in persistentObjects)
        {
            Destroy(obj);
        }
        Destroy(gameObject);
    }

    public void FullReset()
    {
        // Reset runtime data
        ResetGameState();
        //ResetStats
        ResetStats();
        // Destroy all persistent objects
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        // Destroy GameManager itself
        Destroy(gameObject);

        // Optional: Clear static references
        Instance = null;

        // Load Main Menu
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ResetGameState()
    {
        GameState.isGameOver = false;

        Time.timeScale = 1;
    }

    public void ResetStats()
    {
        StatsManager.Instance.maxHealth = 20;
        StatsManager.Instance.ResetHealth();
        StatsManager.Instance.damage = 1;
        StatsManager.Instance.speed = 5;
    }

}
