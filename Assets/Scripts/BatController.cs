using UnityEngine;
using UnityEngine.InputSystem;

public class BatController : MonoBehaviour
{
    [HideInInspector] public Controls controls;
    private Rigidbody2D _rigidbody2D;
    public float speed;
    public void Awake()
    {
        controls = new Controls();
        controls.Bat.Move.performed += OnMovePerformed;
        controls.Bat.Move.canceled += OnMoveCanceled;
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _rigidbody2D.velocity = Vector2.zero;
    }
}