using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour
{
    private const float MIN_VELOCITY = 0.1f;
    
    [SerializeField]
    private float _gravity = -14f;

    [SerializeField]
    private float _jumpForce = 6f;

    private bool _isGravityActive;
    private float _velocity;

    public Vector3 VerticalMovement => Vector3.up * _velocity;
    public bool IsGravityActive => _isGravityActive;
    
    private void FixedUpdate()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (_isGravityActive)
        {
            _velocity = Mathf.Max(_velocity + _gravity * Time.deltaTime);
        }
    }

    public void ActivateGravity()
    {
        _isGravityActive = true;
    }

    public void DeactivateGravity()
    {
        _velocity = -MIN_VELOCITY; // so that CollisionHandler always checks if player is grounded
        _isGravityActive = false;
    }

    public void Jump()
    {
        AddVelocity(_jumpForce);
    }

    public void AddVelocity(float velocity)
    {
        _velocity += velocity;
    }
}