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

    int RandomRange(int min, int max)
    {
        return Random.Range(min, max);
    }

    int FindPoints(Transform objToFindPoints)
    {
        return (objToFindPoints.childCount);
    }


    public IEnumerator SpawnLimbs(Transform limbPoints, List<GameObject> limbPrefab, Transform limbs)
    {
        //how many limbs spawn
        int limbCount = RandomRange(1, FindPoints(limbPoints) + 1);
        print(limbPoints.gameObject.name + ". Random (1 - " + FindPoints(limbPoints) + "): "  + limbCount + " limbs.");

        for (int i = 0; i < limbCount; i++)
        {
            
            //Spawn random limb from array "prefabs".
            int randomIndex = RandomRange(0,limbPrefab.Count);
            GameObject newLimb = Instantiate(limbPrefab[randomIndex]);

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
        torso.transform.position = gameObject.transform.position;
        torso.transform.SetParent(gameObject.transform);
    }


    void Start()
    {     
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
        StartCoroutine(SpawnLimbs(torsoBodypoints.Find("ArmPoints"), armPrefabs, torsoBodyParts.Find("Arms").transform));
        //spawn legs
        StartCoroutine(SpawnLimbs(torsoBodypoints.Find("LegPoints"), legPrefabs, torsoBodyParts.Find("Legs").transform));
        //spawn heads
        StartCoroutine(SpawnLimbs(torsoBodypoints.Find("HeadPoints"), headPrefabs, torsoBodyParts.Find("Heads").transform));
        //spawn extra parts
        StartCoroutine(SpawnLimbs(torsoBodypoints.Find("ExtraPoints"), extraPrefabs, torsoBodyParts.Find("Extras").transform));

    }
}
