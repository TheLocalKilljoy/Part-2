using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{

    Vector2 destination;
    Vector2 move;
    public float speed = 3;
    Rigidbody2D rb;
    public Animator animator;
    bool clickSelf = false;
    public float hp;
    public float maxHP = 5;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();
        hp = maxHP;
    }

    private void FixedUpdate()
    {
        if(isDead) return;
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
        if (isDead) return;
        if (Input.GetMouseButtonDown(0) && !clickSelf)
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        animator.SetFloat("Movement", move.magnitude);

        if (Input.GetMouseButtonDown(1))
        {
            clickSelf = true;
            animator.SetTrigger("attack");
        }
    }
    private void OnMouseDown()
    {
        if (isDead) return;
        clickSelf = true;
        SendMessage("takeDMG", 1);
    }

    private void OnMouseUp()
    {
        if (isDead) return;
        clickSelf = false;
    }

    public void takeDMG(float DMG)
    {
        hp -= DMG;
        hp = Mathf.Clamp(hp, 0, maxHP);
        if (hp == 0 )
        {
            isDead = true;
            animator.SetTrigger("death");
        }
        else
        {
            isDead= false;
           animator.SetTrigger("Take DMG");
        }

    }
}
