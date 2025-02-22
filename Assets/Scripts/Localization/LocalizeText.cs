using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizeText : MonoBehaviour
{
    public string TextKey;
    private Text _textValue;

    void Start()
    {
        _textValue = GetComponent<Text>();
        _textValue.text = Localizer.GetText(TextKey);
    }

    private void OnEnable()
    {
        Localizer.OnLanguageChange += ChangeLanguage;
    }

    private void OnDisable()
    {
        Localizer.OnLanguageChange -= ChangeLanguage;
    }

    private void ChangeLanguage()
    {
        _textValue.text = Localizer.GetText(TextKey);
    }


}
