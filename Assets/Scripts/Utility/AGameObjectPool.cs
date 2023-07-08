using UnityEngine.Pool;
using UnityEngine;

public abstract class AGameObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    //Object data
    IObjectPool<T> pool = null; //Game object pool.

    [Header("Pool Data")]
    [SerializeField]
    int defaultCapacity = 10; //The default capacity the stack will be created with.
    [SerializeField]
    int maxSize = 10; //The maximum size of the pool.
    [SerializeField]
    bool collectionCheck = false; //Collection checks are performed when an instance is returned back to the pool.
                                    //An exception will be thrown if the instance is already in the pool.
                                    //Collection checks are only performed in the Editor.

    [Header("Asset References")]
    [SerializeField]
    T gameObjectPrefab = null;

    //Properties
    public virtual IObjectPool<T> _Pool //Game object pool property
    {
        get
        {
            if (pool == null)
                pool = new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionCheck, defaultCapacity, maxSize); //Stack based

            return pool;
        }
    }

    /// <summary>
    /// Creates a new instance when the pool is empty.
    /// </summary>
    /// <returns>A game object instance.</returns>
    protected virtual T CreatePooledItem()
    {
        var go = Instantiate(gameObjectPrefab, transform, false);

        return go;
    }

    /// <summary>
    /// Called when an item is returned to the pool using Release. This can be used to clean up or disable the instance.
    /// </summary>
    /// <param name="element">The object to be returned to the pool.</param>
    protected virtual void OnReturnedToPool(T element)
    {
        element.gameObject.SetActive(false);
    }

    /// <summary>
    /// Called when the instance is taken from the pool using Get.
    /// </summary>
    /// <param name="element">The object to be taken from the pool.</param>
    protected virtual void OnTakeFromPool(T element)
    {
        element.gameObject.SetActive(true);
    }

    /// <summary>
    /// Called when the element could not be returned to the pool due to the pool reaching the maximum size.
    /// </summary>
    /// <param name="element">The object to be destroyed from the pool.</param>
    protected virtual void OnDestroyPoolObject(T element)
    {
        Destroy(element.gameObject);
    }
}