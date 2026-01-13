using UnityEngine;

public class WalkingInGrass : MonoBehaviour
{
    [SerializeField] private string _propertyName = "_WindAmplitude";
    [SerializeField] private Vector3 _movingAmplitude = new Vector3(10, 0, 0);
    [SerializeField] private Vector3 _stillAmplitude = new Vector3(5, 0, 0);
    [SerializeField] private float _reactionTime = 0.5f;

    private Renderer _renderer;
    private Material _material;

    private Vector3 _currentValue;
    private Vector3 _targetValue;
    private Vector3 _velocity;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;

        _currentValue = _stillAmplitude;
        _targetValue = _stillAmplitude;

        _material.SetVector(_propertyName, _currentValue);
    }

    private void Update()
    {
        if (_currentValue == _targetValue)
            return;

        _currentValue = Vector3.SmoothDamp(
            _currentValue,
            _targetValue,
            ref _velocity,
            _reactionTime
        );

        _material.SetVector(_propertyName, _currentValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        _currentValue = _movingAmplitude;
        _material.SetVector(_propertyName, _currentValue);

        _targetValue = _stillAmplitude;
    }
}