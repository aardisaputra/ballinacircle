using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class BallScript : MonoBehaviour
{
    public float forceUpperRange;
    public float forceLowerRange;

    private Rigidbody2D rb;
    private Vector2 force;
    private float forceY;

    public event Action addScoreEvent;
    public event Action gameOver;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        force = new Vector2(0, 0);
        forceY = 0f;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(gameObject.transform.position.y < -12 && gameOver != null)
        {
            gameOver();
            Destroy(gameObject);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                if ((gameObject.transform.position.y <= -1.35 && gameObject.transform.position.y >= -1.65) && addScoreEvent != null)
                {
                    addScoreEvent();
                    rb.velocity = new Vector2(0, 0);
                }
            }
        }
            
    }

    private void FixedUpdate()
    {
        forceY = Random.Range(-forceUpperRange, -forceLowerRange);
        force.y = forceY;
        rb.AddForce(force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if((gameObject.transform.position.y >= -1.35 ||  gameObject.transform.position.y <= -1.65))
        {
            gameOver();
        }
        
    }
}
