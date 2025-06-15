using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour
{
    private const float GRAVITY = -9.81f;

    private bool _isGravityActive;
    private float _jumpValue;

    public Vector3 VerticalMovement => _isGravityActive ? Vector3.up * (GRAVITY + _jumpValue) : Vector3.zero;
    public bool IsGravityActive => _isGravityActive;
    
    private void Update()
    {
        if (_jumpValue > 0f)
        {
            _jumpValue -= Mathf.Max(0f, _jumpValue - GRAVITY);
        }
    }

    public void AddJump(float jumpVelocity)
    {
        _jumpValue += jumpVelocity;
    }

    public void ActivateGravity()
    {
        _isGravityActive = true;
    }

    public void DeactivateGravity()
    {
        _isGravityActive = false;
    }
}