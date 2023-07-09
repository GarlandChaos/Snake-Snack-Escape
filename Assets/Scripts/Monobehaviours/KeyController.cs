using UnityEngine;

public class KeyController : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Collected key");
        StageController.Instance.CollectKey();
        KeyManager.Instance.DestroyKey(gameObject);
    }
}