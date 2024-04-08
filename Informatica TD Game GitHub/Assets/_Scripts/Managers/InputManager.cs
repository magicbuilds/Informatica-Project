using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInputActions input;

    [SerializeField] private CameraMovement cam;

    private void Awake()
    {
        Instance = this;

        input = new PlayerInputActions();
    }

    public void EnablePlayerInput()
    {
        input.Player.Enable();

        input.Player.Movement.performed += cam.OnMovementPerformed;
        input.Player.Movement.canceled += cam.OnMovementCancelled;

        input.Player.Zoom.performed += cam.OnZoomPerformed;
    }

    public void DisablePlayerInput()
    {
        input.Player.Disable();

        input.Player.Movement.performed -= cam.OnMovementPerformed;
        input.Player.Movement.canceled -= cam.OnMovementPerformed;

        input.Player.Zoom.performed -= cam.OnMovementPerformed;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
