using UnityEngine;

namespace Platforms
{
    public class MovingPlatform : MonoBehaviour, IMovingPlatform
    {
        private const float MIN_DISTANCE_TO_DESTINATION = 0.1f;
        
        [SerializeField]
        private Vector3 _endPosition;

        [SerializeField]
        private float _speed = 3f;

        private Vector3 _startPosition;
        private Vector3 _currentDestination;
        
        public Vector3 Velocity { get; private set; }
        
        private void Awake()
        {
            _startPosition = transform.position;
            _currentDestination = _startPosition;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Velocity = CalculatePlatformVelocity();
            transform.position += Velocity;

            if (Vector3.SqrMagnitude(transform.position - _currentDestination) <= MIN_DISTANCE_TO_DESTINATION)
            {
                ChangeDestination();
            }
        }

        private Vector3 CalculatePlatformVelocity()
        {
            var direction = _currentDestination - transform.position;
            var velocity = direction.normalized * (_speed * Time.fixedDeltaTime);
            
            return velocity;
        }

        private void ChangeDestination()
        {
            _currentDestination = _currentDestination == _endPosition ? _startPosition : _endPosition;
        }
    }
}
