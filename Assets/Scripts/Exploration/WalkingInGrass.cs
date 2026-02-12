using FMODUnity;
using UnityEngine;

public class WalkingInGrass : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private string _propertyName = "_WindAmplitude";
    [SerializeField] private Vector3 _movingAmplitude = new Vector3(10, 0, 0);
    [SerializeField] private Vector3 _stillAmplitude = new Vector3(5, 0, 0);
    [SerializeField] private float _reactionTime = 0.5f;
    [SerializeField] private ParticleSystem _grassVFX;
    [SerializeField] private EventID grassCollision;
    
    //private Renderer _renderer;
    private Material _material;

    private Vector3 _currentValue;
    private Vector3 _targetValue;
    private Vector3 _velocity;
    private bool _isAnimating;

    private void Awake()
    {
        if(_renderer == null)
            _renderer = GetComponent<Renderer>();
        _material = _renderer.material;

        _currentValue = _stillAmplitude;
        _targetValue = _stillAmplitude;

        _material.SetVector(_propertyName, _currentValue);
        _isAnimating = false;
    }

    private void Update()
    {
        if (!_isAnimating)
            return;

        _currentValue = Vector3.SmoothDamp(
            _currentValue,
            _targetValue,
            ref _velocity,
            _reactionTime
        );

        _material.SetVector(_propertyName, _currentValue);
        
        if (Vector3.SqrMagnitude(_currentValue - _targetValue) < 0.0001f)
        {
            _currentValue = _targetValue;
            _material.SetVector(_propertyName, _currentValue);
            _isAnimating = false;
            _velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        _currentValue = _movingAmplitude;
        _material.SetVector(_propertyName, _currentValue);

        _targetValue = _stillAmplitude;
        _isAnimating = true;
        _grassVFX.Play();
        AudioManager.Instance.PlayOneShot3D(grassCollision, gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null && rb.linearVelocity.magnitude > 0.1f)
        {
            if (!_grassVFX.isPlaying)
                _grassVFX.Play();
                AudioManager.Instance.PlayOneShot3D(grassCollision, gameObject);
        }
        else
        {
            if (_grassVFX.isPlaying)
                _grassVFX.Stop();
        }
    }
    
}