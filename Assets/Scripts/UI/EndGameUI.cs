using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject endGameCanvas;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject creditsCanvas;

    [Header("Buttons")]
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button creditsButton;

    private void Awake()
    {
        returnToMenuButton.onClick.AddListener(OnReturnToMenuButtonClicked);
        creditsButton.onClick.AddListener(OnCreditsButtonClicked);
    }

    private void OnReturnToMenuButtonClicked()
    {
        ReturnToMenu();
    }

    private void OnCreditsButtonClicked()
    {
        ShowCredits();
    }

    private void ReturnToMenu()
    {
        UIScreenManager.Instance.HideScreen(endGameCanvas);
        UIScreenManager.Instance.ShowScreen(menuCanvas);
    }

    private void ShowCredits()
    {
        UIScreenManager.Instance.ShowScreen(creditsCanvas);
    }
}
