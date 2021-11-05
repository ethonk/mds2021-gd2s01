using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MerchantShop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Transform Slots;

    [Header("Shop")]
    public List<GameObject> ShopItems; 
    public int MaxShopSpace;
    public GameObject Item;


    // Unity Functions
    void Start()
    {
        print("allah");
        MaxShopSpace = Slots.transform.childCount;
        print(MaxShopSpace);

        for (int i = 0; i < MaxShopSpace; i++)
        {
            ShopItems.Add(null);
        }
    }
}


