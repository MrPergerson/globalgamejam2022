using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight2 : MonoBehaviour
{
    [SerializeField] Camera camera;
    PlayerControls playerControls;

    void Awake()
    {
        playerControls = new PlayerControls();
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

        if(GameManager.Instance.isPaused != true)
        {
            Vector2 mouseScreenPosition = playerControls.Player.Look.ReadValue<Vector2>();
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            Vector3 directionToMousePosition = mouseWorldPosition - transform.position;
            float angleToMousePosition = Mathf.Atan2(directionToMousePosition.y, directionToMousePosition.x);
            angleToMousePosition = angleToMousePosition * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angleToMousePosition));

        }
    }
}
