using System;
using System.Collections.Generic;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;
using PlayerPrefs = UnityEngine.PlayerPrefs;

namespace YandexSDK
{
    public class Localization : MonoBehaviour
    {
        private const string Language = "Language";

        [SerializeField] private LeanLocalization _leanLocalization;

        private string _currentLanguage;

        private Dictionary<string, string> _language = new Dictionary<string, string>()
        {
            { "Ru", "Russian" },
            { "En", "English" },
            { "Tr", "Turkish" },
        };

        public event Action LanguageChanged;

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
}