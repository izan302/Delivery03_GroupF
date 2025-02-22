using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LanguageButton : MonoBehaviour, IPointerClickHandler
{
    public Language Language;

    private TextMeshProUGUI _localizedText;

    public void Start()
    {
        _localizedText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        _localizedText.text = Language.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Language += 1;
        if (Language > Language.Spanish) Language = Language.English;

        Localizer.SetLanguage(Language);

        // Make sure button displays current language text
        _localizedText.text = Language.ToString();
    }
}
