using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

namespace UI
{
    public class LeaderboardView : MonoBehaviour
    {
        private readonly List<LeaderboardEntryView> _leaderboardEntryViews = new List<LeaderboardEntryView>();

        [SerializeField] private LeaderboardEntryView _playerViewTemplate;
        [SerializeField] private Transform _container;

        public void Create(LeaderboardEntryResponse entry)
        {
            LeaderboardEntryView view = Instantiate(_playerViewTemplate, _container);
            view.SetData(entry);
            _leaderboardEntryViews.Add(view);
        }

        public void Clear()
        {
            foreach (var entry in _leaderboardEntryViews)
                Destroy(entry.gameObject);

            _leaderboardEntryViews.Clear();
        }
    }
}