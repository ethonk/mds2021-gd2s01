using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGeneration : MonoBehaviour
{
    [Header("Unsorted")]
    public GameObject arm;
    public int armCount;

    [Header("Body Points")]
    public GameObject armPoints;
    
    [Header("Body Parts")]
    public GameObject arms;

    float RandomRange(float min, float max)
    {
        return Random.Range(min, max);
    }

    int FindPoints(GameObject objToFindPoints)
    {
        return (objToFindPoints.transform.childCount);
    }

    public IEnumerator SpawnArms()
    {
        //how many arms spawn
        armCount = (int) RandomRange(1f, FindPoints(armPoints));

        for (int i = 0; i < armCount; i++)
        {
            
            //spawn arm
            GameObject newArm = Instantiate(arm);

            //randomly allocate arm to a spot
            int armPointIndex;
            Transform armPointSelected;

            while (true)
            {
                //initialize
                armPointIndex = (int) RandomRange(0f, FindPoints(armPoints));
                armPointSelected = armPoints.transform.GetChild(armPointIndex);

                if (!armPointSelected.GetComponent<BodyPointScript>().occupied)
                {
                    //set arm point to occupied
                    armPointSelected.GetComponent<BodyPointScript>().occupied = true;
                    break;
                }

                yield return null;
            }

            //set new arm parent to monster
            newArm.transform.SetParent(arms.transform);

            //pos and rot arm arm
            newArm.transform.localPosition = armPointSelected.transform.localPosition;
            newArm.transform.eulerAngles = armPointSelected.transform.eulerAngles;
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
        //spawn arms
        StartCoroutine(SpawnArms());

    }
}
