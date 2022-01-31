using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight2 : MonoBehaviour
{
    [SerializeField] Camera camera;
    PlayerControls playerControls;
    
    // attribs for gamepad functionality
    // drag and drop the Player game object in inspector into player field
    public GameObject player;
    private Player playerController;
    private Vector2 look;
    private float angleToRAnalogPosition;
    private bool isGamePadEnabled;
    void Awake()
    {
        playerControls = new PlayerControls();
        playerController = player.GetComponent<Player>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        isGamePadEnabled = playerController.usingGamepad;
        // executes if the gamepad is not the currently used device in input system
        if (isGamePadEnabled)
        {
            if (GameManager.Instance.isPaused != true)
            {
                Vector2 mouseScreenPosition = playerControls.Player.Look.ReadValue<Vector2>();
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

                Vector3 directionToMousePosition = mouseWorldPosition - transform.position;
                float angleToMousePosition = Mathf.Atan2(directionToMousePosition.y, directionToMousePosition.x);
                angleToMousePosition = angleToMousePosition * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angleToMousePosition));

            }
        }
        // executes if gamePad is the currently used device in input system
        else
        {
            // Gets the value from user input
            look = playerControls.Player.Look.ReadValue<Vector2>();
            
            angleToRAnalogPosition = Mathf.Atan2(look.y, look.x);
            angleToRAnalogPosition = angleToRAnalogPosition * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angleToRAnalogPosition));

        }

    }
    
}
