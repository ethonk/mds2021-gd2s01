using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Chest,
    Boots,
    Weapon,
    Accessory,
}

[CreateAssetMenu]
public class EquippableItems : Item
{
    public int Stat1;
    public int Stat2;
    public int Stat3;
    public int Stat4;
    [Space]
    public float Stat1Percentage;
    public float Stat2Percentage;
    public float Stat3Percentage;
    public float Stat4Percentage;
    [Space]
    public EquipmentType EquipmentType;
}
