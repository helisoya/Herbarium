using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the languages
/// </summary>
public class Locals
{
    private static Locals self;

    private LocalsData staticData;
    private string currentLanguage;
    private int currentFontIdxPrimary;
    private int currentFontIdxSecondary;
    private int currentSizeIdxPrimary;
    private int currentSizeIdxSecondary;
    private Color currentColorPrimary;
    private Color currentColorSecondary;

    public static string current
    {
        get
        {
            return self.currentLanguage;
        }
    }

    public static int fontIndexPrimary
    {
        get
        {
            return self.currentFontIdxPrimary;
        }
    }

    public static int fontIndexSecondary
    {
        get
        {
            return self.currentFontIdxSecondary;
        }
    }

    public static TMP_FontAsset fontPrimary
    {
        get
        {
            return self.staticData.fonts[self.currentFontIdxPrimary];
        }
    }

    public static TMP_FontAsset fontSecondary
    {
        get
        {
            return self.staticData.fonts[self.currentFontIdxSecondary];
        }
    }

    public static int textSizeIdxPrimary
    {
        get
        {
            return self.currentSizeIdxPrimary;
        }
    }

    public static int textSizeIdxSecondary
    {
        get
        {
            return self.currentSizeIdxSecondary;
        }
    }

    public static int textSizePrimary
    {
        get
        {
            return self.staticData.sizes[self.currentSizeIdxPrimary];
        }
    }

    public static int textSizeSecondary
    {
        get
        {
            return self.staticData.sizes[self.currentSizeIdxSecondary];
        }
    }

    public static Color colorPrimary
    {
        get
        {
            return self.currentColorPrimary;
        }
    }

    public static Color colorSecondary
    {
        get
        {
            return self.currentColorSecondary;
        }
    }


    private Dictionary<string, string> locals;

    private UnityEvent onChangeLocal;
    private UnityEvent<TMP_FontAsset> onChangeFontPrimary;
    private UnityEvent<TMP_FontAsset> onChangeFontSecondary;
    private UnityEvent<int> onChangeSizePrimary;
    private UnityEvent<int> onChangeSizeSecondary;
    private UnityEvent<Color> onChangeColorPrimary;
    private UnityEvent<Color> onChangeColorSecondary;


    /// <summary>
    /// Initiliazes the Locals
    /// </summary>
    public static void Init()
    {
        new Locals();
    }

    public Locals()
    {
        self = this;
        onChangeLocal = new UnityEvent();
        onChangeFontPrimary = new UnityEvent<TMP_FontAsset>();
        onChangeFontSecondary = new UnityEvent<TMP_FontAsset>();
        onChangeSizePrimary = new UnityEvent<int>();
        onChangeSizeSecondary = new UnityEvent<int>();
        onChangeColorPrimary = new UnityEvent<Color>();
        onChangeColorSecondary = new UnityEvent<Color>();

        staticData = Resources.Load<LocalsData>("Data/SO_LocalsData");
        locals = new Dictionary<string, string>();
        currentFontIdxPrimary = 0;
        currentFontIdxSecondary = 1;
        currentSizeIdxPrimary = 0;
        currentSizeIdxSecondary = 0;
        currentColorPrimary = Color.blue;
        currentColorSecondary = Color.red;
        if (staticData.languages.Length > 0) ChangeLanguage(staticData.languages[0]);
    }

    /// <summary>
    /// Registers a localized text
    /// </summary>
    /// <param name="text">The localized text</param>
    public static void RegisterText(LocalizedText text)
    {
        if (Locals.self == null) Init();
        self.onChangeLocal.AddListener(text.ReloadText);
        if (text.isUsingPrimaryFont)
        {
            self.onChangeFontPrimary.AddListener(text.SetFont);
            self.onChangeSizePrimary.AddListener(text.SetSize);
            self.onChangeColorPrimary.AddListener(text.SetColor);

            text.SetFont(fontPrimary);
            text.SetSize(textSizePrimary);
            text.SetColor(colorPrimary);
        }
        else
        {
            self.onChangeFontSecondary.AddListener(text.SetFont);
            self.onChangeSizeSecondary.AddListener(text.SetSize);
            self.onChangeColorSecondary.AddListener(text.SetColor);

            text.SetFont(fontSecondary);
            text.SetSize(textSizeSecondary);
            text.SetColor(colorSecondary);
        } 
    }

    /// <summary>
    /// Unregisters a localized text
    /// </summary>
    /// <param name="text">The localized text</param>
    public static void UnregisterText(LocalizedText text)
    {
        if (Locals.self == null) Init();

        self.onChangeLocal.RemoveListener(text.ReloadText);
        if (text.isUsingPrimaryFont)
        {
            self.onChangeSizePrimary.RemoveListener(text.SetSize);
            self.onChangeFontPrimary.RemoveListener(text.SetFont);
            self.onChangeColorPrimary.RemoveListener(text.SetColor);
        } 
        else{
            self.onChangeSizeSecondary.RemoveListener(text.SetSize);
            self.onChangeFontSecondary.RemoveListener(text.SetFont);
            self.onChangeColorSecondary.RemoveListener(text.SetColor);
        }
    }

    /// <summary>
    /// Changes the current language
    /// </summary>
    /// <param name="newOne">The new language's code</param>
    public static void ChangeLanguage(string newOne)
    {
        if (Locals.self == null) Init();
        if (newOne.Equals(self.currentLanguage)) return;

        self.currentLanguage = newOne;
        self.locals.Clear();
        self.LoadContent(newOne + "_system");
        self.LoadContent(newOne + "_story");
    }

    /// <summary>
    /// Changes the current primary font
    /// </summary>
    /// <param name="fontIndex">The new font</param>
    public static void ChangeFontPrimary(int fontIndex)
    {
        if (Locals.self == null) Init();

        self.currentFontIdxPrimary = fontIndex;
        self.onChangeFontPrimary.Invoke(self.staticData.fonts[fontIndex]);
    }

    /// <summary>
    /// Changes the current secondary font
    /// </summary>
    /// <param name="fontIndex">The new font</param>
    public static void ChangeFontSecondary(int fontIndex)
    {
        if (Locals.self == null) Init();

        self.currentFontIdxSecondary = fontIndex;
        self.onChangeFontSecondary.Invoke(self.staticData.fonts[fontIndex]);
    }

    /// <summary>
    /// Changes the current primary size
    /// </summary>
    /// <param name="sizeIndex">The new size's index</param>
    public static void ChangeSizePrimary(int sizeIndex)
    {
        if (Locals.self == null) Init();

        self.currentSizeIdxPrimary = sizeIndex;
        self.onChangeSizePrimary.Invoke(self.staticData.sizes[sizeIndex]);
    }

    /// <summary>
    /// Changes the current secondary size
    /// </summary>
    /// <param name="sizeIndex">The new size's index</param>
    public static void ChangeSizeSecondary(int sizeIndex)
    {
        if (Locals.self == null) Init();

        self.currentSizeIdxSecondary = sizeIndex;
        self.onChangeSizeSecondary.Invoke(self.staticData.sizes[sizeIndex]);
    }

    /// <summary>
    /// Changes the current primary color
    /// </summary>
    /// <param name="color">The new color</param>
    public static void ChangeColorPrimary(Color color)
    {
        if (Locals.self == null) Init();

        self.currentColorPrimary = color;
        self.onChangeColorPrimary.Invoke(color);
    }

    /// <summary>
    /// Changes the current secondary color
    /// </summary>
    /// <param name="color">The new color</param>
    public static void ChangeColorSecondary(Color color)
    {
        if (Locals.self == null) Init();

        self.currentColorSecondary = color;
        self.onChangeColorSecondary.Invoke(color);
    }

    /// <summary>
    /// Gets a localized string
    /// </summary>
    /// <param name="key">The string's ID</param>
    /// <returns>The localized string</returns>
    public static string GetLocal(string key)
    {
        if (Locals.self == null) Init();
        if (key != null && self.locals.ContainsKey(key)) return self.locals[key];
        return key;
    }

    /// <summary>
    /// Gets a font from the static database
    /// </summary>
    /// <param name="idx">The font's index</param>
    /// <returns>The font</returns>
    public static TMP_FontAsset GetFont(int idx)
    {
        if (Locals.self == null) Init();
        if (idx >= 0 && idx < self.staticData.fonts.Length) return self.staticData.fonts[idx];
        return null;
    }

    /// <summary>
    /// Gets all available fonts
    /// </summary>
    /// <returns>The available fonts</returns>
    public static TMP_FontAsset[] GetFonts()
    {
        if (Locals.self == null) Init();
        return self.staticData.fonts;
    }

    /// <summary>
    /// Gets all available text sizes
    /// </summary>
    /// <returns>The available text sizes</returns>
    public static int[] GetSizes()
    {
        if (Locals.self == null) Init();
        return self.staticData.sizes;
    }

    /// <summary>
    /// Gets a text size
    /// </summary>
    /// <returns>The text size's index</returns>
    public static int GetSize(int index)
    {
        if (Locals.self == null) Init();
        return self.staticData.sizes[index];
    }


    /// <summary>
    /// Gets all available languages
    /// </summary>
    /// <returns>The available languages</returns>
    public static string[] GetLanguages()
    {
        if (Locals.self == null) Init();
        return self.staticData.languages;
    }

    /// <summary>
    /// Loads the content of a file
    /// </summary>
    /// <param name="fileName">The filename</param>
    void LoadContent(string fileName)
    {
        List<string> fileContent = FileManager.ReadTextAsset(Resources.Load<TextAsset>("Locals/" + fileName));
        string line;
        string[] split;

        for (int i = 0; i < fileContent.Count; i++)
        {
            line = fileContent[i];
            if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line)) continue;

            split = line.Split(" = ");

            if (split.Length != 2)
            {
                Debug.LogWarning("Error on line " + line + ". There should be only one = .");
                continue;
            }

            if (split[0].EndsWith(" "))
            {
                split[0] = split[0].Substring(0, split[0].Length - 1);
            }
            if (split[1].EndsWith(" "))
            {
                split[1] = split[1].Substring(0, split[1].Length - 1);
            }
            locals.Add(split[0], split[1]);
        }
    }
}