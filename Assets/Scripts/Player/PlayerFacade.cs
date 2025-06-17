using UnityEngine;

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
        private PlayerGrounded _playerGrounded;

        [SerializeField]
        private PlayerInput _playerInput;

        [SerializeField]
        private PlayerVerticalMovement _playerVerticalMovement;
    
        [SerializeField]
        private PlayerSettings _playerSettings;

        public Rigidbody Rigidbody => _rigidbody;

        public CapsuleCollider CapsuleCollider => _capsuleCollider;
    
        public PlayerMovement PlayerMovement => _playerMovement;

        public IGroundedStatus PlayerGrounded => _playerGrounded;

        public PlayerInput PlayerInput => _playerInput;

        public PlayerVerticalMovement PlayerVerticalMovement => _playerVerticalMovement;
    
        public PlayerSettings PlayerSettings => _playerSettings;

        private void Reset()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _playerGrounded = GetComponent<PlayerGrounded>();
            _playerInput = GetComponent<PlayerInput>();
            _playerVerticalMovement = GetComponent<PlayerVerticalMovement>();
            _playerSettings = GetComponent<PlayerSettings>();
        }
    }
}
