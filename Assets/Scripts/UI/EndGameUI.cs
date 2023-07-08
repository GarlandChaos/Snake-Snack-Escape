using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject endGameCanvas;
    [SerializeField] private GameObject menuCanvas;

    [Header("Buttons")]
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        returnToMenuButton.onClick.AddListener(OnReturnToMenuButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnReturnToMenuButtonClicked()
    {
        ReturnToMenu();
    }

    private void OnQuitButtonClicked()
    {
        Quit();
    }

    private void ReturnToMenu()
    {
        UIScreenManager.Instance.HideScreen(endGameCanvas);
        UIScreenManager.Instance.ShowScreen(menuCanvas);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
