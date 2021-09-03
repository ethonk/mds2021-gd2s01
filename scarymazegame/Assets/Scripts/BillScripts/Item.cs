using UnityEngine;


[CreateAssetMenu]                       //Allows us to create an item asset under the create tab -Needed for when you use ScriptableObject
public class Item : ScriptableObject    //^^^
{
    public string ItemName;
    public Sprite Icon; 

}

