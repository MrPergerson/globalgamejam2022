using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    Player player;
    Rigidbody2D rb;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        Vector2 heading = player.transform.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        //rb.MovePosition

    }
}
