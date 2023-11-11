using Lean.Localization;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;
using PlayerPrefs = UnityEngine.PlayerPrefs;

public class Localization : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

    private const string Language = "Language";

    private string _currentLanguage;

    public event UnityAction LanguageChanged;

    private Dictionary<string, string> _language = new Dictionary<string, string>()
    {
        { "Ru", "Russian" },
        { "En", "English" },
        { "Tr", "Turkish" },
    };

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerPrefs.HasKey(Language))
            SetLanguage(Language);
        else
            SetLanguage(YandexGamesSdk.Environment.i18n.lang);
#endif
    }

    public void SetLanguage(string value)
    {
        if (_language.ContainsKey(value))
        {
            _leanLocalization.SetCurrentLanguage(_language[value]);
            _currentLanguage = value;
            LanguageChanged?.Invoke();
            PlayerPrefs.SetString(Language, _currentLanguage);
        }
    }
}
