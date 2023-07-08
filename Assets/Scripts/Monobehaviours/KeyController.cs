using UnityEngine;

public class KeyController : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Collected key");
        KeyManager.Instance.DestroyKey();
    }
}