//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : MonsterGeneration.cs
// Description : Generates a monster from the ground up. Starting from its torso, generates the limbs.
// Author : Ethan Velasco Uy
// Mail : ethan.uy@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGeneration : MonoBehaviour
{
    [Header("Path - Body part models")]
    string bodyPartsPath = "MonsterParts/";

    [Header("Prefabs - Core Body Parts")]
    public List<GameObject> torsoPrefabs;

    [Header("Prefabs - Limbs and Extra Parts")]
    public List<GameObject> armPrefabs;
    public List<GameObject> legPrefabs;
    public List<GameObject> headPrefabs;
    public List<GameObject> extraPrefabs;
    
    [Header("Body Parts")]
    public GameObject torso;

    public int RandomRange(int min, int max)
    {
        return Random.Range(min, max);
    }

    int FindPoints(Transform objToFindPoints)
    {
        return (objToFindPoints.childCount);
    }


    public IEnumerator SpawnLimbs(Transform limbPoints, List<GameObject> limbPrefab, Transform limbs, string partName)
    {
        //how many limbs spawn
        int limbCount = RandomRange(1, FindPoints(limbPoints) + 1);

        for (int i = 0; i < limbCount; i++)
        {
            
            //Spawn random limb from array "prefabs".
            int randomIndex = RandomRange(0,limbPrefab.Count);
            GameObject newLimb = Instantiate(limbPrefab[randomIndex]);
            newLimb.gameObject.name = partName;

            //randomly allocate limb to a spot
            int limbPointIndex;
            Transform limbPointSelected;
            
            while (true)
            {
                //initialize
                limbPointIndex = RandomRange(0, FindPoints(limbPoints));
                limbPointSelected = limbPoints.GetChild(limbPointIndex);

                if (!limbPointSelected.GetComponent<BodyPointScript>().occupied)
                {
                    //set arm point to occupied
                    limbPointSelected.GetComponent<BodyPointScript>().occupied = true;
                    break;
                }

                yield return null;
            }

            //set new limb parent to monster
            newLimb.transform.SetParent(limbs);

            //pos and rot limb
            newLimb.transform.localPosition = limbPointSelected.transform.localPosition;
            newLimb.transform.eulerAngles = limbPointSelected.transform.eulerAngles;
        }
    }

    public void GenerateTorso()
    {
        int randomTorsoIndex = RandomRange(0,torsoPrefabs.Count);
        torso = Instantiate(torsoPrefabs[randomTorsoIndex]);
        torso.gameObject.name = "Torso";
        torso.transform.position = gameObject.transform.position;
        torso.transform.SetParent(gameObject.transform);
    }

    public void CombineBodypartMesh(GameObject torsoBodyParts)
    {
        MeshFilter[] meshFilters = torsoBodyParts.GetComponentsInChildren<MeshFilter>();    // Get all body parts inside the torso.
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            //meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        this.transform.GetChild(0).GetComponent<MeshFilter>().mesh = new Mesh();
        this.transform.GetChild(0).GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    void Start()
    {   
        // STAGE 0) Assign monster name
        gameObject.name = GetComponent<MonsterDetails>().monsterName;

        // STAGE 1) Load ALL Body parts up into the array
        torsoPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>(bodyPartsPath + "Torso"));
        armPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>(bodyPartsPath + "Arms"));
        legPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>(bodyPartsPath + "Legs"));
        headPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>(bodyPartsPath + "Heads"));
        extraPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>(bodyPartsPath + "Extras"));

        // STAGE 2) Generate core parts
        GenerateTorso();    // Generate torso

        // STAGE 3) Generate derived parts
        Transform torsoBodyParts = torso.transform.Find("BodyParts");   // Define body parts
        Transform torsoBodypoints = torso.transform.Find("BodyPoints"); // Define body points
        //spawn arms
        StartCoroutine(SpawnLimbs(torsoBodypoints.Find("ArmPoints"), armPrefabs, torsoBodyParts, "Arm"));
        //spawn legs
        StartCoroutine(SpawnLimbs(torsoBodypoints.Find("LegPoints"), legPrefabs, torsoBodyParts, "Leg"));
        //spawn heads
        StartCoroutine(SpawnLimbs(torsoBodypoints.Find("HeadPoints"), headPrefabs, torsoBodyParts, "Head"));
        //spawn extra parts
        StartCoroutine(SpawnLimbs(torsoBodypoints.Find("ExtraPoints"), extraPrefabs, torsoBodyParts, "Extra"));

        // Combine meshes
        CombineBodypartMesh(torsoBodyParts.gameObject);
    }
}
