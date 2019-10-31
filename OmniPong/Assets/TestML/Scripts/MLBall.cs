using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLBall : MonoBehaviour
{
    private GameController gameController;
    private Rigidbody2D rb;
    public float speedIncrease = 2.5f;
    //used to keep the data of velocity before collision
    private Vector2 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = Camera.main.GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        velocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Wall")
        {
            rb.velocity = new Vector2(velocity.x,-velocity.y);
        } 
        else if (col.gameObject.tag == "Goal")
        {
            gameController.ScoreGoal(col.gameObject);
        }
        else if(col.gameObject.tag == "Player")
        {
            Vector2 newVelocity = velocity;
            Vector2 ballPos = rb.transform.position;
            Vector2 paddleCenter = col.transform.position;
            
            if (velocity.x < 0)
            {
                paddleCenter.x += col.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;

                if (ballPos.x > paddleCenter.x)
                {
                    newVelocity = new Vector2(-newVelocity.x + speedIncrease, newVelocity.y);
                    newVelocity = new Vector2(newVelocity.x + (ballPos.x - paddleCenter.x), newVelocity.y + (ballPos.y - paddleCenter.y));
                }
                else
                {
                    newVelocity.y = -newVelocity.y;
                }
                rb.velocity = newVelocity;
            }
            else
            {
                paddleCenter.x -= col.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;

                if (ballPos.x < paddleCenter.x)
                {
                    newVelocity = new Vector2(-velocity.x - speedIncrease, velocity.y);
                    newVelocity = new Vector2(newVelocity.x + (ballPos.x - paddleCenter.x), newVelocity.y + (ballPos.y - paddleCenter.y));
                }
                else
                {
                    newVelocity.y = -newVelocity.y;
                }
            
                rb.velocity = newVelocity;
            }
        }
    }
}
