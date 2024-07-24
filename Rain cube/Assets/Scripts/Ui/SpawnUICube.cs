using UnityEngine;

public class SpawnerUICube : SpawnUI
{
    [SerializeField] private SpawnerCube _spawnerCube;

    private void OnEnable()
    {
        _spawnerCube.QuantityChanged += UpdateUI;
    }

    private void OnDisable()
    {
        _spawnerCube.QuantityChanged -= UpdateUI;
    }
}
