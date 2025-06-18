using UnityEngine;

namespace Player
{
    public class PlayerSettings : MonoBehaviour
    {
        [SerializeField]
        private float _horizontalMoveSpeed = 10f;

        [SerializeField]
        private float _horizontalSpeedMultiplierWhenFalling = 0.4f;

        [SerializeField]
        private float _sprintSpeed = 15f;

        [SerializeField]
        private LayerMask _movingPlatformLayerMask;

        public float HorizontalMoveSpeed => _horizontalMoveSpeed;
    
        public float HorizontalSpeedWhenFalling => _horizontalMoveSpeed * _horizontalSpeedMultiplierWhenFalling;
        
        public float SprintSpeed => _sprintSpeed;
        
        public LayerMask MovingPlatformLayerMask => _movingPlatformLayerMask;
    }
}