using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;

    Vector2 end;
    Vector2 move;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        move = end - (Vector2)transform.position;
        if (move.magnitude < 0.5)
        {
            move = Vector2.zero;
        }
        rb.MovePosition(rb.position + move.normalized * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            end = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
    }
}
