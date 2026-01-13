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

    private static readonly int BaseColorID = Shader.PropertyToID("_Color");

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;

        Color c = _material.GetColor(BaseColorID);
        _originalAlpha = c.a;
        _currentAlpha = c.a;
        _targetAlpha = c.a;
    }

    private void Update()
    {
        if (Mathf.Approximately(_currentAlpha, _targetAlpha))
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
    }

    public void FadeOut()
    {
        _targetAlpha = _originalAlpha * fadedAlpha;
    }

    public void FadeIn()
    {
        _targetAlpha = _originalAlpha;
    }
}