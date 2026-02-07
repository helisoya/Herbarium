using TMPro;
using UnityEngine;
using DG.Tweening;

public class TMPDilateAppear : MonoBehaviour
{
    private float startDilate = -1f;
    private float endDilate = 0f;
    private float duration = 100;
    [SerializeField] private Ease ease = Ease.OutCubic;

    private TMP_Text tmp;
    private Material mat;
    private Tween tween;

    private void Awake()
    {
        
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
        tmp = GetComponent<TextMeshProUGUI>();
        Material mat = tmp.material;
        TMP_FontAsset currentFont = tmp.font; 
        Debug.Log("Font actuelle : " + currentFont.name);
        mat.SetFloat("_FaceDilate", startDilate);

        tween = DOTween.To(
            () => mat.GetFloat("_FaceDilate"),
            x => mat.SetFloat("_FaceDilate", x),
            endDilate,
            duration
        ).SetEase(ease);
    }
}