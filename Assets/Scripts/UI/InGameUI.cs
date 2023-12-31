using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text keyCountText;
    [SerializeField] private TMP_Text levelCountText;
    private StageController stageControllerInstance;

    #region Singleton
    public static InGameUI Instance { get; private set; }

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

    private void Start()
    {
        stageControllerInstance = StageController.Instance;
        InitilizeKeyCount();
    }

    private void InitilizeKeyCount()
    {
        keyCountText.SetText($"{stageControllerInstance.keysCount} / {stageControllerInstance.stagesData[stageControllerInstance.currentStageIndex].keysRequired}");
    }

    public void SetKeyCount(MinMax keyCount)
    {
        keyCountText.SetText($"{keyCount.min}/{keyCount.max}");
    }

    public void SetLevelCount(int currentStage)
    {
        levelCountText.SetText($"LV {currentStage + 1}");
    }
    public void SetDoorOpenText()
    {
        keyCountText.SetText("ESCAPE!");
    }
}
