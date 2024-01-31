using UnityEngine;

namespace EnemyLogic
{
    public interface IDamageable
    {
        bool IsAlive(Rigidbody rigidbody, int damage, int force);
    }
}