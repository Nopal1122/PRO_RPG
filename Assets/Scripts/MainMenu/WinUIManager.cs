using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUIManager : MonoBehaviour
{
    public static WinUIManager Instance;
    public CanvasGroup winCanvas;

    private void Awake()
    {
        Instance = this;
        winCanvas.alpha = 0;
        winCanvas.interactable = false;
        winCanvas.blocksRaycasts = false;
    }

    public void ShowWinScreen()
    {
        SoundManager.Instance.PlaySound2D("Win");
        winCanvas.alpha = 1;
        winCanvas.interactable = true;
        winCanvas.blocksRaycasts = true;
        Time.timeScale = 0;
    }

        public void BackToMenu()
    {
        winCanvas.alpha = 0;
        winCanvas.interactable = false;
        winCanvas.blocksRaycasts = false;

        SoundManager.Instance.PlaySound2D("DigitalClick");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.FullReset();
        }
        else
        {
            // Fallback if GameManager is missing
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}

