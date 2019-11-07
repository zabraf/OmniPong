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
    public GameObject WallTop;
    public GameObject WallBot;
    public GameObject WallRight;
    public GameObject WallLeft;
    public Text Score1;
    public Text Score2;
    public Text VictoryText;

    public PaddleAgent AIAgent;

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
    }

    /// <summary>
    /// Setup world dynamically based on the camera's view
    /// </summary>
    public void ResetWorld()
    {
        ResetPlayers();
        ResetBall();

        WallRight.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        WallRight.transform.localScale = new Vector3(1, Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight)).y / 1.2F, 1);
        WallRight.transform.position = new Vector3(WallRight.transform.position.x + WallRight.GetComponent<SpriteRenderer>().bounds.size.x / 2, WallRight.transform.position.y, 10);

        WallLeft.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight / 2));
        WallLeft.transform.localScale = new Vector3(1, Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight)).y / 1.2F, 1);
        WallLeft.transform.position = new Vector3(WallLeft.transform.position.x - WallLeft.GetComponent<SpriteRenderer>().bounds.size.x / 2, WallLeft.transform.position.y, 10);

        WallTop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight));
        WallTop.transform.localScale = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0)).x * 7, 0.1F, 1);
        WallTop.transform.position = new Vector3(WallTop.transform.position.x, WallTop.transform.position.y + WallTop.GetComponent<SpriteRenderer>().bounds.size.y / 2, 10);

        WallBot.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, 0));
        WallBot.transform.localScale = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0)).x * 7, 0.1F, 1);
        WallBot.transform.position = new Vector3(WallBot.transform.position.x, WallBot.transform.position.y - WallBot.GetComponent<SpriteRenderer>().bounds.size.y / 2, 10);
        StartCoroutine("LaunchBall", 1);
        VictoryText.text += "";
    }

    /// <summary>
    /// Reset the ball 
    /// </summary>
    public void ResetBall()
    {
        Ball.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 10));
        ballRigidbody2D.position = Vector3.zero;
        ballRigidbody2D.velocity = Vector3.zero;
    }

    /// <summary>
    /// Reset the players
    /// </summary>
    public void ResetPlayers()
    {
        PaddleJ1.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight / 2));
        PaddleJ1.transform.position = new Vector3(PaddleJ1.transform.position.x + PaddleJ1.GetComponent<SpriteRenderer>().bounds.size.x, PaddleJ1.transform.position.y, 10);

        PaddleJ2.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        PaddleJ2.transform.position = new Vector3(PaddleJ2.transform.position.x - PaddleJ2.GetComponent<SpriteRenderer>().bounds.size.x, PaddleJ2.transform.position.y, 10);
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
                break;
        }
        //wait for delay
        yield return new WaitForSeconds(delay);

        //apply new speed
        ballRigidbody2D.velocity = new Vector2(Mathf.Cos(angle) * ballStartSpeed, Mathf.Sin(angle) * ballStartSpeed);
    }

    /// <summary>
    /// Score for a paddle
    /// </summary>
    public void ScoreGoal(GameObject goal)
    {
        if (goal == WallRight)
        {
            scoreLeft++;
            AIAgent?.AddReward(-1);
            Score1.text = scoreLeft.ToString();
            if (scoreLeft > 9)
            {
                Destroy(Ball);
                StartCoroutine("Victory", "PLAYER 1 WON");
            }
            else
            {
                ResetWorld();
            }
            
        }
        else if (goal == WallLeft)
        {

            scoreRight++;
            AIAgent?.AddReward(1);
            Score2.text = scoreRight.ToString();
            if (scoreRight > 9)
            {
                Destroy(Ball);
                StartCoroutine("Victory", "PLAYER 2 WON");
            }
            else
            {
                ResetWorld();
            }
            
        }
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
