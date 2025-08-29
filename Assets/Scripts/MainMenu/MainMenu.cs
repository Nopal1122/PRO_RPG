using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [Header("UI Panels")]
    public CanvasGroup optionsCanvas;

    private bool optionsOpen = false;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {
        optionsOpen = !optionsOpen;

        optionsCanvas.alpha = optionsOpen ? 1 : 0;
        optionsCanvas.interactable = optionsOpen;
        optionsCanvas.blocksRaycasts = optionsOpen;

        Debug.Log("Options Panel " + (optionsOpen ? "Opened" : "Closed"));
        
    }

    //Quit Button
    public void QuitGame()
    {
        Application.Quit();
    }
}
