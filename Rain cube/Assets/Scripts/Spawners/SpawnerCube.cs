using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCube : SpawnerBase<Cube>
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private SpawnerBomb _spawnerBomb;

    private float _minPosition = -8;
    private float _maxPosition = 8;

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    protected override IEnumerator SpawnObject()
    {
        WaitForSeconds delaySpawn = new(1.5f);

        while (true)
        {
            float randomPosition = Random.Range(_minPosition, _maxPosition);
            Vector3 newPosition = new(randomPosition, transform.position.y, randomPosition);

            Cube cube = PoolObject.GetObject();
            cube.transform.position = _spawnPoint.position + newPosition;
            cube.IsFelled += ReturnCube;
            UpdateScore(ActiveObjects++, PoolObject.CountObject());

            yield return delaySpawn;
        }
    }

    private void ReturnCube(Cube cube)
    {
        cube.IsFelled -= ReturnCube;
        PoolObject.ReturnObject(cube);
        UpdateScore(ActiveObjects--, PoolObject.CountObject());
        _spawnerBomb.Spawn(cube.transform);
    }
}