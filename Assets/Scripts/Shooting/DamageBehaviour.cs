using UnityEngine;

namespace Shooting
{
    public abstract class DamageBehaviour : ScriptableObject
    {
        [SerializeField]
        protected int _damage = 25;

        public abstract void DealDamage(RaycastHit hit);
    }
}
