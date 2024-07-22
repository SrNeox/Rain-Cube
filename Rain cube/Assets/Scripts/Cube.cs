using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random; 

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private LayerMask _layerGround;
    [SerializeField] private Color _color;

    public bool _touchedGround { get; private set; }
    public event Action<Cube> _isFell;

    private int _minLifeTime = 2;
    private int _maxLifeTime = 5;

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
            StartCoroutine(InvokeEvent());
        }
    }

    private IEnumerator InvokeEvent()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime));

        SwitchColor(_color);
        _touchedGround = false;
        _isFell?.Invoke(this);
    }

    private void SwitchColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}
