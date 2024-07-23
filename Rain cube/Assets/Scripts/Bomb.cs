using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 700f;

    private float _fadeDuration;
    private Coroutine _fadeOutAndExplode;

    public event Action<Bomb> OnDestroy;

    public void StartFadeOutAndExplode()
    {
        if(_fadeOutAndExplode != null)
        {
            StopCoroutine(_fadeOutAndExplode);
        }

        _fadeDuration = Random.Range(2f, 5f);
        StartCoroutine(FadeOutAndExplode());
    }

    private IEnumerator FadeOutAndExplode()
    {
        Material materila = _renderer.material;
        Color color = _renderer.material.color;
        float fadeSpeed = 1f / _fadeDuration;

        for (float i = 0; i < 1; i += Time.deltaTime * fadeSpeed)
        {
            color.a = Mathf.Lerp(1, 0, i);
            _renderer.material.color = color;
            yield return null;
        }

        Explode();

        materila.color = color;
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rigidbody = nearbyObject.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        OnDestroy?.Invoke(this);
    }
}

