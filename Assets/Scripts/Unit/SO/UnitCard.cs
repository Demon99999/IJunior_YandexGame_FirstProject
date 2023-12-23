using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit/Create new Unit", order = 51)]
public class UnitCard : ScriptableObject
{
    [SerializeField] private int _id = 0;
    [SerializeField] private int _variety = 0;
    [SerializeField] private int _grade = 0;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackSpeed = 1;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Unit _template;

    public int Id => _id;
    public int Variety => _variety;
    public Unit Template => _template;
    public int Grade => _grade;
    public float AttackSpeed => _attackSpeed;
    public int Damage => _damage;
    public ParticleSystem ParticleSystem => _particleSystem;
}
