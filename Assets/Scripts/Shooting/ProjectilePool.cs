using UnityEngine;
using UnityEngine.Pool;

namespace Shooting
{
    public class ProjectilePool
    {
        private const int INITIAL_SIZE = 10;
        private const int MAX_CAPACITY = 50;
        
        private readonly Projectile _projectilePrefab;
        private readonly Transform _poolParent;
        
        public ObjectPool<Projectile> Pool { get;  }

        public ProjectilePool(Projectile projectilePrefab)
        {
            _projectilePrefab = projectilePrefab;
            _poolParent = new GameObject("ProjectilePool").transform;
            
            Pool = new ObjectPool<Projectile>(CreateProjectile, SpawnCallback, DespawnCallback,
                                               DestroyCallback, true, INITIAL_SIZE, MAX_CAPACITY);
        }

        private Projectile CreateProjectile()
        {
            var projectile = Object.Instantiate(_projectilePrefab, _poolParent, true);
            projectile.AssignPool(this);

            return projectile;
        }

        private void SpawnCallback(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }

        private void DespawnCallback(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void DestroyCallback(Projectile projectile)
        {
            Object.Destroy(projectile.gameObject);
        }
    }
}
