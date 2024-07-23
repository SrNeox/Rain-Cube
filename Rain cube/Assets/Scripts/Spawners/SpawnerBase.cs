using System.Collections;
using TMPro;
using UnityEngine;

public abstract class SpawnerBase<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected PoolObject<T> PoolObject;
    [SerializeField] protected TextMeshProUGUI TotalCountText;
    [SerializeField] protected TextMeshProUGUI ActiveCountText;

    protected int TotalObjects = 0;

    private void Start()
    {
        StartCoroutine(SpawnObject());   
    }

    protected abstract IEnumerator SpawnObject();

    protected virtual void UpdateUI(int totalObject)
    {
        TotalCountText.text = $"Total: {totalObject}";
        ActiveCountText.text = $"Active: {PoolObject.CountObject()}";
    }
}
