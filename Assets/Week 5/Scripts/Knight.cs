using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{

    Vector2 destination;
    Vector2 move;
    public float speed = 3;
    Rigidbody2D rb;
    Animator animator;
    bool clickSelf = false;
    public float hp;
    public float maxHP = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();
        hp = maxHP;
    }

    private void FixedUpdate()
    {
        move = destination - (Vector2)transform.position;
        if(move.magnitude < 0.1 ) 
        {
            move = Vector2.zero;
        }
        rb.MovePosition(rb.position + move.normalized * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !clickSelf)
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        animator.SetFloat("Movement", move.magnitude);
    }
    private void OnMouseDown()
    {
        clickSelf = true;
        takeDMG(1);
    }

    private void OnMouseUp()
    {
        clickSelf = false;
    }

    void takeDMG(float DMG)
    {
        hp -= DMG;
        hp = Mathf.Clamp(hp, 0, maxHP);
        if (hp == 0 )
        {
            //die
            animator.SetTrigger("death");
        }
        else
        {
           animator.SetTrigger("Take DMG");
        }

    }
}
