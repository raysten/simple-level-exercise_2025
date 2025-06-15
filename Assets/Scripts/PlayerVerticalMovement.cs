using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour
{
    private const float MIN_VELOCITY = 0.01f;

    [SerializeField]
    private float _gravity = -14f;

    [SerializeField]
    private float _jumpForce = 6f;

    private bool _isGravityActive;
    private float _velocity;

    public bool ShouldApplyVerticalMovement => Mathf.Abs(_velocity) > MIN_VELOCITY;
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
            _velocity += _gravity * Time.deltaTime;
        }
    }

    public void Jump()
    {
        _velocity = _jumpForce;
    }

    public void ActivateGravity()
    {
        _isGravityActive = true;
    }

    public void DeactivateGravity()
    {
        _velocity = 0f;
        _isGravityActive = false;
    }
}