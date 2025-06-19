using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player.Powerups
{
    public class PowerupsController : MonoBehaviour
    {
        private readonly Dictionary<EPlayerStatistic, List<Powerup>> _powerups = new();

        public void Add(PowerupConfig powerupConfig)
        {
            var statistic = powerupConfig.Statistic;
            _powerups.TryAdd(statistic, new List<Powerup>());
            
            _powerups[statistic].Add(new Powerup(powerupConfig));
        }

        private void Update()
        {
            UpdateOwnedPowerups();
        }

        private void UpdateOwnedPowerups()
        {
            foreach (var powerups in _powerups)
            {
                var powerupsOfType = powerups.Value;
                
                for (var i = powerupsOfType.Count - 1; i >= 0; i--)
                {
                    UpdatePowerup(powerupsOfType, i);
                }
            }
        }

        private static void UpdatePowerup(List<Powerup> powerupsOfType, int i)
        {
            var powerup = powerupsOfType[i];

            if (powerup.UpdateAndCheckIfFinished(Time.deltaTime))
            {
                powerupsOfType.RemoveAt(i);
            }
        }

        public float FindSumOfMultipliersOf(EPlayerStatistic statistic)
        {
            var multiplier = 1f;
            
            if (_powerups.TryGetValue(statistic, out var powerups) && powerups.Count > 0)
            {
                multiplier = powerups.Sum(p => p.MultiplierValue);
            }

            return multiplier;
        }
    }
}
