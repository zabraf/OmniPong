using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerL1 : MonoBehaviour
{
    public GameObject PaddleJ1;
    public GameObject PaddleJ2;
    public GameObject Ball;
    public Vector2 topLeft;
    public Vector2 bottomRight;
    public Text Score1;
    public Text Score2;
    public Text VictoryText;
    public int scoreToWin;

    public bool training;
    public PaddleAgentL1 AIAgent1;
    public PaddleAgentL1 AIAgent2;

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
            SceneManager.LoadScene("Menu");
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

            if (scoreLeft > scoreToWin && !training)
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

            if (scoreRight > scoreToWin && !training)
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
        int nextLevel = PlayerPrefs.GetInt("Level") + 1;
        StartCoroutine("TextWrite", text);
        Debug.Log(PlayerPrefs.GetInt("Switch"));
        Debug.Log(nextLevel);
        yield return new WaitForSeconds(3);
        
        if(PlayerPrefs.GetInt("Switch") == 1)
        {
            if(nextLevel <= 3)
            { 
                PlayerPrefs.SetInt("Level", nextLevel);
                SceneManager.LoadScene("Level" + nextLevel);
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
        else
        {
            SceneManager.LoadScene("Level" + (nextLevel - 1));
        }
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
