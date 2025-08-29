using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public CanvasGroup pauseCanvas;
    private bool pauseOpen = false;
    public CanvasGroup statsOpen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        //Check jika GameOver menu terbuka atau tidak
        if (GameState.isGameOver) return;

        if (Input.GetButtonDown("Togglepause") && statsOpen.alpha == 0)
            if (pauseOpen)
            {
                Time.timeScale = 1;
                pauseCanvas.alpha = 0;
                pauseOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                pauseCanvas.alpha = 1;
                pauseOpen = true;
            }
        pauseCanvas.interactable = pauseOpen;
        pauseCanvas.blocksRaycasts = pauseOpen;
    }

    public void TogglePausePanel()
    {
        if (pauseOpen == true)
        {
            Time.timeScale = 1;
            pauseCanvas.alpha = 0;
            pauseOpen = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseCanvas.alpha = 1;
            pauseOpen = true;
        }
        pauseCanvas.interactable = pauseOpen;
        pauseCanvas.blocksRaycasts = pauseOpen;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
