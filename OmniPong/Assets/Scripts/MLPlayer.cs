using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLPlayer : MonoBehaviour
{
    public Camera cam;
    public bool useControls = false;
    public KeyCode keyUp = KeyCode.W;
    public KeyCode keyDown = KeyCode.S;

    //between -1 and 1, 1 being up
    public float movingDirection = 0;

    public float speed = 2.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if using controls
        if (useControls)
        {
            //change the moving direction depending on the inputs
            if (Input.GetKey(keyUp))
                movingDirection = 1;
            else if (Input.GetKey(keyDown))
                movingDirection = -1;
            else
                movingDirection = 0;
        }
        float f = cam.ScreenToWorldPoint(new Vector3(0, 0, -10)).y;
        float Y = transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2;
        //change speed by multiplying by moving direction
        if (movingDirection == 1 && transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2 > cam.ScreenToWorldPoint(new Vector3(0,cam.pixelHeight,-10)).y)
        {
            movingDirection = 0;
        }
        if (movingDirection == -1 && transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2 < cam.ScreenToWorldPoint(new Vector3(0, 0, -10)).y)
        {
            movingDirection = 0;
        }
        rb.velocity = new Vector2(0, speed * movingDirection);
    }
}
