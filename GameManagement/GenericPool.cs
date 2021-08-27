using System.Collections.Generic;
using UnityEngine;

public abstract class GenericPool<Type> : MonoBehaviour where Type : Component
{
    /// <summary>
    ///  Type-agnostic monobehavior pooling system. Inherit from this, define the Type, and then overload functions
    /// </summary>
    
   
    [SerializeField]
    private Type spawnPrefab;

    protected Queue<Type> ObjectPool = new Queue<Type>();

    //make static singleton
    public static GenericPool<Type> Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    //return object to pool 
    public void ReturnToPool(Type returnObject)
    {
        returnObject.gameObject.SetActive(false);
        ObjectPool.Enqueue(returnObject);
    }

    //get object from pool queue
    public Type Get()
    {
        if (ObjectPool.Count == 0)
        {
            SpawnToPool(1);
        }
        return ObjectPool.Dequeue();
    }
    //instantiate object into pool
    protected void SpawnToPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var spawnObject = GameObject.Instantiate(spawnPrefab);
            spawnObject.gameObject.SetActive(false);
            ObjectPool.Enqueue(spawnObject);
        }
        
    }
}
