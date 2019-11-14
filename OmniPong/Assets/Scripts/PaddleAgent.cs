using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAgent : Agent
{
    public GameObject Ball;
    public GameObject Opponent;


    public override void AgentAction(float[] vectorAction, string textAction)
    {
        base.AgentAction(vectorAction, textAction);

        if(vectorAction[0] == 1)
        {
            this.gameObject.GetComponent<MLPlayer>().movingDirection = 1;
        }
        else if(vectorAction[0] == 2)
        {
            this.gameObject.GetComponent<MLPlayer>().movingDirection = -1;
        }
        else
        {
            this.gameObject.GetComponent<MLPlayer>().movingDirection = 0;
        }
    }

    public override void CollectObservations()
    {
        base.CollectObservations();

        Vector2 positionOfBall = Ball.transform.position;
        Vector2 speedOfBall = Ball.GetComponent<Rigidbody2D>().velocity;
        float heightOfOpponent = Opponent.transform.position.y;
        float ourHeight = this.transform.position.y;

        AddVectorObs(positionOfBall);
        AddVectorObs(speedOfBall);
        //AddVectorObs(heightOfOpponent);
        AddVectorObs(ourHeight);
    }

    public override float[] Heuristic()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return new float[] { 1 };
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            return new float[] { 2 };
        }
        return new float[] { 0 };
    }
}
