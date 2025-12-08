using System;
using System.Collections;
using UnityEngine;

public class WalkingInGrass : MonoBehaviour
{
    [SerializeField] private string _propertyName = "_WindAmplitude";
    [SerializeField] private Vector3 _movingAmplitude = new Vector3(10, 0, 0);
    [SerializeField] private Vector3 _stillAmplitude = new Vector3(5, 0, 0);
    [SerializeField] private float _reactionTime = 0.5f;
    
    private Renderer _renderer;

    private void Start()
    {
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();

        _renderer = GetComponent<Renderer>();
        _renderer.material.SetVector(_propertyName, _stillAmplitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ResetShaderValue(_movingAmplitude, _stillAmplitude));

        }
    }

    private IEnumerator ResetShaderValue(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;

        _renderer.material.SetVector(_propertyName, from);

        while (elapsed < _reactionTime)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.SmoothStep(0f, 1f, elapsed / _reactionTime);
            Vector3 newValue = Vector3.Lerp(from, to, progress);

            _renderer.material.SetVector(_propertyName, newValue);

            yield return null;
        }

        _renderer.material.SetVector(_propertyName, _stillAmplitude);
    }

}