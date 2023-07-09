using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{

    [Header("Canvas")]
    [SerializeField] private GameObject VictoryCanvas;
    [SerializeField] private GameObject menuCanvas;

    [Header("Buttons")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnContinueButtonClicked()
    {
        Continue();
    }

    private void OnQuitButtonClicked()
    {
        Quit();
    }

    private void Continue()
    {
        UIScreenManager.Instance.HideScreen(VictoryCanvas);
        StageController.Instance.NextStage();
    }

    private void Quit()
    {
        UIScreenManager.Instance.HideScreen(VictoryCanvas);
        UIScreenManager.Instance.ShowScreen(menuCanvas);
    }

}
