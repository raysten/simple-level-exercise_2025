using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private const string PLAYER_MAP_ID = "Player";

    [SerializeField]
    private InputActionAsset _input;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _movementSpeed = 10f;

    private InputAction _moveInputAction;
    private Vector2 _movementInput;
    private Vector3 _collisionMovement;
    private Vector3 _collisionNormal;
    private Collision _currentCollision;
    private List<Vector3> _normals = new();

    private void OnEnable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Enable();
    }

    private void Awake()
    {
        _moveInputAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        _movementInput = _moveInputAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        var forwardDelta = _movementInput.y;
        var rightDelta = _movementInput.x;
        
        var movementDelta = (transform.forward * forwardDelta + transform.right * rightDelta).normalized;
        
        // Debug.DrawRay(transform.position, movementDelta, Color.blue, 1f);

        // if (_movementInput.sqrMagnitude > 0f && _collisionNormal != Vector3.zero)
        // {
        //     var isMovingIntoWall = Vector3.Dot(movementDelta, _collisionNormal) < 0f;
        //
        //     if (isMovingIntoWall)
        //     {
        //         movementDelta = Vector3.ProjectOnPlane(movementDelta, _collisionNormal).normalized;
        //     }
        // }

        if (_movementInput.sqrMagnitude > 0f && _normals.Any())
        {
            // Debug.LogError($"collision contacts: {_currentCollision.contactCount}");
            foreach (var normal in _normals)
            {
                if (normal != Vector3.zero)
                {
                    var isMovingIntoWall = Vector3.Dot(movementDelta, normal) < 0f;
        
                    if (isMovingIntoWall)
                    {
                        movementDelta = Vector3.ProjectOnPlane(movementDelta, normal).normalized;
                        // break;
                    }
                }
            }
        }
        
        // Debug.DrawRay(transform.position, movementDelta, Color.black, 1f);

        _rigidbody.MovePosition(_rigidbody.position + movementDelta * (_movementSpeed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collisionMovement = Vector3.zero;
        _collisionNormal = Vector3.zero;
        _currentCollision = collision;
        
        // Debug.LogError($"contacts count: {collision.contacts.Length}");
        // foreach (var contactPoint in collision.contacts)
        // {
        //     // _collisionMovement += new Vector3(transform.position.x, 0f, transform.position.z) - new Vector3(contactPoint.point.x, 0f, contactPoint.point.z);
        //     
        //     _collisionNormal += contactPoint.normal;
        // }
        //
        // _collisionNormal = _collisionNormal.normalized;

        var contactPoint = collision.contacts[0];
        _collisionNormal = contactPoint.normal;
        
        _normals.Clear();
        _normals.AddRange(collision.contacts.Select(c => c.normal));
    }

    private void OnCollisionExit(Collision other)
    {
        _collisionMovement = Vector3.zero;
        _collisionNormal = Vector3.zero;
        _currentCollision = null;
        _normals.Clear();
    }

    private void OnDisable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Disable();
    }
}
