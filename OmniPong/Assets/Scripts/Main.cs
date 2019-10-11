using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject PaddleJ1;
    public GameObject PaddleJ2;
    public GameObject Ball;
    public GameObject WallTop;
    public GameObject WallBot;
    public GameObject WallRight;
    public GameObject WallLeft;
    // Start is called before the first frame update
    void Start()
    {
        PaddleJ1.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight / 2));
        PaddleJ1.transform.position = new Vector3(PaddleJ1.transform.position.x + PaddleJ1.GetComponent<SpriteRenderer>().bounds.size.x, PaddleJ1.transform.position.y, 10);

        PaddleJ2.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        PaddleJ2.transform.position = new Vector3(PaddleJ2.transform.position.x - PaddleJ2.GetComponent<SpriteRenderer>().bounds.size.x, PaddleJ2.transform.position.y, 10);

        WallRight.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        WallRight.transform.localScale = new Vector3(1, Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight)).y / 1.2F, 1);
        WallRight.transform.position = new Vector3(WallRight.transform.position.x + WallRight.GetComponent<SpriteRenderer>().bounds.size.x / 2, WallRight.transform.position.y, 10);

        WallLeft.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight / 2));
        WallLeft.transform.localScale = new Vector3(1, Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight)).y / 1.2F, 1);
        WallLeft.transform.position = new Vector3(WallLeft.transform.position.x - WallLeft.GetComponent<SpriteRenderer>().bounds.size.x / 2, WallLeft.transform.position.y, 10);


        WallTop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight));
        WallTop.transform.localScale = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0)).x * 7, 0.1F, 1);
        WallTop.transform.position = new Vector3(WallTop.transform.position.x, WallTop.transform.position.y + WallTop.GetComponent<SpriteRenderer>().bounds.size.y / 2, 10);

        WallBot.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, 0));
        WallBot.transform.localScale = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0)).x * 7, 0.1F, 1);
        WallBot.transform.position = new Vector3(WallBot.transform.position.x, WallBot.transform.position.y - WallBot.GetComponent<SpriteRenderer>().bounds.size.y / 2, 10);

        Ball.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
