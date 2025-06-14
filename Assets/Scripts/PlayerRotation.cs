using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset _input;
    
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Rigidbody _rigidbody;

    private InputAction _lookInputAction;
    private Vector2 _mouseInput;

    private void Awake()
    {
        _lookInputAction = InputSystem.actions.FindAction("Look");
    }

    private void Update()
    {
        _mouseInput = _lookInputAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_mouseInput.x != 0)
        {
            var deltaRotation = Quaternion.Euler(0f, _mouseInput.x, 0f);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        }
    }
}
