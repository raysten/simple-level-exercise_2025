using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour
{
    private const float GRAVITY = -10f;

    [SerializeField]
    private float _jumpForce = 6f;

    private bool _isGravityActive;
    private float _velocity;

    public bool ShouldApplyVerticalMovement => Mathf.Abs(_velocity) > 0.01f;
    public Vector3 VerticalMovement => Vector3.up * _velocity;
    
    private void FixedUpdate()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (_isGravityActive)
        {
            _velocity += GRAVITY * Time.deltaTime;
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