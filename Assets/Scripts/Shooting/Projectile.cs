using UnityEngine;

namespace Shooting
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 15f;

        [SerializeField]
        private float _timeToLive = 3f;

        private float _lifetime;
        private ProjectilePool _pool;
        
        public float DamageMultiplier { get; private set; }

        public void AssignPool(ProjectilePool pool)
        {
            _pool = pool;
        }

        public void Initialize(Vector3 position, Quaternion rotation, float damageMultiplier)
        {
            _lifetime = 0f;
            transform.SetPositionAndRotation(position, rotation);
            
            DamageMultiplier = damageMultiplier;
        }

        private void FixedUpdate()
        {
            Move();
            TryDespawn();
        }

        private void Move()
        {
            transform.position += transform.forward * (_speed * Time.fixedDeltaTime);
        }

        private void TryDespawn()
        {
            _lifetime += Time.fixedDeltaTime;

            if (_lifetime >= _timeToLive)
            {
                Despawn();
            }
        }

        public void Despawn()
        {
            _pool.Pool.Release(this);
        }
    }
}
