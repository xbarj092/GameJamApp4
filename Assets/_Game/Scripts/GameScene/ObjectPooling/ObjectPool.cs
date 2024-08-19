using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private T _prefab;
    private Queue<T> _objects = new();

    private Transform _spawnTransform;

    private static DummyMonoBehaviour _dummyMonoBehaviour;
    private static DummyMonoBehaviour DummyMonoBehaviour
    {
        get
        {
            if (_dummyMonoBehaviour == null)
            {
                GameObject loaderGameObject = new("ObjectPooler Game Object");
                _dummyMonoBehaviour = loaderGameObject.AddComponent<DummyMonoBehaviour>();
            }

            return _dummyMonoBehaviour;
        }
    }

    public ObjectPool(T prefab, int initialSize, Transform spawnTransform)
    {
        _prefab = prefab;
        _spawnTransform = spawnTransform;
        for (int i = 0; i < initialSize; i++)
        {
            T newObject = Object.Instantiate(_prefab, _spawnTransform);
            newObject.gameObject.SetActive(false);
            _objects.Enqueue(newObject);
        }
    }

    public T GetObject()
    {
        if (_objects.Count > 0)
        {
            T pooledObject = _objects.Dequeue();
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }
        else
        {
            T newObject = Object.Instantiate(_prefab, _spawnTransform);
            return newObject;
        }
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _objects.Enqueue(obj);
    }

    public void ReturnObjectWithDelay(T obj, float delay)
    {
        DummyMonoBehaviour.StartCoroutine(ReturnObjectAfterDelay(obj, delay));
    }

    private IEnumerator<WaitForSeconds> ReturnObjectAfterDelay(T obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnObject(obj);
    }
}
