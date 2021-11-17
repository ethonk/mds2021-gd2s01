//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : MonsterDetails.cs
// Description : Generates the stats and properties of each monster.
// Author : Ethan Velasco Uy
// Mail : ethan.uy@mediadesignschool.com
//

using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetails : MonoBehaviour
{
    // Types of monster
    public enum Types {Steel = 1, Poison = 2,  Grass = 3, Water = 4, Electric = 5, Rock = 6, Ghost = 7};
    [Header("Monster Details")]
    public string monsterName = "unknown";
    public Types monsterType;
    public List<string> nameSyllables; // Monster name syllables

    [Header("Monster Stats")]
    public float health;
    public float speed;
    public float currentSpeed;

    [Header("Audio")]
    public AudioClip jsSound;
    public AudioClip eatSound;
    public AudioClip spitSound;
    public AudioClip slashSound;
    
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
        //LoadNames();    // Load index of name syllables.
        //monsterName = GenerateName();   // Generate monster name

        currentSpeed = speed;   // Initialize speed
    }
}
