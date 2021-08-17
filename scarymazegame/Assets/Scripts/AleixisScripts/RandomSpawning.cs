using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    [Header("Variables")]
    float ElapsedTime = 0f;

    [Header("Serialized Fields")] //serializes the fields
    [SerializeField] private Transform Merchant;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private Transform DefaultSpawn;

    private void OnTriggerEnter(Collider other) // On trigger enter means when the script attached object hits the box collider of a trigger object.
    {
        if (other.CompareTag("Spawner")) //When asset hits trigger, the script compares the tag of the trigger to see if it is a spawner
        {
            Merchant.transform.position = SpawnPoint.transform.position; //Takes the position of the merchant then transforms it (changes it) to the transform of the selected position
            Physics.SyncTransforms(); //Syncs transforms so that nothing breaks, unity does a lot of the damage control
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spawner"))
        {
            ElapsedTime = 0;
            StartCoroutine(ElapsedTimer());

            if (ElapsedTime > 3)
            {
                Merchant.transform.position = DefaultSpawn.transform.position;
            }
        }
    }

    void Start()
    {
        Merchant.transform.position = DefaultSpawn.transform.position;
    }

    void Update()
    {
        Debug.Log(ElapsedTime);
    }

    private IEnumerator ElapsedTimer()
    {
        ElapsedTime += Time.deltaTime;

        yield return null;
    }
}
