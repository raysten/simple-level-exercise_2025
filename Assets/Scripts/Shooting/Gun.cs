using UnityEngine;

namespace Shooting
{
    public class Gun : MonoBehaviour
    {
        [SerializeField]
        private Projectile _projectilePrefab;

        [SerializeField]
        private Transform _spawnPoint;
        
        private ProjectilePool _projectilePool;

        private void Awake()
        {
            _projectilePool = new ProjectilePool(_projectilePrefab);
        }

        public void Shoot(float damageMultiplier)
        {
            var projectile = _projectilePool.Pool.Get();
            projectile.Initialize(_spawnPoint.position, _spawnPoint.rotation, damageMultiplier);
        }
    }
}
