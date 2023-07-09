using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject menuCanvas;

    [Header("Buttons")]
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button giveUpButton;

    private void Awake()
    {
        tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        giveUpButton.onClick.AddListener(OnGiveUpButtonClicked);
    }

    private void OnTryAgainButtonClicked()
    {
        TryAgain();
    }

    private void OnGiveUpButtonClicked()
    {
        GiveUp();
    }

    private void TryAgain()
    {
        UIScreenManager.Instance.HideScreen(gameOverCanvas);
        StageController.Instance.SetStage();
    }

    private void GiveUp()
    {
        UIScreenManager.Instance.HideScreen(gameOverCanvas);
        UIScreenManager.Instance.ShowScreen(menuCanvas);
        AudioManager.Instance.PlayMenuMusic();
    }
}
