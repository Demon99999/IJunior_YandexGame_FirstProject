using System;
using Agava.YandexGames;
using GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoginPanel : Screen
    {
        [SerializeField] private Button _accept;
        [SerializeField] private Button _decline;
        [SerializeField] private SaveSystem _saveSystem;

        public event Action Decline;

        public event Action Accept;

        private void OnEnable()
        {
            _accept.onClick.AddListener(OnAccept);
            _decline.onClick.AddListener(OnDecline);
        }

        private void OnDisable()
        {
            _accept.onClick.RemoveListener(OnAccept);
            _decline.onClick.RemoveListener(OnDecline);
        }

        private void OnAccept()
        {
            if (!PlayerAccount.IsAuthorized)
                PlayerAccount.Authorize();

            Accept?.Invoke();
            CloseScreen();
        }

        private void OnDecline()
        {
            Decline?.Invoke();
            CloseScreen();
        }
    }
}