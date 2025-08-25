using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public CanvasGroup pauseCanvas;
    private bool pauseOpen = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Togglepause"))
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
}
