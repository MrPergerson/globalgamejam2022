using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputData input;

    private PlayerControls playerControls;
    private PlayerInput playerInput;

    void Awake()
    {
        playerControls = new PlayerControls();
        input.playerControls = playerControls.Player;
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerControls.Enable();

        playerInput.onControlsChanged += UpdateCurrentDevice;
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        input.move = playerControls.Player.Move.ReadValue<Vector2>();
        input.look = playerControls.Player.Look.ReadValue<Vector2>();
    }


    private void UpdateCurrentDevice(PlayerInput playerInput)
    {
        input.usingGamepad = playerInput.currentControlScheme.Equals("Gamepad") ? true : false;
    }
}
