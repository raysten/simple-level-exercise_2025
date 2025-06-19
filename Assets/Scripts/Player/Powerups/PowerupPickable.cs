using System.Collections.Generic;
using UnityEngine;

namespace Player.Powerups
{
    public class PowerupPickable : MonoBehaviour
    {
        [SerializeField]
        private List<PowerupConfig> _powerupConfigs = new();

        public IReadOnlyList<PowerupConfig> PowerupConfigs => _powerupConfigs;

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
