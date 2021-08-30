using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Start is called before the first frame update
    //void Start()
    //{

    //}

    //Update is called once per frame
    //void Update()
    //{

    //}
    public GameObject Canvas;
    public GameObject Panel;
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Settings()
    {
        Panel.SetActive(true);
        Canvas.SetActive(false);
    }

    public void Credits()
    {

    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit!!!");
    }

    public void Return()
    {
        Panel.SetActive(false);
        Canvas.SetActive(true);

    }
}
