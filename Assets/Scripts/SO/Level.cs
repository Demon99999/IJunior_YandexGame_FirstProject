using UnityEngine;

[CreateAssetMenu(fileName = "new Level", menuName = "Level", order = 52)]
public class Level : ScriptableObject
{
    [SerializeField] private EnemyCount[] _enemyCounts;
    [SerializeField] private int _goldReward;
    
    public EnemyCount[] EnemyCounts => _enemyCounts;
    public int GoldReward => _goldReward;
}

[System.Serializable]
public class EnemyCount
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _count;

    public Enemy Enemy => _enemy;
    public int Count => _count;
}
