using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float minBallDist = 0.5f;

    [SerializeField]
    private float thrust = 50;

    [SerializeField]
    private float maxRunSpeed = 5;

    [SerializeField]
    private GameObject ball;

	void Start()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update()
    {
		
	}

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * thrust);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector3.right * thrust);
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector3.forward * thrust);
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector3.back * thrust);
        }

        float ballDist = (ball.transform.position - this.transform.position).magnitude;

        //Lob
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(ballDist <= minBallDist)
            {
                ball.GetComponent<Ball>().Lob();
            }
        }
        //Smash
        else if(Input.GetKeyDown(KeyCode.E))
        {
            if (ballDist <= minBallDist)
            {
                ball.GetComponent<Ball>().Smash();
            }
        }
    }

    void LateUpdate()
    {
        if(rb.velocity.magnitude > maxRunSpeed)
            rb.velocity = Vector3.Normalize(rb.velocity) * maxRunSpeed;
    }
}
