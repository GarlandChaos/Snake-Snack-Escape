using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject triggerGameObject = null;
    [SerializeField] private Animator doorAnimator;

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
        doorAnimator.SetTrigger("Open");
        triggerGameObject.SetActive(true);
    }

    public void CloseDoor()
    {
        doorAnimator.SetTrigger("Close");
    }
}