using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject Selector;
    public Text PVA;
    public Text PVP;

    private bool isPVASelected;
    // Start is called before the first frame update
    void Start()
    {
        Selector.transform.position = new Vector2(-4.17f, PVA.transform.position.y); 
        StartCoroutine("Blink");
        isPVASelected = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Return))
        {
            if(isPVASelected)
            {
                //launche scene PVA
            }
            else
            {
                //launch scene PVP
            }
        }
        if(isPVASelected)
        {
            if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                isPVASelected = false;
                Selector.transform.position = new Vector2(-4.17f, PVP.transform.position.y);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                isPVASelected = true;
                Selector.transform.position = new Vector2(-4.17f, PVA.transform.position.y);
            }
        }
    }
    public IEnumerator Blink()
    {
        while (true)
        {
            if (Selector.activeSelf)
            {
                Selector.SetActive(false);
            }
            else
            {
                Selector.SetActive(true);
            }
            yield return new WaitForSeconds(0.5F);
        }
        
    }
}
