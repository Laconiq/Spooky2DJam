using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BatController : MonoBehaviour
{
    [HideInInspector] public Controls controls;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    public float speed;

    public void Awake()
    {
        controls = new Controls();
        controls.Bat.Move.performed += OnMovePerformed;
        controls.Bat.Move.canceled += OnMoveCanceled;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        controls.Bat.Disable();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        Vector2 moveVelocity = moveInput * speed;
        _rigidbody2D.velocity = moveVelocity;
        if (moveInput.x > 0)
            _spriteRenderer.flipX = false;
        else if (moveInput.x < 0)
            _spriteRenderer.flipX = true;
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _rigidbody2D.velocity = Vector2.zero;
    }
}