using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Membuat semua menu tidak bisa dibuka
public static class GameState
{
    public static bool isGameOver = false;
}

public class GameOver : MonoBehaviour
{
    public CanvasGroup gameOverCanvas;


    private void OnEnable()
    {
        Debug.Log("Game Over Scene Active");
        PlayerHealh.OnPlayerDeath += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        GameState.isGameOver = false;
        PlayerHealh.OnPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
        GameState.isGameOver = true;

        gameOverCanvas.alpha = 1f;               // Make it visible
        gameOverCanvas.interactable = true;      // Allow interaction
        gameOverCanvas.blocksRaycasts = true;

        Time.timeScale = 0;

        Debug.Log("Canvas Activated");


    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
