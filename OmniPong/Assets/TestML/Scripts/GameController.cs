using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject PaddleJ1;
    public GameObject PaddleJ2;
    public GameObject Ball;
    public Vector2 topLeft;
    public Vector2 bottomRight;
    public Text Score1;
    public Text Score2;

    public PaddleAgent AIAgent1;
    public PaddleAgent AIAgent2;

    public int scoreLeft = 0;
    public int scoreRight = 0;

    public float ballStartSpeed = 40;

    private Rigidbody2D ballRigidbody2D;
    void Start()
    {
        ballRigidbody2D = Ball.GetComponent<Rigidbody2D>();
        ResetWorld();
    }

    /// <summary>
    /// Setup world dynamically based on the camera's view
    /// </summary>
    public void ResetWorld()
    {
        ResetPlayers();
        ResetBall();
        StartCoroutine("LaunchBall", 1);
    }

    /// <summary>
    /// Reset the ball 
    /// </summary>
    public void ResetBall()
    {
        Ball.transform.position = Vector3.zero;
        ballRigidbody2D.position = Vector3.zero;
        ballRigidbody2D.velocity = Vector3.zero;
    }

    /// <summary>
    /// Reset the players
    /// </summary>
    public void ResetPlayers()
    {
        PaddleJ1.transform.position = new Vector3(-14.5f, 0);

        PaddleJ2.transform.position = new Vector3(14.5f, 0);
    }

    /// <summary>
    /// Launch the ball randomly
    /// </summary>
    /// <param name="delay">Delay before launching</param>
    public IEnumerator LaunchBall(float delay)
    {
        //find an angle that is not directly up or down
        float angle;
        int rdm = UnityEngine.Random.Range(0,6);
        switch (rdm)
        {
            case 0:
                angle = 0;
                break;
            case 1:
                angle = 45;
                break;
            case 2:
                angle = 135;
                break;
            case 3:
                angle = 180;
                break;
            case 4:
                angle = 225;
                break;
            case 5:
                angle = 315;
                break;
            default:
                throw new Exception("Wrong RDM");
        }
        //wait for delay
        yield return new WaitForSeconds(delay);

        //apply new speed
        ballRigidbody2D.velocity = new Vector2(Mathf.Cos(angle) * ballStartSpeed, Mathf.Sin(angle) * ballStartSpeed);
    }

    /// <summary>
    /// Score for one side
    /// </summary>
    public void ScoreGoal(bool leftSideScored)
    {
        if (leftSideScored)
        {
            scoreLeft++;
            AIAgent1?.AddReward(-1);
            AIAgent1?.Done();
            AIAgent2?.AddReward(1);
            AIAgent2?.Done();
            Score1.text = scoreLeft.ToString();
            ResetWorld();
        }
        else
        {
            scoreRight++;
            AIAgent1?.AddReward(1);
            AIAgent1?.Done();
            AIAgent2?.AddReward(-1);
            AIAgent2?.Done();
            Score2.text = scoreRight.ToString();
            ResetWorld();
        }
    }
}
