using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a color picker
/// </summary>
public class ColorPicker : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject root;
    [SerializeField] private Image previewImage;
    [SerializeField] private Slider hSlider;
    [SerializeField] private Slider sSlider;
    [SerializeField] private Slider vSlider;

    private Action<Color> endCallback;

    /// <summary>
    /// Opens the color picker
    /// </summary>
    /// <param name="startColor">The start color</param>
    /// <param name="callback">The end callback</param>
    public void Open(Color startColor,Action<Color> callback)
    {
        root.SetActive(true);
        endCallback = callback;

        float h,s,v;
        Color.RGBToHSV(startColor,out h, out s, out v);

        hSlider.SetValueWithoutNotify(h);
        sSlider.SetValueWithoutNotify(s);
        vSlider.SetValueWithoutNotify(v);
        RefreshPreview();
    }

    /// <summary>
    /// Refreshs the preview
    /// </summary>
    public void RefreshPreview()
    {
        previewImage.color = Color.HSVToRGB(hSlider.value,sSlider.value,vSlider.value);
    }

    /// <summary>
    /// Applies the color
    /// </summary>
    public void Apply()
    {
        Color endColor = Color.HSVToRGB(hSlider.value,sSlider.value,vSlider.value);
        endColor.a = 1.0f;
        endCallback.Invoke(endColor);
        Close();
    }

    /// <summary>
    /// Closes the picker
    /// </summary>
    public void Close()
    {
        root.SetActive(false);
    }
}
