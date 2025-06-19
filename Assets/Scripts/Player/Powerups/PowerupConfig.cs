using System;
using UnityEngine;

namespace Player.Powerups
{
    [Serializable]
    public class PowerupConfig
    {
        [SerializeField]
        private EPlayerStatistic _statistic;
        
        [SerializeField]
        private float _multiplierValue;
        
        [SerializeField]
        private float _duration;
        
        public EPlayerStatistic Statistic => _statistic;
        public float MultiplierValue => _multiplierValue;
        public float Duration => _duration;
    }
}
