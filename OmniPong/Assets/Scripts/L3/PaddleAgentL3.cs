using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAgentL3 : Agent
{
    public GameObject Ball;
    public GameControllerL3 gameController;


    public override void AgentAction(float[] vectorAction, string textAction)
    {
        base.AgentAction(vectorAction, textAction);

        if (vectorAction.Length > 0)
        {
            if (vectorAction[0] == 1)
            {
                this.gameObject.GetComponent<MLPlayer>().movingDirection = 1;
            }
            else if (vectorAction[0] == 2)
            {
                this.gameObject.GetComponent<MLPlayer>().movingDirection = -1;
            }
            else
            {
                this.gameObject.GetComponent<MLPlayer>().movingDirection = 0;
            }
        }
    }

    public override void CollectObservations()
    {
        base.CollectObservations();

        float ourHeight = this.transform.position.y;

        AddVectorObs(gameController.htba);
        AddVectorObs(ourHeight);
    }

    public override float[] Heuristic()
    {
        return new float[0];
    }
}
