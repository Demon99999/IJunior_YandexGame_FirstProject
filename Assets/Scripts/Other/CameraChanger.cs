using Cinemachine;
using UI;
using UnityEngine;

namespace GameLogic
{
    public class CameraChanger : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera[] _virtualCameras;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private DefeatScreen _defeatScreen;
        [SerializeField] private BattleScreen _battleScreen;

        private int _currentIndex;

        private void OnEnable()
        {
            _victoryScreen.ResumeButtonClick += OnSwitchCamera;
            _victoryScreen.BonusButtonClick += OnSwitchCamera;
            _defeatScreen.RestartButtonClick += OnSwitchCamera;
            _defeatScreen.BonusButtonClick += OnSwitchCamera;
            _battleScreen.PlayButtonClick += OnSwitchCamera;
        }

        private void OnDisable()
        {
            _victoryScreen.ResumeButtonClick -= OnSwitchCamera;
            _victoryScreen.BonusButtonClick -= OnSwitchCamera;
            _defeatScreen.RestartButtonClick -= OnSwitchCamera;
            _defeatScreen.BonusButtonClick -= OnSwitchCamera;
            _battleScreen.PlayButtonClick -= OnSwitchCamera;
        }

        private void OnSwitchCamera()
        {
            _virtualCameras[_currentIndex].gameObject.SetActive(false);
            _currentIndex++;

            if (_currentIndex >= _virtualCameras.Length)
            {
                _currentIndex = 0;
            }

            _virtualCameras[_currentIndex].gameObject.SetActive(true);
        }
    }
}