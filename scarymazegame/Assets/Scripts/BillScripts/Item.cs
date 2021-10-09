using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : MonoBehaviour
{
    public Category ItemType;
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

}

public enum Category
{
    Weapon,
    Craftable,
    Tools,
    Resource

};

