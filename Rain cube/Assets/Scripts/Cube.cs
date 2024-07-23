using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random; 

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private LayerMask _layerGround;
    [SerializeField] private Color _color;

    private int _minLifeTime = 2;
    private int _maxLifeTime = 5;

    public event Action<Cube> IsFelled;
    public bool _touchedGround { get; private set; }

    private void Start()
    {
        SwitchColor(_color);
        _touchedGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_touchedGround == false && collision.gameObject.TryGetComponent(out Ground ground))
        {
            _touchedGround = true;
            SwitchColor(Random.ColorHSV());
            StartCoroutine(InvokeIsFelled());
        }
    }

    private IEnumerator InvokeIsFelled()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime));

        SwitchColor(_color);
        _touchedGround = false;
        IsFelled?.Invoke(this);
    }

    private void SwitchColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}
