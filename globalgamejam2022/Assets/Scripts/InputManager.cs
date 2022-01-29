using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputData input;

    private PlayerControls playerControls;
    private PlayerInput playerInput;

    // Controller related attribs
    //[SerializeField] private float controllerDeadZone = 0.1f;
    [SerializeField] private float rotationSmoothing = 1000.0f;


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
        
        // FIX ME: FIX THIS FOR ROTATION INPUT
        if (input.usingGamepad)
        {
            // for 'looking'/rotating the layer using a gamepad
            // TEST: is 'Vector3.right' and 'Vector3.forward' in the tutorial
            Vector3 lookingDirection = (Vector3.right * input.look.x) + (Vector3.forward * input.look.y);
            if (lookingDirection.sqrMagnitude > 0.0f)
            {
                // TEST: is 'Vector3.up' in the tutorial

                Quaternion updateRotation = Quaternion.LookRotation(lookingDirection, Vector3.up);
                
                transform.rotation = Quaternion.RotateTowards(transform.rotation, updateRotation, rotationSmoothing * Time.deltaTime);
                
            }
        }
        
    }

    // SHAHBAZ: Changed to public so that the InputManager Game object can access it easily
    public void UpdateCurrentDevice(PlayerInput playerInput)
    {
        input.usingGamepad = playerInput.currentControlScheme.Equals("Gamepad") ? true : false;
    }


}
