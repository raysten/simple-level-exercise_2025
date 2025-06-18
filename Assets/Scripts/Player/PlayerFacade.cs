using UnityEngine;
using Utilities;

namespace Player
{
    public class PlayerFacade : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private CapsuleCollider _capsuleCollider;
    
        [SerializeField]
        private PlayerMovement _playerMovement;

        [SerializeField]
        private PlayerGroundCheck _playerGroundCheck;

        [SerializeField]
        private PlayerInput _playerInput;

        [SerializeField]
        private PlayerVerticalMovement _playerVerticalMovement;
    
        [SerializeField]
        private PlayerSettings _playerSettings;
        
        [SerializeField]
        private DebugDisplay _debugDisplay;

        public Rigidbody Rigidbody => _rigidbody;

        public CapsuleCollider CapsuleCollider => _capsuleCollider;
    
        public PlayerMovement PlayerMovement => _playerMovement;

        public PlayerGroundCheck PlayerGroundCheck => _playerGroundCheck;

        public PlayerInput PlayerInput => _playerInput;

        public PlayerVerticalMovement PlayerVerticalMovement => _playerVerticalMovement;
    
        public PlayerSettings PlayerSettings => _playerSettings;
        
        public DebugDisplay DebugDisplay => _debugDisplay;

        private void Reset()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _playerGroundCheck = GetComponent<PlayerGroundCheck>();
            _playerInput = GetComponent<PlayerInput>();
            _playerVerticalMovement = GetComponent<PlayerVerticalMovement>();
            _playerSettings = GetComponent<PlayerSettings>();
        }
    }
}
