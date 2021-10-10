using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetails : MonoBehaviour
{
    [Header("Monster Details")]
    public string monsterName = "unknown";
    public string monsterType = "unknown";
    public List<string> nameSyllables; // Monster name syllables

    [Header("Audio")]
    public AudioClip eatSound;
    public AudioClip spitSound;
    
    public void LoadNames()
    { 
        var stream = new StreamReader("Assets/Resources/MonsterData/nameSyllables.txt");
        
        while (!stream.EndOfStream)
        {
            nameSyllables.Add(stream.ReadLine());
        }
    }

    string GenerateName()
    {
        string name1 = nameSyllables[GetComponent<MonsterGeneration>().RandomRange(0, nameSyllables.Count)];
        string name2 = nameSyllables[GetComponent<MonsterGeneration>().RandomRange(0, nameSyllables.Count)];
        string name3 = nameSyllables[GetComponent<MonsterGeneration>().RandomRange(0, nameSyllables.Count)];
        
        string finalName = name1 + name2 + name3;

        GetComponent<MonsterGeneration>().name = finalName;
        return finalName;
    }

    void Start()
    {
        LoadNames();    // Load index of name syllables.
        monsterName = GenerateName();   // Generate monster name
    }
}
