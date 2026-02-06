using TMPro;
using UnityEngine;
using DG.Tweening;

public class TMPDilateAppear : MonoBehaviour
{
    [SerializeField] private float startDilate = -1f;
    [SerializeField] private float endDilate = 0f;
    [SerializeField] private float duration = 0.6f;
    [SerializeField] private Ease ease = Ease.OutCubic;

    private TMP_Text tmp;
    private Material mat;
    private Tween tween;

    private void Awake()
    {
        tmp = GetComponent<TMP_Text>();
        mat = tmp.fontMaterial;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAppear()
    {
        tween?.Kill();

        mat.SetFloat("_FaceDilate", startDilate);

        tween = DOTween.To(
            () => mat.GetFloat("_FaceDilate"),
            x => mat.SetFloat("_FaceDilate", x),
            endDilate,
            duration
        ).SetEase(ease);
    }
}