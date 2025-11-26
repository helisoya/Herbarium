using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Represents a text that is localized
/// </summary>
public class LocalizedText : MonoBehaviour
{
    [SerializeField] protected string localKey;
    [SerializeField] protected bool usePrimaryFont = true;
    [SerializeField] protected TMP_Text text;
    protected object[] injectors;

    void Start()
    {
        Locals.RegisterText(this,isUsingPrimaryFont);

        ReloadText();
    }

    protected void OnDestroy()
    {
        Locals.UnregisterText(this);
    }

    /// <summary>
    /// Changes the ID of the localized text
    /// </summary>
    /// <param name="key">The new ID</param>
    public void SetNewKey(string key)
    {
        localKey = key;
        ReloadText();
    }

    /// <summary>
    /// Reloads the localized text
    /// </summary>
    public virtual void ReloadText()
    {
        string txt = Locals.GetLocal(localKey);
        if(injectors != null && injectors.Length > 0)
        {
            for(int i = 0; i < injectors.Length; i++)
            {
                txt.Replace(string.Concat("[",i,"]"),injectors[i].ToString());
            }
        }
        text.text = txt;
    }
    

    public void SetInjectors(object[] newInjectors, bool reloadText = true)
    {
        injectors = newInjectors;
        if(reloadText) ReloadText();
    }

    /// <summary>
    /// Sets the current font for the text
    /// </summary>
    /// <param name="font">The new font</param>
    public void SetFont(TMP_FontAsset font){
        text.font = font;
    } 

    /// <summary>
    /// Returns the text field
    /// </summary>
    /// <returns>The text field</returns>
    public TMP_Text GetText()
    {
        return text;
    }

    /// <summary>
    /// Changes the text's color
    /// </summary>
    /// <param name="color">The new color</param>
    public void SetColor(Color color)
    {
        text.color = color;
    }

    /// <summary>
    /// Sets the font size
    /// </summary>
    /// <param name="size">The new size</param>
    public void SetSize(int size)
    {
        text.fontSize = size;
        text.fontSizeMax = size;
    }

    public string key { get { return localKey; } }
    public bool isUsingPrimaryFont {get{return usePrimaryFont;}}
}