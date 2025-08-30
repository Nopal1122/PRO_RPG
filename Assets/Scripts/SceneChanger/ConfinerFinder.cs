using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfinerFinder : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
                CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();
        confiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("Confiner").GetComponent<PolygonCollider2D>();
    }
}
