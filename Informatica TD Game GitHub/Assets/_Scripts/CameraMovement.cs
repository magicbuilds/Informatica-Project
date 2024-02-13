using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private PlayerInputActions input;

    private Vector2 moveVector;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomSensitivity;

    [SerializeField] private Camera cam;
    private float camSize = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        input = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();

        cam.orthographicSize = camSize;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector * moveSpeed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
    }

    private void OnZoomPerformed(InputAction.CallbackContext context)
    {
        camSize -= context.ReadValue<float>() * zoomSensitivity;
        cam.orthographicSize = camSize;
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;

        input.Player.Zoom.performed += OnZoomPerformed;
    }

    private void OnDisable()
    {
        input.Disable();

        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;

        input.Player.Zoom.performed -= OnZoomPerformed;
    }
}
