using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private Controls _controls;
    private float _horizontalInput;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 5;
    private bool _isGrounded;
    private void Awake()
    {
        _controls = new Controls();
        _controls.Player.Move.performed += OnMovePerformed;
        _controls.Player.Move.canceled += OnMoveCanceled;
        _controls.Player.Jump.performed += OnJumpPerformed;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _controls.Player.Enable();
    }
    private void OnDisable()
    {
        _controls.Player.Disable();
    }
    private void OnDestroy()
    {
        _controls.Player.Move.performed -= OnMovePerformed;
        _controls.Player.Move.canceled -= OnMoveCanceled;
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<Vector2>().x;
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _horizontalInput = 0f;
    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (_isGrounded)
            Jump();
    }
    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
    }
    private void Update()
    {
        _isGrounded = Physics2D.IsTouchingLayers(_rigidbody2D.GetComponent<Collider2D>(), LayerMask.GetMask("Ground"));
        _rigidbody2D.velocity = new Vector2(_horizontalInput * speed, _rigidbody2D.velocity.y);
    }
}
