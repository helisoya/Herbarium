using System;
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

    public enum Channel
    {
        CHANNEL0,
        CHANNEL1,
        CHANNEL2,
        CHANNEL3,
        CHANNEL4,
        CHANNEL5,
        CHANNEL6,
        CHANNEL7,
        CHANNEL8,
        CHANNEL9
    }

    public static string current
    {
        get
        {
            return self.currentLanguage;
        }
    }


    private Dictionary<string, string> locals;

    private UnityEvent onChangeLocal;

    private LocalChannel[] channels;


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

        Array values = Enum.GetValues(typeof(Locals.Channel));
        channels = new LocalChannel[values.Length];

        for (int i = 0; i < values.Length; i++)
        {
            channels[i] = new LocalChannel();
            channels[i].data = new LocalChannelData()
            {
                fontIndex = 0,
                sizeIndex = 0,
                color = Color.black
            };
            channels[i].onChangeColor = new UnityEvent<Color>();
            channels[i].onChangeFont = new UnityEvent<TMP_FontAsset>();
            channels[i].onChangeSize = new UnityEvent<int>();
        }

        staticData = Resources.Load<LocalsData>("Data/SO_LocalsData");
        locals = new Dictionary<string, string>();
        if (staticData.languages.Length > 0) ChangeLanguage(staticData.languages[0]);
    }

    /// <summary>
    /// Registers a localized text
    /// </summary>
    /// <param name="text">The localized text</param>
    /// <param name="channel">The text's channel</param>
    public static void RegisterText(LocalizedText text, Locals.Channel channel)
    {
        if (Locals.self == null) Init();
        self.onChangeLocal.AddListener(text.ReloadText);

        LocalChannel linkedChannel = self.channels[(int)channel];
        self.channels[(int)channel].onChangeSize.AddListener(text.SetSize);
        self.channels[(int)channel].onChangeFont.AddListener(text.SetFont);
        self.channels[(int)channel].onChangeColor.AddListener(text.SetColor);
        text.SetColor(linkedChannel.data.color);
        text.SetFont(self.staticData.fonts[linkedChannel.data.fontIndex]);
        text.SetSize(self.staticData.sizes[linkedChannel.data.sizeIndex]);
    }

    /// <summary>
    /// Unregisters a localized text
    /// </summary>
    /// <param name="text">The localized text</param>
    /// <param name="channel">The text's channel</param>
    public static void UnregisterText(LocalizedText text, Locals.Channel channel)
    {
        if (Locals.self == null) Init();

        self.onChangeLocal.RemoveListener(text.ReloadText);

        self.channels[(int)channel].onChangeSize.RemoveListener(text.SetSize);
        self.channels[(int)channel].onChangeFont.RemoveListener(text.SetFont);
        self.channels[(int)channel].onChangeColor.RemoveListener(text.SetColor);
    }

    /// <summary>
	/// Gets the font size for a channel
	/// </summary>
	/// <param name="channel">The channel</param>
	/// <returns>Its font size</returns>
    public static int GetFontSize(Locals.Channel channel)
    {
        if (Locals.self == null) Init();
        return self.staticData.sizes[self.channels[(int)channel].data.sizeIndex];
    }

    /// <summary>
	/// Gets the font size index for a channel
	/// </summary>
	/// <param name="channel">The channel</param>
	/// <returns>Its font size index</returns>
    public static int GetFontSizeIndex(Locals.Channel channel)
    {
        if (Locals.self == null) Init();
        return self.channels[(int)channel].data.sizeIndex;
    }

    /// <summary>
	/// Gets the font for a channel
	/// </summary>
	/// <param name="channel">The channel</param>
	/// <returns>Its font</returns>
    public static TMP_FontAsset GetFont(Locals.Channel channel)
    {
        if (Locals.self == null) Init();
        return self.staticData.fonts[self.channels[(int)channel].data.fontIndex];
    }

    /// <summary>
	/// Gets the font index for a channel
	/// </summary>
	/// <param name="channel">The channel</param>
	/// <returns>Its font index</returns>
    public static int GetFontIndex(Locals.Channel channel)
    {
        if (Locals.self == null) Init();
        return self.channels[(int)channel].data.fontIndex;
    }

    /// <summary>
	/// Gets the font color for a channel
	/// </summary>
	/// <param name="channel">The channel</param>
	/// <returns>Its font color</returns>
    public static Color GeColor(Locals.Channel channel)
    {
        if (Locals.self == null) Init();
        return self.channels[(int)channel].data.color;
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
    /// Changes the current font for a channel
    /// </summary>
    /// <param name="channel">The channel</param>
    /// <param name="fontIndex">The new font</param>
    public static void ChangeFont(Locals.Channel channel, int fontIndex)
    {
        if (Locals.self == null) Init();

        self.channels[(int)channel].data.fontIndex = fontIndex;
        self.channels[(int)channel].onChangeFont.Invoke(self.staticData.fonts[fontIndex]);
    }

    /// <summary>
    /// Changes the current font size for a channel
    /// </summary>
    /// <param name="channel">The channel</param>
    /// <param name="sizeIndex">The new size index</param>
    public static void ChangeSize(Locals.Channel channel, int sizeIndex)
    {
        if (Locals.self == null) Init();

        self.channels[(int)channel].data.sizeIndex = sizeIndex;
        self.channels[(int)channel].onChangeSize.Invoke(self.staticData.sizes[sizeIndex]);
    }

    /// <summary>
    /// Changes the current font color for a channel
    /// </summary>
    /// <param name="channel">The channel</param>
    /// <param name="color">The new font color</param>
    public static void ChangeColor(Locals.Channel channel, Color color)
    {
        if (Locals.self == null) Init();

        self.channels[(int)channel].data.color = color;
        self.channels[(int)channel].onChangeColor.Invoke(color);
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