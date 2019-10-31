using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLBall : MonoBehaviour
{
    public GameController gameController;
    private Rigidbody2D rb;
    public float speedIncrease = 2.5f;
    //used to keep the data of velocity before collision
    private Vector2 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            if(velocity.x < 0)
            {
                rb.velocity = new Vector2(-velocity.x + speedIncrease, velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-velocity.x - speedIncrease, velocity.y);
            }
        }
    }
}
