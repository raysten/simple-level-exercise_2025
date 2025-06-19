using System;
using UnityEngine;

namespace Damageables
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private int _maxHealth = 200;
        
        [SerializeField]
        private ColorByHealth _colorByHealth;

        private int _currentHealth;

        private void Reset()
        {
            _colorByHealth = GetComponent<ColorByHealth>();
        }

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            _colorByHealth.ChangeColor((float)_currentHealth / _maxHealth);

            if (_currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
