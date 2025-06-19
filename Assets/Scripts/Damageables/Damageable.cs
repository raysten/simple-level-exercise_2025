using UnityEngine;

namespace Damageables
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        public void TakeDamage(int damage)
        {
            Debug.LogError($"Took {damage} damage");
        }
    }
}
