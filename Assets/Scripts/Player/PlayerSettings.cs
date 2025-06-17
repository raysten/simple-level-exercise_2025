using UnityEngine;

namespace Player
{
    public class PlayerSettings : MonoBehaviour
    {
        [SerializeField]
        private float _horizontalMoveSpeed = 10f;

        [SerializeField]
        private float _horizontalSpeedMultiplierWhenFalling = 0.4f;

        public float HorizontalMoveSpeed => _horizontalMoveSpeed;
    
        public float HorizontalSpeedWhenFalling => _horizontalMoveSpeed * _horizontalSpeedMultiplierWhenFalling;
    }
}