using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    Player player;
    Rigidbody2D rb;
    [SerializeField] private float speed = 50.0f;
    [SerializeField] private bool isHunting;
    [SerializeField] private bool isVisible;
    private float seenProgress;
    private float timeInIdle;
    [SerializeField] private float flashLightTolerance = 2;
    [SerializeField] private float maxTimeInIdle = 8;

    [SerializeField] private SpriteRenderer spriteRender;
    [SerializeField] private Material idleMaterial;
    [SerializeField] private Material huntingMaterial;

    private Animator enim;
    private void Awake()
    {
        isVisible = false;
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        spriteRender = GetComponentInChildren<SpriteRenderer>();
        if (spriteRender == null) Debug.LogError("No Sprite Renderer found in child of Robot.cs");

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
        if(isHunting)
        {
            if (!isVisible)
            {
                resetConstraint();
                enim.SetBool("isHunting", true);
                Vector2 heading = player.transform.position - transform.position;
                var distance = heading.magnitude; // this is a slow calcuation
                var direction = heading / distance;

                rb.MovePosition((Vector2)transform.position + direction * speed * Time.fixedDeltaTime);
            }

            if (isVisible)
            {
                seenProgress += Time.deltaTime;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                enim.SetBool("isHunting", false);
            }

            if(seenProgress > flashLightTolerance)
            {
                print("isIdle");
                isHunting = false;
                timeInIdle = 0;
                spriteRender.material = idleMaterial;
                return;
            }
        }
        else
        {
            if (timeInIdle >= maxTimeInIdle)
            {
                print("isHunting");
                isHunting = true;
                seenProgress = 0;
                spriteRender.material = huntingMaterial;
                return;
            }

            timeInIdle += Time.deltaTime;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            enim.SetBool("isHunting", false);
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
