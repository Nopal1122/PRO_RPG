using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverMenu;

    // private void OnEnable()
    // {
    //     PlayerHealh.OnPlayerDeath += EnableGameOverMenu;
    // }

    // private void OnDisable()
    // {
    //     PlayerHealh.OnPlayerDeath -= EnableGameOverMenu;
    // }

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
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
