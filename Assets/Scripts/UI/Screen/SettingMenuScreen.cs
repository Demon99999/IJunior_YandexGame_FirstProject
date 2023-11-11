using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingMenuScreen : Screen
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _russianButton;
    [SerializeField] private Button _englishButton;
    [SerializeField] private Button _turkishButton;
    [SerializeField] private Localization _localization;

    private const string Ru = "Ru";
    private const string En = "En";
    private const string Tr = "Tr";

    public event UnityAction ExitButtonClick;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButton);
        _russianButton.onClick.AddListener(SetLanguagesRu);
        _englishButton.onClick.AddListener(SetLanguagesEn);
        _turkishButton.onClick.AddListener(SetLanguagesTr);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButton);
        _russianButton.onClick.RemoveListener(SetLanguagesRu);
        _englishButton.onClick.RemoveListener(SetLanguagesEn);
        _turkishButton.onClick.RemoveListener(SetLanguagesTr);
    }

    private void OnExitButton()
    {
        ExitButtonClick?.Invoke();
        CloseScreen();
    }

    private void SetLanguagesRu()
    {
        _localization.SetLanguage(Ru);
    }

    private void SetLanguagesEn()
    {
        _localization.SetLanguage(En);
    }

    private void SetLanguagesTr()
    {
        _localization.SetLanguage(Tr);
    }
}
