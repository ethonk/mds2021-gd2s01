using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{

    public Transform playertransform;
    public GameObject[] Spawnpoints;
    public float closestDistance;
    public float Distance;
    public float[] distance;
    
    void Start()
    {
        
        playertransform = GameObject.FindWithTag("Player").transform;
        Spawnpoints = GameObject.FindGameObjectsWithTag("Spawner");
        closestDistance = 100.0f;
        distance = new float[Spawnpoints.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawnpoints.Length > 0)
        {
            for (int i = 0; i< Spawnpoints.Length; i++)
            {
                distance[i] = Vector3.Distance(playertransform.position, Spawnpoints[i].transform.position);
            }
            

           


        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PointToClosest();
        }
        
    }


    void PointToClosest()
    {
        Debug.Log(closestDistance);
    }
}
