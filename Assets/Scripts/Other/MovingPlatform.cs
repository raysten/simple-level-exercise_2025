using UnityEngine;

namespace Other
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _startPosition;

        [SerializeField]
        private Vector3 _endPosition;

        [SerializeField]
        private float _speed;

        private Vector3 _currentDestination;

        private void Awake()
        {
            _currentDestination = _startPosition;
        }

        private void Update()
        {
            var direction = _currentDestination - transform.position;
            transform.position += direction.normalized * (_speed * Time.deltaTime);
        }
    }
}
