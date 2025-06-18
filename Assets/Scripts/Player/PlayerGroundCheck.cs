using UnityEngine;

namespace Player
{
    public class PlayerGroundCheck : MonoBehaviour
    {
        [SerializeField]
        private CapsuleCollider _capsuleCollider;
        
        [SerializeField]
        private LayerMask _collisionMask;

        [SerializeField]
        private float _offsetIntoCapsule = 0.05f; // to avoid detecting walls

        [SerializeField]
        private float _groundDistance = 0.1f;

        private float _checkRadius;
        
        public bool IsGrounded { get; private set; }

        private void Awake()
        {
            _checkRadius = _capsuleCollider.radius - _offsetIntoCapsule;
        }

        private void FixedUpdate()
        {
            IsGrounded = CheckIsGrounded();
        }

        private bool CheckIsGrounded()
        {
            var origin = transform.position + Vector3.down * (_capsuleCollider.height / 2f - _checkRadius + _groundDistance);
            return Physics.CheckSphere(origin, _checkRadius, _collisionMask, QueryTriggerInteraction.Ignore);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var origin = transform.position + Vector3.down * (_capsuleCollider.height / 2f - _checkRadius + _groundDistance);
            Gizmos.DrawWireSphere(origin, _checkRadius);
        }
    }
}