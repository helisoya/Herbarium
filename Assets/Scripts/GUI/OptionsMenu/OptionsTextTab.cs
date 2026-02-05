using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the text tab of the options
/// </summary>
public class OptionsTextTab : OptionsTab
{
    [Header("Language")]
    [SerializeField] private TMP_Dropdown languageDropdown;
    
    [Header("Texts")]
    [SerializeField] private TMP_Dropdown textsFontDropdown;
    [SerializeField] private TMP_Dropdown textsSizeDropdown;
    [SerializeField] private Image textsColorImage;

    [Header("Titles")]
    [SerializeField] private TMP_Dropdown titlesFontDropdown;
    [SerializeField] private TMP_Dropdown titlesSizeDropdown;
    [SerializeField] private Image titlesColorImage;

    [Header("Dialogs")]
    [SerializeField] private TMP_Dropdown dialogsFontDropdown;
    [SerializeField] private TMP_Dropdown dialogsSizeDropdown;
    [SerializeField] private Image dialogsColorImage;
    [SerializeField] private Slider dialogsBackgroundOpacity;


    protected override void OnClose()
    {
    }

    protected override void OnOpen()
    {
        // Language
        int currentLanguage = 0;
        List<string> languagesOptions = new List<string>();
        for(int i = 0; i < Locals.GetLanguages().Length; i++)
        {
            print(Locals.current + " " +Locals.GetLanguages()[i]);
            languagesOptions.Add(Locals.GetLocal(Locals.GetLocal("Language_"+Locals.GetLanguages()[i])));
            if(currentLanguage == 0 && Locals.current.Equals(Locals.GetLanguages()[i])) currentLanguage = i;
        }
        languageDropdown.ClearOptions();
        languageDropdown.AddOptions(languagesOptions);
        languageDropdown.SetValueWithoutNotify(currentLanguage);

        // Generate shared lists
        List<string> fontsOptions = new List<string>();
        foreach(TMP_FontAsset font in Locals.GetFonts())
        {
            fontsOptions.Add(font.name);
        }

        List<string> sizesOptions = new List<string>();
        foreach(int size in Locals.GetSizes())
        {
            sizesOptions.Add(size.ToString());
        }

        // Texts
        textsFontDropdown.ClearOptions();
        textsFontDropdown.AddOptions(fontsOptions);
        textsFontDropdown.SetValueWithoutNotify(Locals.GetFontIndex(Locals.Channel.CHANNEL0));

        textsSizeDropdown.ClearOptions();
        textsSizeDropdown.AddOptions(sizesOptions);
        textsSizeDropdown.SetValueWithoutNotify(Locals.GetFontSizeIndex(Locals.Channel.CHANNEL0));

        textsColorImage.color = Locals.GetColor(Locals.Channel.CHANNEL0);

        // Titles
        titlesFontDropdown.ClearOptions();
        titlesFontDropdown.AddOptions(fontsOptions);
        titlesFontDropdown.SetValueWithoutNotify(Locals.GetFontIndex(Locals.Channel.CHANNEL1));

        titlesSizeDropdown.ClearOptions();
        titlesSizeDropdown.AddOptions(sizesOptions);
        titlesSizeDropdown.SetValueWithoutNotify(Locals.GetFontSizeIndex(Locals.Channel.CHANNEL1));

        titlesColorImage.color = Locals.GetColor(Locals.Channel.CHANNEL1);

        // Dialogs
        dialogsFontDropdown.ClearOptions();
        dialogsFontDropdown.AddOptions(fontsOptions);
        dialogsFontDropdown.SetValueWithoutNotify(Locals.GetFontIndex(Locals.Channel.CHANNEL2));

        dialogsSizeDropdown.ClearOptions();
        dialogsSizeDropdown.AddOptions(sizesOptions);
        dialogsSizeDropdown.SetValueWithoutNotify(Locals.GetFontSizeIndex(Locals.Channel.CHANNEL2));

        dialogsColorImage.color = Locals.GetColor(Locals.Channel.CHANNEL2);
        dialogsBackgroundOpacity.SetValueWithoutNotify(Settings.instance.GetSubtitlesBackgroundOpacity());

    }

    /// <summary>
    /// Callback for changing the language
    /// </summary>
    /// <param name="languageIndex">The language index</param>
    public void ChangeLanguage(int languageIndex)
    {
        parent.InvokeOnClickEvent();
        Settings.instance.ChangeLanguage(Locals.GetLanguages()[languageIndex]);

        List<string> languagesOptions = new List<string>();
        for(int i = 0; i < Locals.GetLanguages().Length; i++)
        {
            languagesOptions.Add(Locals.GetLocal(Locals.GetLocal("Language_"+Locals.GetLanguages()[i])));
        }
        languageDropdown.ClearOptions();
        languageDropdown.AddOptions(languagesOptions);
        languageDropdown.SetValueWithoutNotify(languageIndex);
    }

    /// <summary>
    /// Callback for changing the text's font
    /// </summary>
    /// <param name="fontIndex">The font index</param>
    public void ChangeTextsFont(int fontIndex)
    {
        parent.InvokeOnClickEvent();
        Settings.instance.SetTextFont(Locals.Channel.CHANNEL0,fontIndex);
    }

    /// <summary>
    /// Callback for changing the text's size
    /// </summary>
    /// <param name="fontIndex">The size index</param>
    public void ChangeTextsSize(int sizeIndex)
    {
        parent.InvokeOnClickEvent();
        Settings.instance.SetTextSize(Locals.Channel.CHANNEL0,sizeIndex);
    }

    /// <summary>
    /// Starts changing the text's color
    /// </summary>
    public void StartChangeTextsColor()
    {
        parent.InvokeOnClickEvent();
        parent.GetColorPicker().Open(Locals.GetColor(Locals.Channel.CHANNEL0),ChangeTextsColor);
    }

    /// <summary>
    /// Changes the text's color
    /// </summary>
    /// <param name="color">The new color</param>
    public void ChangeTextsColor(Color color)
    {
        textsColorImage.color = color;
        Settings.instance.SetTextColor(Locals.Channel.CHANNEL0,color);
    }

    /// <summary>
    /// Callback for changing the title's font
    /// </summary>
    /// <param name="fontIndex">The font index</param>
    public void ChangeTitlesFont(int fontIndex)
    {
        parent.InvokeOnClickEvent();
        Settings.instance.SetTextFont(Locals.Channel.CHANNEL1,fontIndex);
    }

    /// <summary>
    /// Callback for changing the title's size
    /// </summary>
    /// <param name="fontIndex">The size index</param>
    public void ChangeTitlesSize(int sizeIndex)
    {
        parent.InvokeOnClickEvent();
        Settings.instance.SetTextSize(Locals.Channel.CHANNEL1,sizeIndex);
    }

    /// <summary>
    /// Starts changing the title's color
    /// </summary>
    public void StartChangeTitlesColor()
    {
        parent.InvokeOnClickEvent();
        parent.GetColorPicker().Open(Locals.GetColor(Locals.Channel.CHANNEL1),ChangeTitlesColor);
    }

    /// <summary>
    /// Changes the titles's color
    /// </summary>
    /// <param name="color">The new color</param>
    public void ChangeTitlesColor(Color color)
    {
        titlesColorImage.color = color;
        Settings.instance.SetTextColor(Locals.Channel.CHANNEL1,color);
    }

    /// <summary>
    /// Callback for changing the dialog's font
    /// </summary>
    /// <param name="fontIndex">The font index</param>
    public void ChangeDialogsFont(int fontIndex)
    {
        parent.InvokeOnClickEvent();
        Settings.instance.SetTextFont(Locals.Channel.CHANNEL2,fontIndex);
    }

    /// <summary>
    /// Callback for changing the dialog's size
    /// </summary>
    /// <param name="fontIndex">The size index</param>
    public void ChangeDialogsSize(int sizeIndex)
    {
        parent.InvokeOnClickEvent();
        Settings.instance.SetTextSize(Locals.Channel.CHANNEL2,sizeIndex);
    }

    /// <summary>
    /// Starts changing the dialog's color
    /// </summary>
    public void StartChangeDialogsColor()
    {
        parent.InvokeOnClickEvent();
        parent.GetColorPicker().Open(Locals.GetColor(Locals.Channel.CHANNEL2),ChangeDialogsColor);
    }

    /// <summary>
    /// Changes the dialog's color
    /// </summary>
    /// <param name="color">The new color</param>
    public void ChangeDialogsColor(Color color)
    {
        dialogsColorImage.color = color;
        Settings.instance.SetTextColor(Locals.Channel.CHANNEL2,color);
    }

    /// <summary>
    /// Callback for changing the dialog's opacity
    /// </summary>
    /// <param name="opacity">The new opacity</param>
    public void ChangeDialogsOpacity(float opacity)
    {
        parent.InvokeOnSliderEvent();
        Settings.instance.SetSubtitlesBackgroundOpacity(opacity);
    }


    /// <summary>
    /// Resets all settings
    /// </summary>
    public void ResetAll()
    {
        parent.InvokeOnClickEvent();
        parent.GetConfirmPopup().Open(CallbackResetAll);
    }

    /// <summary>
    /// Callback for reseting all settings
    /// </summary>
    public void CallbackResetAll()
    {
        Settings.instance.ResetText();
        OnOpen();
    }
}
