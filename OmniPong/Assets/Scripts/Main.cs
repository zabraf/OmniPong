using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject PaddleJ1;
    public GameObject PaddleJ2;
    public GameObject Ball;
    public GameObject WallTop;
    // Start is called before the first frame update
    void Start()
    {
        PaddleJ1.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight / 2));
        PaddleJ1.transform.position = new Vector3(PaddleJ1.transform.position.x + PaddleJ1.GetComponent<SpriteRenderer>().bounds.size.x, PaddleJ1.transform.position.y, 10);
        PaddleJ2.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        PaddleJ2.transform.position = new Vector3(PaddleJ2.transform.position.x - PaddleJ2.GetComponent<SpriteRenderer>().bounds.size.x, PaddleJ2.transform.position.y, 10);
        Ball.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
