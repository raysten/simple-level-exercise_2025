using UnityEngine;

namespace Shooting
{
    // @todo: make it a dynamic rigidbody
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 15f;

        [SerializeField]
        private float _timeToLive = 3f;

        private float _lifetime;
        private ProjectilePool _pool;

        public void AssignPool(ProjectilePool pool)
        {
            _pool = pool;
        }

        public void Initialize(Vector3 position, Quaternion rotation)
        {
            _lifetime = 0f;
            transform.SetPositionAndRotation(position, rotation);
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
                _pool.Pool.Release(this);
            }
        }
    }
}
