using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private float thrust = 50;

    [SerializeField]
    private float maxRunSpeed = 5;

    [SerializeField]
    private float minBallDist = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate()
    {
        Ball b = ball.GetComponent<Ball>();
		if(b.BallTurn == Ball.Direction.TO_OPPONENT)
        {
            Vector3 ballPos = ball.transform.position;
            Vector3 oppPos = this.transform.position;
            Vector3 dir = ballPos - oppPos;
            dir.y = 0;
            dir = Vector3.Normalize(dir);

            rb.AddForce(dir * thrust);

            if((ballPos - oppPos).magnitude < minBallDist)
            {
                b.Lob();
            }
        }
	}

    void LateUpdate()
    {
        if (rb.velocity.magnitude > maxRunSpeed)
            rb.velocity = Vector3.Normalize(rb.velocity) * maxRunSpeed;
    }
}
