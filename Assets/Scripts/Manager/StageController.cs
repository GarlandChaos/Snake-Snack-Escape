using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public StageData[] stagesData;
    public int currentStageIndex { get; private set; } = 0;
    public int keysCount { get; private set; } = 0;

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

    private void CollectKey()
    {
        keysCount++;
        if(keysCount < stagesData[currentStageIndex].keysRequired)
        {
            InGameUI.Instance.SetKeyCount(new MinMax { min = keysCount, max = stagesData[currentStageIndex].keysRequired });
        }
        else
        {
            InGameUI.Instance.SetDoorOpenText();
        }
    }

    public void NextStage()
    {
        if (currentStageIndex < stagesData.Length - 1)
        {
            currentStageIndex++;
            // Load Next Level
        }
        else
        {
            // End Game Screen
        }
    }
}
