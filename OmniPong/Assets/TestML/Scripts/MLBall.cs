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
        Debug.Log("Col : " + col.gameObject.tag);
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
            Vector2 newVelocity;
            if (velocity.x < 0)
            {
                newVelocity = new Vector2(-velocity.x + speedIncrease, velocity.y);
                newVelocity = new Vector2(newVelocity.x + (rb.transform.position.x - col.transform.position.x), newVelocity.y + (rb.transform.position.y - col.transform.position.y));
                rb.velocity = newVelocity;
            }
            else
            {
                newVelocity = new Vector2(-velocity.x - speedIncrease, velocity.y);
                newVelocity = new Vector2(newVelocity.x + (rb.transform.position.x - col.transform.position.x), newVelocity.y + (rb.transform.position.y - col.transform.position.y));
                rb.velocity = newVelocity;
            }
        }
    }
}
