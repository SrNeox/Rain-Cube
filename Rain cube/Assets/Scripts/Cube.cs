using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private LayerMask _layerGround;
    [SerializeField] private Color _color;

    private Renderer _renderer;
    private int _minLifeTime = 2;
    private int _maxLifeTime = 5;

    public event Action<Cube> IsFelled;
    public bool TouchedGround { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        SwitchColor(_color);
        TouchedGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (TouchedGround == false && collision.gameObject.TryGetComponent(out Ground ground))
        {
            TouchedGround = true;
            SwitchColor(Random.ColorHSV());
            StartCoroutine(InvokeIsFelled());
        }
    }

    private IEnumerator InvokeIsFelled()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime));

        SwitchColor(_color);
        TouchedGround = false;
        IsFelled?.Invoke(this);
    }

    private void SwitchColor(Color color)
    {
        _renderer.material.color = color;
    }
}
