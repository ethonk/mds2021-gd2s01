using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetails : MonoBehaviour
{
    public string monsterName = "unknown";
    public string monsterType = "unknown";


    public List<string> nameSyllables; // Monster name syllables

    
    public void LoadNames()
    { 
        var stream = new StreamReader("Assets/Resources/MonsterData/nameSyllables.txt");
        
        while (!stream.EndOfStream)
        {
            nameSyllables.Add(stream.ReadLine());
        }
    }

    void Start()
    {
        LoadNames();    // Load index of name syllables.
    }
}
