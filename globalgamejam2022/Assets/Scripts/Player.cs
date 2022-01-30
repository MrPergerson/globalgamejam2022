using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerInput playerInput;
    public Vector2 move;
    public Vector2 look;
    public bool usingGamepad;

    public Rigidbody2D rigiBod;
    public float playerSpeed;
    // Controller related attribs
    //[SerializeField] private float controllerDeadZone = 0.1f;
    [SerializeField] private float rotationSmoothing = 1000.0f;

    //  TEST: temp attribs for debugging
    public Vector3 lookingDirection;
    public Quaternion updateRotation;
    public float thirdRotVal;

    public Light2D spotLight;
    public Light2D pointLight;
    public FieldofView fov;
    

    void Awake()
    {
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        rigiBod = GetComponent<Rigidbody2D>();
        playerSpeed = 5.0f;
    }

    private void OnEnable()
    {
        playerControls.Enable();

        //playerInput.onControlsChanged += UpdateCurrentDevice;
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void FixedUpdate()
    {
        thirdRotVal = rotationSmoothing * Time.deltaTime;
        move = playerControls.Player.Move.ReadValue<Vector2>();
        look = playerControls.Player.Look.ReadValue<Vector2>();
        rigiBod.MovePosition (((Vector2) transform.position) + (move * playerSpeed * Time.fixedDeltaTime));


        //Debug.Log("Executed the FixedUpdate");
        // FIX ME: FIX THIS FOR ROTATION INPUT
        if (usingGamepad)
        {

            Debug.Log("Executed the usingGamepad");
            // for 'looking'/rotating the layer using a gamepad
            // TEST: is 'Vector3.right' and 'Vector3.forward' in the tutorial
            // Vector3 lookingDirection = (Vector3.right * look.x) + (Vector3.forward * look.y);
            lookingDirection = (Vector3.right * look.x) + (Vector3.forward * look.y);

            if (lookingDirection.sqrMagnitude > 0.0f)
            {
                // TEST: is 'Vector3.up' in the tutorial
                Debug.Log("Executed the looking direction loop");
                //Quaternion updateRotation = Quaternion.LookRotation(lookingDirection, Vector3.up);
                updateRotation = Quaternion.LookRotation(lookingDirection, Vector3.forward);

                // transform.rotation = Quaternion.RotateTowards(transform.rotation, updateRotation, rotationSmoothing * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, updateRotation, thirdRotVal);
                // rigiBod.MoveRotation(Quaternion.RotateTowards(transform.rotation, updateRotation, rotationSmoothing * Time.fixedDeltaTime));


            }
        }

    }

    // SHAHBAZ: Changed to public so that the InputManager Game object can access it easily
    public void UpdateCurrentDevice(PlayerInput playerInput)
    {

        Debug.Log("PlayerInput.currentControlScheme: " + playerInput.currentControlScheme);
        
        usingGamepad = playerInput.currentControlScheme.Equals("GamePad") ? true : false;
    }
}
