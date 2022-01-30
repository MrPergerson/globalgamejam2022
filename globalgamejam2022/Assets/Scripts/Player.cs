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

    public FlashLight flash;

    //  TEST: temp attribs for debugging
    public Vector3 lookingDirection;
    public Quaternion updateRotation;
    public float thirdRotVal;

    public Light2D spotLight;
    public Light2D pointLight;
    public FieldofView fov;

    private Animator anim;

    void Awake()
    {
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        rigiBod = GetComponent<Rigidbody2D>();
        playerSpeed = 5.0f;
        // flash.SetOrigin(transform.position);
        // anim = GetComponent<Sprite>().GetComponent<Animator>();
        try
        {
            anim = GetComponentInChildren<Animator>();
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("PLAYER.CS: ANIMATOR NOT FOUND!!");
        }
        
    }

    private void Start()
    {
        //flash.SetOrigin (transform.position);
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

    /**
     * Fixed Update
     *  - handles player movement
     *  - handles player torch rotation [FIX ME: GamePad Functionality broken]
     *  
     */
    private void FixedUpdate()
    {

        thirdRotVal = rotationSmoothing * Time.deltaTime;
        move = playerControls.Player.Move.ReadValue<Vector2>();
        look = playerControls.Player.Look.ReadValue<Vector2>();
        rigiBod.MovePosition (((Vector2) transform.position) + (move * playerSpeed * Time.fixedDeltaTime));
        //flash.SetOrigin(transform.position);
        //flash.SetAimDirection(look);
        if ((move.x != 0) || (move.y != 0))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        /*
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
        */
    }

    public void Update()
    {
        // flash.SetOrigin(transform.position);
        // flash.SetAimDirection(look);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Player.cs: Player collision triggered!!!");
        if (other.gameObject.CompareTag("Enemy"))
        {
            Robot temp = other.gameObject.GetComponent<Robot>(); 
            if (!temp.getState())
            {
                GameManager.Instance.EndGame();
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
