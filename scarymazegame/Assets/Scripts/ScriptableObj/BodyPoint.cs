using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Body Point", menuName = "Monster Parts/Body Point")]
public class BodyPoint : ScriptableObject
{
    public string jointName;
    public bool occupied = false;
}
