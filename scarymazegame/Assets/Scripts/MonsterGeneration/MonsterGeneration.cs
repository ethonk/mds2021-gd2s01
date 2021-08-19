using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGeneration : MonoBehaviour
{
    [Header("Prefabs - Core Body Parts")]
    public GameObject[] torsoPrefabs;
    public GameObject[] pelvisPrefabs;
    public GameObject[] neckPrefabs;

    [Header("Prefabs - Limbs and Extra Parts")]
    public GameObject[] armPrefabs;
    public GameObject[] legPrefabs;
    public GameObject[] headPrefabs;
    
    [Header("Body Parts")]
    public GameObject torso;
    public GameObject pelvis;
    public GameObject neck;

    float RandomRange(float min, float max)
    {
        return Random.Range(min, max);
    }

    int FindPoints(Transform objToFindPoints)
    {
        return (objToFindPoints.childCount);
    }

    public IEnumerator SpawnLimbs(Transform limbPoints, GameObject[] limbPrefab, Transform limbs)
    {
        //how many limbs spawn
        int limbCount = (int) RandomRange(1f, FindPoints(limbPoints));

        for (int i = 0; i < limbCount; i++)
        {
            
            //Spawn random limb from array "prefabs".
            int randomIndex = (int) RandomRange(0,limbPrefab.Length);
            GameObject newLimb = Instantiate(limbPrefab[randomIndex]);

            //randomly allocate limb to a spot
            int limbPointIndex;
            Transform limbPointSelected;

            while (true)
            {
                //initialize
                limbPointIndex = (int) RandomRange(0f, FindPoints(limbPoints));
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
        int randomTorsoIndex = (int) RandomRange(0,torsoPrefabs.Length);
        torso = Instantiate(torsoPrefabs[randomTorsoIndex]);
        torso.transform.position = gameObject.transform.position;
        torso.transform.SetParent(gameObject.transform);
    }

    public void GeneratePelvis(Transform pelvisPoints)
    {
        int randomPelvisIndex = (int) RandomRange(0,pelvisPrefabs.Length);
        pelvis = Instantiate(pelvisPrefabs[randomPelvisIndex]);
        pelvis.transform.SetParent(gameObject.transform);

        //set position to random point of torso
        //randomly allocate pelvis to a spot
        int pelvisPointIndex;
        Transform pelvisPointSelected;

        //initialize
        pelvisPointIndex = (int) RandomRange(0f, FindPoints(pelvisPoints));
        pelvisPointSelected = pelvisPoints.GetChild(pelvisPointIndex);

        //set new pelvis parent to monster
        pelvis.transform.SetParent(torso.transform.parent);

        //pos and rot limb
        pelvis.transform.position = pelvisPointSelected.transform.position;
        pelvis.transform.eulerAngles = pelvisPointSelected.transform.eulerAngles;
    }

    public void GenerateNeck(Transform neckPoints)
    {
        int randomNeckIndex = (int) RandomRange(0,neckPrefabs.Length);
        neck = Instantiate(neckPrefabs[randomNeckIndex]);
        neck.transform.SetParent(gameObject.transform);

        //set position to random point of torso
        //randomly allocate neck to a spot
        int neckPointIndex;
        Transform neckPointSelected;

        //initialize
        neckPointIndex = (int) RandomRange(0f, FindPoints(neckPoints));
        neckPointSelected = neckPoints.GetChild(neckPointIndex);

        //set new neck parent to monster
        pelvis.transform.SetParent(torso.transform.parent);

        //pos and rot limb
        neck.transform.position = neckPointSelected.transform.position;
        neck.transform.eulerAngles = neckPointSelected.transform.eulerAngles;
    }


    // Start is called before the first frame update
    void Start()
    {        
        //generate torso
        GenerateTorso();
        //generate pelvis
        GeneratePelvis(torso.transform.Find("PelvisPoints"));
        GenerateNeck(torso.transform.Find("NeckPoints"));

        //spawn arms
        StartCoroutine(SpawnLimbs(torso.transform.Find("ArmPoints"), armPrefabs, torso.transform.Find("Arms").transform));
        //spawn legs
        StartCoroutine(SpawnLimbs(pelvis.transform.Find("LegPoints"), legPrefabs, pelvis.transform.Find("Legs").transform));
        //spawn heads
        StartCoroutine(SpawnLimbs(neck.transform.Find("HeadPoints"), headPrefabs, neck.transform.Find("Heads").transform));

    }
}
