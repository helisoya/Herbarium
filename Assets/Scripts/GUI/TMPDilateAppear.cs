using TMPro;
using UnityEngine;
using DG.Tweening;

public class TMPDilateAppear : MonoBehaviour
{
    private float startDilate = -1f;
    private float endDilate = 0f;
    private float duration = 3;
    [SerializeField] private Ease ease = Ease.OutCubic;

    private TMP_Text tmp;
    private Material mat;
    private Tween tween;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
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

        DOVirtual.DelayedCall(0.02f, () => {
            if (tmp == null) return;

            Material matInstance = tmp.fontMaterial;
            tmp.ForceMeshUpdate();

            matInstance.SetFloat(ShaderUtilities.ID_FaceDilate, startDilate);

            tween = DOTween.To(
                () => matInstance.GetFloat(ShaderUtilities.ID_FaceDilate),
                x => matInstance.SetFloat(ShaderUtilities.ID_FaceDilate, x),
                endDilate,
                duration
            ).SetEase(ease).SetTarget(this);
        }).SetTarget(this);
    }
    
    private void OnDestroy()
    {
        tween?.Kill();
    }
}