using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGeneration : MonoBehaviour
{
    public GameObject arm;

    public int armCount;

    float RandomRange(float min, float max)
    {
        return Random.Range(min, max);
    }

    int FindArmPoints()
    {
        return (gameObject.transform.Find("ArmPoints").transform.childCount);
    }

    void SpawnArms()
    {
        for (int i = 0; i < armCount; i++)
        {
            //spawn arm
            GameObject newArm = Instantiate(arm);
            newArm.transform.SetParent(gameObject.transform.Find("Arms"));
            print("arm " + i + " spawned");

            //pos and rot arm arm
            newArm.transform.localPosition = gameObject.transform.Find("ArmPoints").transform.GetChild(i).transform.localPosition;
            newArm.transform.eulerAngles = gameObject.transform.Find("ArmPoints").transform.GetChild(i).transform.eulerAngles;
        }
    }

    // Start is called before the first frame update
    void Update()
    {        
        //how many arms spawn
        armCount = 5;//(int) RandomRange(1f, FindArmPoints());

        //spawn arms
        SpawnArms();

    }
}
