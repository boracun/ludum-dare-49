using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableMovement : MonoBehaviour
{
    public Transform waiterTransform;

    public Rigidbody2D rBody;

    public float speed;
    // Start is called before the first frame update
    void Start()
    { 
        waiterTransform = GameObject.FindGameObjectWithTag("Waiter").transform;
        rBody = gameObject.GetComponent<Rigidbody2D>();
        var positionWaiter = waiterTransform.position;
        float waiterX = positionWaiter.x;
        float waiterY = positionWaiter.y;
        
        var position = transform.position;
        float goalX = waiterX - position.x;
        float goalY = waiterY - position.y;
        /*Vector2 direction = new Vector2((Math.Abs(goalX) / (float)Math.Sqrt((Math.Pow(Math.Abs(goalX), 2f) + Math.Pow(Math.Abs(goalY), 2f)))),
            (Math.Abs(goalY) / (float)Math.Sqrt(Math.Pow(Math.Abs(goalX), 2f) + Math.Pow(Math.Abs(goalY), 2f)))).normalized;*/
        Vector2 direction = new Vector2(goalX, goalY).normalized;
        rBody.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, 4f);
    }
}
