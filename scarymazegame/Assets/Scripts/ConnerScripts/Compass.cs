//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : Compass
// Description : The main function for Battleships.
// Author : Conner Hall
// Mail : conner.hall@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{

    public GameObject playertransform;
    public GameObject[] Spawnpoints;


    public Vector3 NorthDir;
    public Vector3[] distance;
    public Vector3[] direction;

    public GameObject Arrow;

    public Quaternion SpawnerQ;

    void Start()
    {
        
        playertransform = GameObject.Find("Player");
        Spawnpoints = GameObject.FindGameObjectsWithTag("Spawner");
        
        
        distance = new Vector3[Spawnpoints.Length];
        direction = new Vector3[Spawnpoints.Length];
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeNorth();
        if (Spawnpoints.Length > 0)
        {
            for (int i = 0; i< Spawnpoints.Length; i++)
            {
                distance[i].x = Mathf.Abs(playertransform.transform.position.x - Spawnpoints[i].transform.position.x);

            }
            if (distance[0].x < distance[1].x && distance[0].x < distance[2].x)
            {
                
                //Point to distance 0
                Vector3 Direction = Arrow.transform.position - Spawnpoints[0].transform.position;
                SpawnerQ = Quaternion.LookRotation(Direction);
                SpawnerQ.z = -SpawnerQ.y;
                SpawnerQ.x = 0;
                SpawnerQ.y = 0;

                Arrow.transform.localRotation = SpawnerQ * Quaternion.Euler(NorthDir);


            }
            if (distance[1].x < distance[0].x && distance[1].x < distance[2].x)
            {
                //Point to distance 1
                Vector3 Direction = Arrow.transform.position - Spawnpoints[0].transform.position;
                SpawnerQ = Quaternion.LookRotation(Direction);
                SpawnerQ.z = -SpawnerQ.y;
                SpawnerQ.x = 0;
                SpawnerQ.y = 0;

                Arrow.transform.localRotation = SpawnerQ * Quaternion.Euler(NorthDir);

            }
            if (distance[2].x < distance[0].x && distance[2].x < distance[1].x)
            {
                //Point to distance 2
                Vector3 Direction = Arrow.transform.position - Spawnpoints[0].transform.position;
                SpawnerQ = Quaternion.LookRotation(Direction);
                SpawnerQ.z = -SpawnerQ.y;
                SpawnerQ.x = 0;
                SpawnerQ.y = 0;

                Arrow.transform.localRotation = SpawnerQ * Quaternion.Euler(NorthDir);

            }




        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PointToClosest();
        }
        
    }
    void ChangeNorth()
    {
        NorthDir.z = (playertransform.transform.eulerAngles.y) - 90;
       

    }
    void ChangeCompassDirection()
    {
        //Vector3 Direction = Arrow.transform.position - direction[/*i*/].position;
        //SpawnerQ = Quaternion.LookRotation(Direction);
        //SpawnerQ.z = -SpawnerQ.y;
        //SpawnerQ.x = 0;
        //SpawnerQ.y = 0;

        //Arrow.localRotation = SpawnerQ * Quaternion.Euler(NorthDir);
    }

    void PointToClosest()
    {
        
    }
}
