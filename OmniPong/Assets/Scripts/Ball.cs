using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 vitesse;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("StartBall", 1);
    }
    void StartBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            vitesse = new Vector2(20, -15);
            
        }
        else
        {
            vitesse =  new Vector2(-20, -15);
        }
        rb.AddForce(vitesse);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            rb.velocity = Vector3.zero;
            vitesse = new Vector2(vitesse.x,-vitesse.y);
            rb.AddForce(vitesse);
        } else if (col.gameObject.tag == "Goal")
        {
            RestartGame();
        }else if(col.gameObject.tag == "Player")
        {
            rb.velocity = Vector3.zero;
            if(vitesse.x < 0)
            {
                vitesse = new Vector2(-vitesse.x + 5, vitesse.y);
            }
            else
            {
                vitesse = new Vector2(-vitesse.x - 5, vitesse.y);
            }
          
            rb.AddForce(vitesse);

        }
    }
}
