using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBomb : MonoBehaviour
{
    [SerializeField] private PoolBomb _poolBomb;

    public void Spawn(Transform positionCube)
    {
        Bomb bomb = _poolBomb.GetObject();

        bomb.OnDestroy += ReturnPool;
        bomb.transform.position = positionCube.position;
        bomb.StartFadeOutAndExplode();
    }

    private void ReturnPool(Bomb bomb)
    {
        bomb.OnDestroy -= ReturnPool;
        _poolBomb.ReturnObject(bomb);
    }
}
