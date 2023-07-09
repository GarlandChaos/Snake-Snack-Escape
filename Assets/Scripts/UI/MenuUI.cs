using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject creditsCanvas;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject inGameCanvas;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button openCreditsButton;
    [SerializeField] private Button closeCreditsButton;
    [SerializeField] private Button soundButton;   

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        openCreditsButton.onClick.AddListener(OnOpenCreditsButtonClicked);
        closeCreditsButton.onClick.AddListener(OnCloseCreditsButtonClicked);
        soundButton.onClick.AddListener(OnSoundButtonClicked);
    }

    private void OnSoundButtonClicked()
    {
        AudioManager.Instance.ToggleMute();
    }

    private void OnOpenCreditsButtonClicked()
    {
        ShowCredits();
    }

    private void OnCloseCreditsButtonClicked()
    {
        HideCredits();
    }

    private void OnPlayButtonClicked()
    {
        StartGame();
        
    }

    private void ShowCredits()
    {
        UIScreenManager.Instance.ShowScreen(creditsCanvas);
    }

    private void HideCredits()
    {
        UIScreenManager.Instance.HideScreen(creditsCanvas);
    }

    private void StartGame()
    {
        UIScreenManager.Instance.HideScreen(menuCanvas);
        UIScreenManager.Instance.ShowScreen(inGameCanvas);
        GameState.gameRunning = true;
        GameState.snakeActive = true;
        GameState.ratActive = true;
        GameState.playerActive = true;
        AudioManager.Instance.PlayStageMusic();
    }
}
