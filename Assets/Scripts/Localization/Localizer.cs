using System;
using System.Collections.Generic;
using UnityEngine;

public class Localizer : MonoBehaviour
{
    public static Localizer Instance; // Singleton instance of Localizer

    public TextAsset DataSheet; // Unity text asset to be assigned (.csv)

    Dictionary<string, LanguageData> Data; // Text data from CSV

    private Language currentLanguage;
    public Language DefaultLanguage;
    
    public static Action OnLanguageChange; // Change language event

    private void Awake()
    {
        Instance = this;
        currentLanguage = DefaultLanguage;

        LoadLanguageSheet();
    }

    public static string GetText(string textKey)
    {
        return Instance.Data[textKey].GetText(Instance.currentLanguage);
    }

    public static void SetLanguage(Language language)
    {
        Instance.currentLanguage = language;

        OnLanguageChange?.Invoke();
    }

    void LoadLanguageSheet()
    {
        string[] lines = DataSheet.text.Split(new char[]{ '\n'});

        for (int i = 1; i < lines.Length; i++)
        {
            if (lines.Length > 1) AddLanguageData(lines[i]);
        }
    }

    void AddLanguageData(string str)
    {
        string[] entry = str.Split(new char[] { ';' });

        var languageData = new LanguageData(entry);

        if (Data == null) Data = new Dictionary<string, LanguageData>();

        Data.Add(entry[0], languageData);
    }
}
