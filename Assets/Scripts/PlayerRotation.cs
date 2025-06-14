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
        transform.Rotate(Vector3.up, _mouseInput.x * _mouseSensitivity * Time.deltaTime);
    }
}
