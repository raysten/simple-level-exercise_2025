using UnityEngine;

namespace Shooting
{
    public abstract class DamageBehaviour : ScriptableObject
    {
        [SerializeField]
        protected int _damage;
        
        public abstract void DealDamage(Vector3 hitPoint);
    }
}
