using System;
using UnityEngine;

namespace Damageables
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private float _maxHealth = 200f;
        
        [SerializeField]
        private ColorByHealth _colorByHealth;

        private float _currentHealth;

        private void Reset()
        {
            _colorByHealth = GetComponent<ColorByHealth>();
        }

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            _colorByHealth.ChangeColor(_currentHealth / _maxHealth);

            if (_currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
