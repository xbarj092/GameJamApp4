using UnityEngine;
using System.Collections.Generic;

public class PoolHandler : MonoBehaviour
{
    private Dictionary<PoolType, object> _objectPools = new();

    public void CreatePool<T>(PoolType key, T prefab, int initialSize, Transform spawnTransform) where T : Component
    {
        if (!_objectPools.ContainsKey(key))
        {
            ObjectPool<T> pool = new(prefab, initialSize, spawnTransform);
            _objectPools.Add(key, pool);
        }
        else
        {
            Debug.LogWarning($"Pool with key '{key}' already exists.");
        }
    }

    public T GetObject<T>(PoolType key) where T : Component
    {
        if (_objectPools.TryGetValue(key, out object pool))
        {
            return ((ObjectPool<T>)pool).GetObject();
        }

        Debug.LogError($"No pool found with key '{key}'.");
        return null;
    }

    public void ReturnObject<T>(PoolType key, T obj) where T : Component
    {
        if (_objectPools.TryGetValue(key, out object pool))
        {
            ((ObjectPool<T>)pool).ReturnObject(obj);
        }
        else
        {
            Debug.LogError($"No pool found with key '{key}'.");
        }
    }

    public void ReturnObjectWithDelay<T>(PoolType key, T obj, float delay) where T : Component
    {
        if (_objectPools.TryGetValue(key, out object pool))
        {
            ((ObjectPool<T>)pool).ReturnObjectWithDelay(obj, delay);
        }
        else
        {
            Debug.LogError($"No pool found with key '{key}'.");
        }
    }
}
