using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAcademy : Academy
{
    private GameArea gameArea;
    public override void AcademyReset()
    {
        base.AcademyReset();

        if(gameArea == null)
        {
            gameArea = GameObject.FindObjectOfType<GameArea>();
        }

        gameArea.ResetArea();
    }
}
