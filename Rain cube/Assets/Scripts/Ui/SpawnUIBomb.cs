using UnityEngine;

public class SpawnUIBomb : SpawnUI
{
    [SerializeField] private SpawnerBomb _spawnerBomb;

    private void OnEnable()
    {
        _spawnerBomb.QuantityChanged += UpdateUI;
    }

    private void OnDisable()
    {
        _spawnerBomb.QuantityChanged -= UpdateUI;
    }
}
