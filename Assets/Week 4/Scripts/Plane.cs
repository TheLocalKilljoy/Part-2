using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public List<Vector2> points;
    public float newPosThresh = 0.2f;
    Vector2 lastPos;
    LineRenderer lineR;

    private void Start()
    {
        lineR = GetComponent<LineRenderer>();
        lineR.positionCount = 1;
        lineR.SetPosition(0, transform.position);
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
