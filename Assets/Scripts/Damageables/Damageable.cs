using System;
using UnityEngine;

namespace Damageables
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private int _maxHealth = 200;
        
        [SerializeField]
        private ColorByDamage _colorByDamage;

        private int _currentHealth;

        private void Reset()
        {
            _colorByDamage = GetComponent<ColorByDamage>();
        }

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            _colorByDamage.ChangeColor((float)_currentHealth / _maxHealth);

            if (_currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
