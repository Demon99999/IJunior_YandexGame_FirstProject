using System;
using Constants;
using EnemyLogic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class FinishScreen : Screen
    {
        [SerializeField] private Button _switchingButton;
        [SerializeField] private Spawner _spawner;

        public event Action SwitchingButtonClick;

        private void OnEnable()
        {
            _spawner.CurrentLevelExceedsCount += OpenScreen;
            _switchingButton.onClick.AddListener(OnSwitchingButton);
        }

        private void OnDisable()
        {
            _spawner.CurrentLevelExceedsCount -= OpenScreen;
            _switchingButton.onClick.RemoveListener(OnSwitchingButton);
        }

        public void OnSwitchingButton()
        {
            SwitchingButtonClick?.Invoke();
            SceneManager.LoadScene(ScenesNames.Start);
            CloseScreen();
        }
    }
}