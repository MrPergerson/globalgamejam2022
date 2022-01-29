using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{

    public InputData input;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var move = new Vector2(input.move.x, input.move.y);
        rb.MovePosition(rb.position + move * 10 * Time.deltaTime);
    }
}
