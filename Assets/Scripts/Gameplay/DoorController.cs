using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject triggerGameObject = null;

    private void OnEnable()
    {
        StageController.Instance.allKeysCollected += OpenDoor;
    }

    private void OnDisable()
    {
        StageController.Instance.allKeysCollected -= OpenDoor;
    }

    void OpenDoor()
    {
        //Animate the doors opening
        //Enable trigger 
        triggerGameObject.SetActive(true);
    }
}