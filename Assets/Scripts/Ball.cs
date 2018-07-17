using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public delegate void BallTouchedGround(int whom);
    public event BallTouchedGround Score;

    private Rigidbody rb;

    [SerializeField]
    private float lobStrength = 30;

    public enum Direction
    {
        TO_PLAYER,
        TO_OPPONENT
    };
    
    public Direction BallTurn
    {
        get;
        set;
    }
    
    private bool doLob = false;
    private bool doSmash = false;

    public void Lob()
    {
        doLob = true;
        BallTurn = BallTurn == Direction.TO_PLAYER ? Direction.TO_OPPONENT : Direction.TO_PLAYER;
    }

    public void Smash()
    {
        doSmash = true;
        BallTurn = BallTurn == Direction.TO_PLAYER ? Direction.TO_OPPONENT : Direction.TO_PLAYER;
    }

    public void ResetBall(GameObject whoStarts, Direction dir)
    {
        rb.velocity = Vector3.zero;
        this.transform.position = whoStarts.transform.position + Vector3.up * 15;
        BallTurn = dir;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        BallTurn = Direction.TO_PLAYER;
	}

    void FixedUpdate()
    {
        float forwardFactor = BallTurn == Direction.TO_PLAYER ? -1 : 1;
        float upFactor = 1;
        float sideFactor = Random.Range(-0.2f, 0.2f);

        if (doLob)
        {
            forwardFactor *= 0.7f;

            doLob = false;
        }
        else if(doSmash)
        {
            upFactor *= 0.5f;

            doSmash = false;
        }
        else
        {
            forwardFactor = 0;
            upFactor = 0;
            sideFactor = 0;
        }

        Vector3 impulse = Vector3.forward * forwardFactor + Vector3.right * sideFactor + Vector3.up * upFactor;

        if(impulse.magnitude != 0)
            rb.AddForce(impulse * lobStrength, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        int whoScored = GameManager.PLAYER;

        if (collision.gameObject.CompareTag("PlayFieldPlayer"))
        {
            if (BallTurn == Direction.TO_OPPONENT)
            {
                whoScored = GameManager.PLAYER;
            }
            else
            {
                whoScored = GameManager.OPPONENT;
            }
        }
        else if (collision.gameObject.CompareTag("PlayFieldOpponent"))
        {
            if (BallTurn == Direction.TO_PLAYER)
            {
                whoScored = GameManager.OPPONENT;
            }
            else
            {
                whoScored = GameManager.PLAYER;
            }
        }
        else
        {
            whoScored = BallTurn == Direction.TO_OPPONENT ? GameManager.PLAYER : GameManager.OPPONENT;
        }

        if (Score != null)
        {
            Score(whoScored);
        }
    }
}
