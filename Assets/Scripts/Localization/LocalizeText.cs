using TMPro;
using UnityEngine;

public class LocalizeText : MonoBehaviour
{
    public string TextKey;
    private TextMeshProUGUI _textValue;

    void Start()
    {
        _textValue = GetComponent<TextMeshProUGUI>();
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