using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenManager : MonoBehaviour
{
    #region Singleton
    public static UIScreenManager Instance { get; private set; }
    public GameObject menuCanvas;
    public GameObject gameOverCanvas;
    public GameObject victoryCanvas;
    public GameObject endGameCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void HideScreen(GameObject screen)
    {
        screen.SetActive(false);
    }

    public void ShowScreen(GameObject screen)
    {
        screen.SetActive(true);
    }

}
