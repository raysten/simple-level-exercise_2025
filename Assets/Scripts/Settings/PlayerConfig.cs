using UnityEngine;
using UnityEngine.InputSystem;

namespace Settings
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Settings/" + nameof(PlayerConfig))]
    public class PlayerConfig : ScriptableObject, IMovementLayers, IPowerupLayerMask, IVerticalMovementConfig,
                                IInputAsset, IHorizontalSpeedConfig, IRotationSettings, IGroundCheckConfig
    {
        [SerializeField]
        private InputActionAsset _input;

        [SerializeField]
        private LayerMask _powerupLayerMask;
        
        [Header("Movement"), Space]
        [SerializeField]
        private LayerMask _playerMovementCollisionMask;
        
        [SerializeField]
        private LayerMask _movingPlatformLayerMask;
        
        [Header("VerticalMovement"), Space]
        [SerializeField]
        private float _gravity = -14f;

        [SerializeField]
        private float _jumpForce = 7f;
        
        [Header("HorizontalMovement"), Space]
        [SerializeField]
        private float _horizontalMovementSpeed = 10f;

        [SerializeField]
        private float _sprintSpeed = 15f;

        [SerializeField]
        private float _horizontalSpeedMultiplierWhenFalling = 0.4f;
        
        [SerializeField]
        private float _mouseSensitivity = 30f;

        [SerializeField]
        private float _yAxisRotationClamp = 15f;
        
        [Header("Ground check"), Space]
        [SerializeField]
        private LayerMask _groundCheckLayerMask;

        [SerializeField]
        private float _groundCheckOffsetIntoCapsule = 0.05f; // to avoid detecting walls

        [SerializeField]
        private float _groundDistance = 0.05f;

        public InputActionAsset InputAsset => _input;
        
        public LayerMask PowerupLayerMask => _powerupLayerMask;
        
        public LayerMask PlayerMovementCollisionMask => _playerMovementCollisionMask;
        public LayerMask MovingPlatformLayerMask => _movingPlatformLayerMask;
        
        public float Gravity => _gravity;
        public float JumpForce => _jumpForce;
        
        public float HorizontalMovementSpeed => _horizontalMovementSpeed;
        public float SprintSpeed => _sprintSpeed;
        public float HorizontalSpeedWhenFalling => _horizontalMovementSpeed * _horizontalSpeedMultiplierWhenFalling;
        public float MouseSensitivity => _mouseSensitivity;
        public float YAxisRotationClamp => _yAxisRotationClamp;
        
        public LayerMask GroundCheckLayerMask => _groundCheckLayerMask;
        public float GroundCheckOffsetIntoCapsule => _groundCheckOffsetIntoCapsule;
        public float GroundDistance => _groundDistance;
    }
}
