using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    Player player;
    Rigidbody2D rb;
    [SerializeField] private float speed = 50.0f;
    [SerializeField] private bool isVisible;

    private Animator enim;
    private void Awake()
    {
        isVisible = false;
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        try
        {
            enim = GetComponentInChildren<Animator>();
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("ROBOT.CS: ANIMATOR NOT FOUND!!");
        }

    }

    void Start()
    {
        
    }


    void FixedUpdate()
    {
        if (!isVisible)
        {
            resetConstraint();
            // enim.SetBool("isHunting", true);
            Vector2 heading = player.transform.position - transform.position;
            var distance = heading.magnitude; // this is a slow calcuation
            var direction = heading / distance;

            rb.MovePosition((Vector2)transform.position + direction * speed * Time.fixedDeltaTime);
        }

        if (isVisible)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            // enim.SetBool("isHunting", false);
        }

    }
    public void updateState(bool stateChange)
    {
        isVisible = stateChange;
    }
    public bool getState()
    {
        return isVisible;
    }

    private void resetConstraint()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
