using UnityEngine;

public class FadeObstacle : MonoBehaviour
{
    [Header("Fade Settings")]
    public float fadedAlpha = 0.2f;
    public float fadeDuration = 0.25f;

    private Renderer _renderer;
    private Material _material;

    private float _originalAlpha;
    private float _currentAlpha;
    private float _targetAlpha;
    private float _fadeVelocity;
    
    private bool _isFading;

    private static readonly int BaseColorID = Shader.PropertyToID("_Color");

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;

        Color c = _material.GetColor(BaseColorID);
        _originalAlpha = c.a;
        _currentAlpha = _originalAlpha;
        _targetAlpha = _originalAlpha;
        _isFading = false;
    }

    private void Update()
    {
        if (!_isFading)
            return;

        _currentAlpha = Mathf.SmoothDamp(
            _currentAlpha,
            _targetAlpha,
            ref _fadeVelocity,
            fadeDuration
        );

        Color c = _material.GetColor(BaseColorID);
        c.a = _currentAlpha;
        _material.SetColor(BaseColorID, c);
        
        if (Mathf.Abs(_currentAlpha - _targetAlpha) < 0.001f)
        {
            _currentAlpha = _targetAlpha;
            c.a = _currentAlpha;
            _material.SetColor(BaseColorID, c);

            _fadeVelocity = 0f;
            _isFading = false;
        }
    }

    public void FadeOut()
    {
        _targetAlpha = _originalAlpha * fadedAlpha;
        _isFading = true;
    }

    public void FadeIn()
    {
        _targetAlpha = _originalAlpha;
        _isFading = true;
    }
}