//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : MerchantShop.cs
// Description : main script for handling the merchant shop in the game
// Author : Aliexis Alvarez
// Mail : Aliexis.Alvarez@mediadesignschool.com
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MerchantShop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Transform Slots;
    [SerializeField] public GlobalInventory m_MerchantInv;

    [Header("Player references")]
    [SerializeField] public GameObject m_Player;
    [SerializeField] public GlobalInventory m_PlayerInv;

    [Header("Shop")]
    private List<GameObject> PlayerInventory;
    private int MaxShopSpace;
    public List<GameObject> Items;

    [Header("Items")]
    public GameObject Item0;
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


    // Unity Functions
    void Start()
    {
        m_Player = GameObject.Find("Player");
        m_PlayerInv = m_Player.GetComponent<GlobalInventory>();

        // find the max shop space in merchant inventory
        MaxShopSpace = Slots.transform.childCount; 

        // add the items onto the items list
        Items.Add(Item0);
        Items.Add(Item1);
        Items.Add(Item2);
        Items.Add(Item3);
        Items.Add(Item4);
        Items.Add(Item5);
        Items.Add(Item6);
        Items.Add(Item7);
        Items.Add(Item8);
        Items.Add(Item9);
        Items.Add(Item10);
        Items.Add(Item11);
        
        // add gameObject items to merchant backpack
        foreach (GameObject obj in Items)
        {
            for ( int i = 0; i < obj.GetComponent<ItemScript>().maxStack; i++)
            {
                if (obj != null)
                {
                    m_MerchantInv.AddItem(obj);
                }
                else
                {
                    print("No gameObject!");
                }
            }
        } 
    }
}


