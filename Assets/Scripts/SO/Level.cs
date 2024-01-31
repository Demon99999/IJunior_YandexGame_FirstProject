using EnemyLogic;
using UnityEngine;

namespace GameLogic
{
    [CreateAssetMenu(fileName = "new Level", menuName = "Level", order = 52)]
    public class Level : ScriptableObject
    {
        [SerializeField] private EnemyCount[] _enemyCounts;
        [SerializeField] private int _goldReward;

        public EnemyCount[] EnemyCounts => _enemyCounts;

        public int GoldReward => _goldReward;
    }
}