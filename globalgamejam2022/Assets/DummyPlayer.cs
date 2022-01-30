using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{

    PlayerControls playerControls;
    public FlashLight fl;
    public Player player;

    void Start()
    {
        playerControls = new PlayerControls();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPosition = playerControls.Player.Look.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 targetDirection = (mouseWorldPosition - transform.position).normalized;
        fl.SetAimDirection(targetDirection);
        fl.SetOrigin(player.transform.position);

        //float angle = Mathf.
    }
}
