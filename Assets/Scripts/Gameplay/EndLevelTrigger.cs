using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        StageController.Instance.SetVictory();
    }
}