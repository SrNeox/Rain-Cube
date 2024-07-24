using System.Collections;
using UnityEngine;

public class SpawnerBomb : SpawnerBase<Bomb>
{
    public void Spawn(Transform positionCube)
    {
        Bomb bomb = PoolObject.GetObject();

        bomb.OnDestroy += ReturnBomb;
        bomb.transform.position = positionCube.position;
        bomb.StartFadeOutAndExplode();
        UpdateScore(ActiveObjects++, PoolObject.CountObject());
    }

    protected override IEnumerator SpawnObject()
    {
        yield return null;
    }

    private void ReturnBomb(Bomb bomb)
    {
        bomb.OnDestroy -= ReturnBomb;
        PoolObject.ReturnObject(bomb);
        UpdateScore(ActiveObjects--, PoolObject.CountObject());
    }
}
