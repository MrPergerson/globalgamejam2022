using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    Player player;
    Rigidbody2D rb;
    [SerializeField] private float speed = .5f;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }


    void FixedUpdate()
    {
        Vector2 heading = player.transform.position - transform.position;
        var distance = heading.magnitude; // this is a slow calcuation
        var direction = heading / distance;

        if(distance > .5)
            rb.MovePosition((Vector2)transform.position + direction * speed * Time.fixedDeltaTime);

    }
}
