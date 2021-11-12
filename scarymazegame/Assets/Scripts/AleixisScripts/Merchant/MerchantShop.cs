using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MerchantShop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Transform Slots;
    [SerializeField] public GlobalInventory m_MerchantInv;
    [SerializeField] public GlobalInventory m_PlayerInv;

    [Header("Shop")]
    private List<GameObject> PlayerInventory;
    private int MaxShopSpace;
    public List<GameObject> Items;

    [Header("Items")]
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    public GameObject Item4;
    public GameObject Item5;
    public GameObject Item6;
    public GameObject Item7;
    public GameObject Item8;
    public GameObject Item9;
    public GameObject Item10;
    public GameObject Item11;
    public GameObject Item12;


    // Unity Functions
    void Start()
    {
        print("allah");

        // find the max shop space in merchant inventory
        MaxShopSpace = Slots.transform.childCount; 

        // add the items onto the items list
        Items.Add(Item1);
        
        // add gameObject items to merchant backpack
        foreach (GameObject obj in Items)
        {
            for ( int i = 0; i < obj.GetComponent<ItemScript>().maxStack; i++)
            {
                m_MerchantInv.AddItem(obj);
            }
        }
    }

    void Update()
    {  

    }
}


