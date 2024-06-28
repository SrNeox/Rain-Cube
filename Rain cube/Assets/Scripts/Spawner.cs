using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PoolCube _poolCube;

    private WaitForSeconds delay = new(1);

    private float _minPosition = -8;
    private float _maxPosition = 8;

    private void Start()
    {
        StartCoroutine(SpawnCube());
    }

    private IEnumerator SpawnCube()
    {
        WaitForSeconds delaySpawn = new(1.5f);

        while (true)
        {
            float randomPosition = Random.Range(_minPosition, _maxPosition);
            Vector3 newPosition = new(randomPosition, transform.position.y, randomPosition);

            Cube cube = _poolCube.GetObject();
            cube.transform.position = _spawnPoint.position + newPosition;
            cube._isFell += ReturnCube;

            yield return delaySpawn;
        }
    }

    private void ReturnCube(Cube cube)
    {
        cube._isFell -= ReturnCube;
        _poolCube.ReturnObject(cube);
    }
}
