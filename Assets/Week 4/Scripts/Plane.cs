using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public List<Vector2> points;
    public float newPosThresh = 0.2f;
    Vector2 lastPos;
    LineRenderer lineR;
    Vector2 currentPos;
    Rigidbody2D rb;
    public float speed;
    public AnimationCurve landing;
    float landTimer;

    private void Start()
    {
        lineR = GetComponent<LineRenderer>();
        lineR.positionCount = 1;
        lineR.SetPosition(0, transform.position);

        rb = GetComponent<Rigidbody2D>();

        speed = Random.Range(1, 3);
    }

    private void FixedUpdate()
    {
        currentPos = transform.position;
        if(points.Count > 0 ) 
        {
            Vector2 direction = points[0] - currentPos;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            rb.rotation = -angle;
        }
        rb.MovePosition(rb.position + (Vector2)transform.up * speed * Time.deltaTime);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space)) 
        {
            landTimer += 0.5f * Time.deltaTime;
            float interpolation = landing.Evaluate(landTimer);
            if (transform.localScale.z < 0.1f)
            {
                Destroy(gameObject);
            }
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, interpolation);
        }

        lineR.SetPosition(0, transform.position);
        if (points.Count > 0)
        {
            if(Vector2.Distance(currentPos, points[0]) < newPosThresh) 
            {
                points.RemoveAt(0);

                for (int i = 0; i < lineR.positionCount - 2; i++)
                {
                    lineR.SetPosition(i, lineR.GetPosition(i + 1));
                }
                lineR.positionCount--;
            }
        }
    }

    private void OnMouseDown()
    {
        points = new List<Vector2>();
        lineR.positionCount = 1;
        lineR.SetPosition(0, transform.position);
    }

    private void OnMouseDrag()
    {
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(lastPos, newPos) > newPosThresh) 
        {
            points.Add(newPos);
            lineR.positionCount++;
            lineR.SetPosition(lineR.positionCount -1, newPos);
            lastPos = newPos;
        }
    }

}