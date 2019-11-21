using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public GameObject Selector;
    public GameObject Selector2;
    public Text PVA;
    public Text PVP;
    public Text LVL1;
    public Text LVL2;
    public Text LVL3;
    public AudioSource audio;


    private Selection selected;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        Selector.transform.position = new Vector2(-4.17f, PVP.transform.position.y);
        StartCoroutine("Blink");
        selected = Selection.PVP;
    }
    enum Selection
    {
        PVP,
        PVA,
        PVS1,
        PVS2,
        PVS3
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        switch (selected)
        {
            case Selection.PVP:
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    audio.Play();
                    selected = Selection.PVA;
                    Selector.transform.position = new Vector2(-4.17f, PVA.transform.position.y + 0.05f);
                }
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Alpha8))
                {
                    audio.Play();
                    SceneManager.LoadScene("PVP");
                }
                break;
            case Selection.PVA:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    audio.Play();
                    selected = Selection.PVP;
                    Selector.transform.position = new Vector2(-4.17f, PVP.transform.position.y + +0.05f);
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    audio.Play();
                    selected = Selection.PVS1;
                    Selector2.transform.position = new Vector2(-3.19f, LVL1.transform.position.y + 0.05f);
                    Selector2.SetActive(true);
                    Selector.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Alpha8))
                {
                    audio.Play();
                    PlayerPrefs.SetInt("Switch", 1);
                    PlayerPrefs.SetInt("Level", 1);
                    SceneManager.LoadScene("Level1");
                }
                break;
            case Selection.PVS1:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    audio.Play();
                    selected = Selection.PVA;
                    Selector2.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    audio.Play();
                    selected = Selection.PVS2;
                    Selector2.transform.position = new Vector2(-3.19f, LVL2.transform.position.y + 0.05f);
                    Selector2.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Alpha8))
                {
                    audio.Play();
                    PlayerPrefs.SetInt("Switch", 0);
                    PlayerPrefs.SetInt("Level", 1);
                    SceneManager.LoadScene("Level1");
                    
                }
                break;
            case Selection.PVS2:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    audio.Play();
                    selected = Selection.PVS1;
                    Selector2.transform.position = new Vector2(-3.19f, LVL1.transform.position.y + 0.05f);
                    Selector2.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    audio.Play();
                    selected = Selection.PVS3;
                    Selector2.transform.position = new Vector2(-3.19f, LVL3.transform.position.y + 0.05f);
                    Selector2.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Alpha8))
                {
                    audio.Play();
                    PlayerPrefs.SetInt("Switch", 0);
                    PlayerPrefs.SetInt("Level", 2);
                    SceneManager.LoadScene("Level2");
                    
                }
                break;
            case Selection.PVS3:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    audio.Play();
                    selected = Selection.PVS2;
                    Selector2.transform.position = new Vector2(-3.19f, LVL2.transform.position.y + 0.05f);
                    Selector2.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Alpha8))
                {
                    audio.Play();
                    PlayerPrefs.SetInt("Switch", 0);
                    PlayerPrefs.SetInt("Level", 3);
                    SceneManager.LoadScene("Level3");
                    
                }
                break;
            default:
                break;
        }

    }
    public IEnumerator Blink()
    {
        while (true)
        {
            if (selected == Selection.PVS1 || selected == Selection.PVS2 || selected == Selection.PVS3)
            {
                if (Selector2.activeSelf)
                {
                    Selector2.SetActive(false);
                }
                else
                {
                    Selector2.SetActive(true);
                }
            }
            else
            {
                if (Selector.activeSelf)
                {
                    Selector.SetActive(false);
                }
                else
                { 
                    Selector.SetActive(true);
                }
            }
            yield return new WaitForSeconds(0.5F);
        }

    }
}
