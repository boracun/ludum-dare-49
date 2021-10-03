using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableMovement : MonoBehaviour
{
    public Transform waiterTransform;

    public Rigidbody2D rBody;

    public float speed = 2f;
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
        rBody.velocity = new Vector2((Math.Abs(goalX) / (float)(Math.Pow(Math.Abs(goalX), 2f) + Math.Pow(Math.Abs(goalY), 2f))) * speed,
            (Math.Abs(goalY) / (float)(Math.Pow(Math.Abs(goalX), 2f) + Math.Pow(Math.Abs(goalY), 2f))) * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
