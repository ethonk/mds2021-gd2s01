using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public IEnumerator Flicker()
    {
        yield return new WaitForSeconds(5.0f);
        GetComponent<Light>().intensity = 0;
        transform.parent.Find("Lamp").GetComponent<MeshRenderer>().enabled = false;
    
        StartCoroutine(UnFlicker());
    }

    public IEnumerator UnFlicker()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Light>().intensity = 10;
        transform.parent.Find("Lamp").GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(Flicker());
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flicker());
    }
}
