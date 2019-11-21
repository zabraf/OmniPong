using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLBallL3: MonoBehaviour
{
    public GameControllerL3 gameController;
    private Rigidbody2D rb;
    public float speedIncrease = 2.5f;
    //used to keep the data of velocity before collision
    private Vector2 velocity;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        velocity = rb.velocity;

        if(this.transform.position.y > gameController.topLeft.y || this.transform.position.y < gameController.bottomRight.y)
        {
            rb.velocity = new Vector2(velocity.x, -velocity.y);
        }
        else if (this.transform.position.x > gameController.bottomRight.x)
        {
            gameController.ScoreGoal(true);
        }
        else if (this.transform.position.x < gameController.topLeft.x)
        {
            gameController.ScoreGoal(false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if we hit a players
        if(col.gameObject.tag == "Player")
        {
            Vector2 newVelocity = velocity;
            Vector2 ballPos = rb.transform.position;
            Vector2 paddleCenter = col.transform.position;
            
            //If the ball is going left
            if (velocity.x < 0)
            {
                paddleCenter.x += col.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;

                //if the ball is not past the paddle
                if (ballPos.x > paddleCenter.x)
                {
                    //calculate new velocity based on the angle of the hit
                    newVelocity = new Vector2(-newVelocity.x + speedIncrease, newVelocity.y);
                    newVelocity = new Vector2(newVelocity.x + (ballPos.x - paddleCenter.x), newVelocity.y + (ballPos.y - paddleCenter.y));
                    //Add a reward to the AI if he hit the ball
                    PaddleAgent agent = col.gameObject.GetComponent<PaddleAgent>();
                    agent?.AddReward(5f);
                }
                else
                {
                    //Inverse the y direction to have some visual feedback
                    newVelocity.y = -newVelocity.y;
                }
            }
            else
            {
                paddleCenter.x -= col.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;

                //if the ball is not past the paddle
                if (ballPos.x < paddleCenter.x)
                {
                    //calculate new velocity based on the angle of the hit
                    newVelocity = new Vector2(-velocity.x - speedIncrease, velocity.y);
                    newVelocity = new Vector2(newVelocity.x + (ballPos.x - paddleCenter.x), newVelocity.y + (ballPos.y - paddleCenter.y));
                    //Add a reward to the AI if he hit the ball
                    PaddleAgent agent = col.gameObject.GetComponent<PaddleAgent>();
                    agent?.AddReward(5f);
                }
                else
                {
                    //Inverse the y direction to have some visual feedback
                    newVelocity.y = -newVelocity.y;
                }
            }

            //do not allow the ball to be static
            if(newVelocity.x < 0.2f && newVelocity.x > -0.2f)
            {
                newVelocity.x = velocity.x;
            }

            //apply the new velocity
            rb.velocity = newVelocity;
        }
    }
}
