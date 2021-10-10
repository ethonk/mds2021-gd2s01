using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene("Game");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeScene());
    }
}
