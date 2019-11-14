using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject PaddleJ1;
    public GameObject PaddleJ2;
    public GameObject Ball;
    public Vector2 topLeft;
    public Vector2 bottomRight;
    public Text Score1;
    public Text Score2;
    public Text VictoryText;


    public bool training;
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        List<Vector2> rayPoints = new List<Vector2>();
        rayPoints.Add(this.Ball.transform.position);
        Vector2 ballPos = this.Ball.transform.position;
        Vector2 rayhitPoint = this.Ball.transform.position;
        Vector2 ballVelocity = this.Ball.GetComponent<Rigidbody2D>().velocity;

        if (ballVelocity != Vector2.zero)
        {
            float slope = ballVelocity.y / ballVelocity.x;
            
            //see geogebra doc
            //calculate the next points the ball will hit
            while (rayhitPoint.x > topLeft.x && rayhitPoint.x < bottomRight.x && rayPoints.Count < 20)
            {
                float y;
                float x;
                float b = -(slope * rayhitPoint.x - rayhitPoint.y);
                if (ballVelocity.y > 0)
                {
                    y = topLeft.y;
                    x = (y - b) / slope;
                }
                else if(ballVelocity.y == 0)
                {
                    y = ballPos.y;

                    if (ballVelocity.x > 0)
                        x = bottomRight.x;
                    else
                        x = topLeft.x;
                }
                else
                {
                    y = bottomRight.y;
                    x = (y - b) / slope;
                }

                if(x < topLeft.x)
                {
                    x = topLeft.x;
                    y = slope * x + b;
                }
                else if(x > bottomRight.x)
                {
                    x = bottomRight.x;
                    y = slope * x + b;
                }

                rayhitPoint = new Vector2(x,y);//add the velocity
                
                //inverse the slope for the next calculation
                slope *= -1;
                ballVelocity.y *= -1;

                rayPoints.Add(rayhitPoint);
            }

            //draw the trajectory found
            for (int i = 0; i < rayPoints.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(rayPoints[i].x, rayPoints[i].y), new Vector3(rayPoints[i + 1].x, rayPoints[i + 1].y), Color.red);
            }

            //if the ball is going left
            if (ballVelocity.x < 0)
            {
                //score = negative distance to where the ball will be + Paddlesize/2 (Min 0) 
                float score = -Vector2.Distance(PaddleJ1.transform.position, rayPoints[rayPoints.Count - 1]);
                score += PaddleJ1.GetComponent<SpriteRenderer>().bounds.size.y / 2;
                AIAgent1?.AddReward(score);
            }
            else
            {
                //score = negative distance to where the ball will be + Paddlesize/2 (Min 0) 
                float score = -Vector2.Distance(PaddleJ2.transform.position, rayPoints[rayPoints.Count - 1]);
                score += PaddleJ2.GetComponent<SpriteRenderer>().bounds.size.y / 2;
                AIAgent2?.AddReward(score);
            }
        }
    }

    /// <summary>
    /// Setup world dynamically based on the camera's view
    /// </summary>
    public void ResetWorld()
    {
        ResetPlayers();
        ResetBall();
        StartCoroutine("LaunchBall", 1);
        VictoryText.text += "";
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
        float angle = 0;
        float maxAngle = 90;
        int side = UnityEngine.Random.Range(0, 2);

        while(angle == 180 || angle == 0)
        {
            angle = UnityEngine.Random.Range(0, maxAngle);
            angle += (side == 0) ? 135 : -45 ;
        }
        //wait for delay
        yield return new WaitForSeconds(delay);

        //apply new speed
        ballRigidbody2D.velocity = new Vector2(Mathf.Cos(angle *Mathf.Deg2Rad) * ballStartSpeed, Mathf.Sin(angle * Mathf.Deg2Rad) * ballStartSpeed);
        
    }

    /// <summary>
    /// Score for one side
    /// </summary>
    public void ScoreGoal(bool leftSideScored)
    {
        if (leftSideScored)
        {
            scoreLeft++;
            Score1.text = scoreLeft.ToString();

            if (scoreLeft > 9 && !training)
            {
                Destroy(Ball);
                StartCoroutine("Victory", "PLAYER 1 WON");
            }
            else
            {
                ResetWorld();
            }
        }
        else
        {
            scoreRight++;
            Score2.text = scoreRight.ToString();

            if (scoreRight > 9 && !training)
            {
                Destroy(Ball);
                StartCoroutine("Victory", "PLAYER 2 WON");
            }
            else
            {
                ResetWorld();
            }
        }
        AIAgent1?.Done();
        AIAgent2?.Done();
    }
    public IEnumerator Victory(string text)
    {
        StartCoroutine("TextWrite", text);
        yield return new WaitForSeconds(3.5F);
        SceneManager.LoadScene("Menu");
    }
    public IEnumerator TextWrite(string text)
    {
        foreach (char c in text)
        {
            VictoryText.text += c;
            yield return new WaitForSeconds(0.125f);
        }
        yield return new WaitForSeconds(1);
        VictoryText.text = "";
    }
}
