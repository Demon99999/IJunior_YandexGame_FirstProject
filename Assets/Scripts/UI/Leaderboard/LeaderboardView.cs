using Agava.YandexGames;
using UnityEngine;
using System.Collections.Generic;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private LeaderboardEntryView _playerViewTemplate;
    [SerializeField] private Transform _container;

    private readonly List<LeaderboardEntryView> _leaderboardEntryViews=new List<LeaderboardEntryView>();

    public void Create(LeaderboardEntryResponse entry)
    {
        LeaderboardEntryView view = Instantiate(_playerViewTemplate,_container);
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
