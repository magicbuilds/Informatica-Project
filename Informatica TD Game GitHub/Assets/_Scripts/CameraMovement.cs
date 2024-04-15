using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private Vector2 moveVector;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float baseZoomSensitivity;

    [SerializeField] private Camera cam;
    [SerializeField] private float camSize = 5f;
    [SerializeField] private Rigidbody2D rb;

    private float minCamSize = 1f;
    private float maxCamSize = 30f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        cam.orthographicSize = camSize;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector * baseMoveSpeed * camSize;
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
        camSize -= context.ReadValue<float>() * baseZoomSensitivity * camSize;

        if (camSize < minCamSize) camSize = minCamSize;
        if (camSize > maxCamSize) camSize = maxCamSize;

        cam.orthographicSize = camSize;
    }
}
