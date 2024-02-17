using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private Vector2 moveVector;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomSensitivity;

    [SerializeField] private Camera cam;
    private float camSize = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        cam.orthographicSize = camSize;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector * moveSpeed;
    }

    public void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    public void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
    }

    public void OnZoomPerformed(InputAction.CallbackContext context)
    {
        camSize -= context.ReadValue<float>() * zoomSensitivity;
        cam.orthographicSize = camSize;
    }
}
