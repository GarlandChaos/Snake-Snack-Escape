using UnityEngine;

public class KeyController : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Debug.Log("Collected key");
        KeyManager.Instance.DestroyKey();
    }
}