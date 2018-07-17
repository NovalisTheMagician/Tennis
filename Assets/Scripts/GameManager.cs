using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const int PLAYER = 0;
    public const int OPPONENT = 1;

    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject opponent;

    [SerializeField]
    private Text playerScoreText;

    [SerializeField]
    private Text opponentScoreText;

    private int playerScore;
    private int opponentScore;

    void Start()
    {
        ball.GetComponent<Ball>().Score += OnScore;
	}
	
	void Update()
    {
		
	}

    private void OnScore(int who)
    {
        GameObject nextStart = player;
        Ball.Direction newDir = Ball.Direction.TO_PLAYER;
        if(who == PLAYER)
        {
            playerScore++;
            playerScoreText.text = playerScore.ToString();
        }
        else
        {
            opponentScore++;
            opponentScoreText.text = opponentScore.ToString();

            nextStart = opponent;
            newDir = Ball.Direction.TO_OPPONENT;
        }

        ball.GetComponent<Ball>().ResetBall(nextStart, newDir);
    }
}
