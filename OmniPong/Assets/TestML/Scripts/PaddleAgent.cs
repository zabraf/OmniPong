using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAgent : Agent
{
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
}
