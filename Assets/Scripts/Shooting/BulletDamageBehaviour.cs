using Damageables;
using UnityEngine;

namespace Shooting
{
    [CreateAssetMenu(fileName = nameof(BulletDamageBehaviour),
                     menuName = "Shooting/DamageBehaviours/" + nameof(BulletDamageBehaviour))]
    public class BulletDamageBehaviour : DamageBehaviour
    {
        [SerializeField]
        private float _damageArea = 0.1f;
        
        [SerializeField]
        private LayerMask _affectedLayers;

        private readonly Collider[] _hitBuffer = new Collider[1];

        public override void DealDamage(Vector3 hitPoint)
        {
            var hitCount = Physics.OverlapSphereNonAlloc(hitPoint, _damageArea, _hitBuffer, _affectedLayers);

            if (hitCount > 0)
            {
                var damageable = _hitBuffer[0].GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(_damage);
                }
            }
        }
    }
}
