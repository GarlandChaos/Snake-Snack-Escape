using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public StageData[] stagesData;
    public int currentStageIndex { get; private set; } = 0;
    public int keysCount { get; private set; } = 0;
    public Action allKeysCollected = null;

    public int KeysLeft => stagesData[currentStageIndex].keysRequired - keysCount;

    #region Singleton
    public static StageController Instance { get; private set; }

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

    public void ResetKeyCount()
    {
        keysCount = 0;
    }

    public void CollectKey()
    {
        keysCount++;
        if(keysCount < stagesData[currentStageIndex].keysRequired)
        {
            InGameUI.Instance.SetKeyCount(new MinMax { min = keysCount, max = stagesData[currentStageIndex].keysRequired });
            AudioManager.Instance.PlayKeySFX();
        }
        else
        {
            AudioManager.Instance.PlayDoorOpenSFX();
            InGameUI.Instance.SetDoorOpenText();
            allKeysCollected?.Invoke();
        }
    }

    public void NextStage()
    {
        if (currentStageIndex < stagesData.Length - 1)
        {
            currentStageIndex++;
            SetStage();
        }
    }

    public void SetVictory()
    {
        AudioManager.Instance.PlayVictorySFX();
        if (currentStageIndex < stagesData.Length - 1)
        {
            DisableGameStates();
            UIScreenManager.Instance.ShowScreen(UIScreenManager.Instance.victoryCanvas);
        }
        else
        {
            DisableGameStates();
            UIScreenManager.Instance.ShowScreen(UIScreenManager.Instance.endGameCanvas);
        }
    }

    public void SetGameOver()
    {
        DisableGameStates();
        AudioManager.Instance.PlayDefeatSFX();
        UIScreenManager.Instance.ShowScreen(UIScreenManager.Instance.gameOverCanvas);
    }

    public void StartGame()
    {
        currentStageIndex = 0;
        SetStage();
    }

    public void SetStage()
    {
        ResetKeyCount();
        InGameUI.Instance.SetKeyCount(new MinMax { min = keysCount, max = stagesData[currentStageIndex].keysRequired });
        InGameUI.Instance.SetLevelCount(currentStageIndex);
        LevelController.Instance.SetMap(stagesData[currentStageIndex]);
        EnableGameStates();
        AudioManager.Instance.PlayStageMusic();
    }

    private void EnableGameStates()
    {
        GameState.gameRunning = true;
        GameState.snakeActive = true;
        GameState.ratActive = true;
        GameState.playerActive = true;
    }

    private void DisableGameStates()
    {
        AudioManager.Instance.StopMusic();
        InputController.Instance.ResetVelocity();
        InputController.Instance.StopMoving();
        AudioManager.Instance.StopWalkingSFX();
        GameState.gameRunning = false;
        GameState.snakeActive = false;
        GameState.ratActive = false;
        GameState.playerActive = false;
    }
}
