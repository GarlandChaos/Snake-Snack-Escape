using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    //Singleton
    private static KeyManager instance = null;

    //Object Data
    //private Stack<KeyController> keyControllerStack = new Stack<KeyController>();
    public Action keyCollected = null;

    //[Header("Key Controller Pool")]
    //[SerializeField] private KeyControllerPool keyControllerPool = null;

    public GameObject key;
    public List<GameObject> keys;

    //Properties
    public static KeyManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnKey(Vector3 position)
    {
        //KeyController keyController = keyControllerPool._Pool.Get();
        var keyController = Instantiate(key, transform);
        keyController.transform.position = position;
        //keyControllerStack.Push(keyController);
        keys.Add(keyController);
    }

    public void DestroyKey(GameObject key)
    {
        //if (keyControllerStack.Count == 0)
        //    return;
        //
        //keyControllerPool._Pool.Release(keyControllerStack.Pop());
        //keyCollected?.Invoke();
        keys.Remove(key);
        Destroy(key);
    }
}