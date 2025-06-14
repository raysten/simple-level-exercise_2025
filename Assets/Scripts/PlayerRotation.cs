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

    [SerializeField]
    private float _mouseSensitivity = 20f;
    
    private InputAction _lookInputAction;
    private Vector2 _mouseInput;

    private void Awake()
    {
        _lookInputAction = InputSystem.actions.FindAction("Look");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _mouseInput = _lookInputAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        var deltaRotation = Quaternion.Euler(0f, _mouseInput.x * _mouseSensitivity * Time.fixedDeltaTime, 0f);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
    }
}
