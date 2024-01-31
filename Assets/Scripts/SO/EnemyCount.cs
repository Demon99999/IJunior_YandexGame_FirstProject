using UnityEngine;

namespace EnemyLogic
{
    [System.Serializable]
    public class EnemyCount
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private int _count;

        public Enemy Enemy => _enemy;

        public int Count => _count;
    }
}