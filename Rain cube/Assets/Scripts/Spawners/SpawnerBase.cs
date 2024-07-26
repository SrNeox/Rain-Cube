using System;
using System.Collections;
using TMPro;
using UnityEngine;

public abstract class SpawnerBase<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected PoolObject<T> PoolObject;

    protected int ActiveObjects = 1;

    public event Action<int, int> QuantityChanged;

    protected abstract IEnumerator SpawnObject();

    public void UpdateScore(int activeObject, int totalObject) => QuantityChanged?.Invoke(activeObject, totalObject);

    protected void ReturnObjectInPool(T obj)
    {
        PoolObject.ReturnObject(obj);
        UpdateScore(ActiveObjects--, PoolObject.CountObject());
    }
}
