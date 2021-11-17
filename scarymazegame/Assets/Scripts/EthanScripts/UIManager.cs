//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : UIManager.cs
// Description : Manages all things UI in the game.
// Author : Ethan Velasco Uy
// Mail : ethan.uy@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    private PlayerScript player;
    private GameObject playerObj;
    private Camera inventoryCamera;
    private Camera merchantCamera;

    [Header("Merchant")]
    private GameObject merchantObj;
    private MerchantShop merchant;

    [Header("UI")]
    public GameObject crosshair;
    public GameObject itemDetails;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemCount;
    public GameObject bind_equip;
    public GameObject bind_consume;
    public GameObject bind_drop;
    public GameObject bind_craft;
    public TextMeshProUGUI monsters_caught;

    [Header("Values")]
    public Vector3 keybindStartPos = new Vector3(0, 5, 0);
    public float keybindButtonOffset = -26.0f;


    void Start()
    {
        // Initialize Player
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        playerObj = GameObject.Find("Player");
        inventoryCamera = player.GetComponent<GlobalInventory>().backpackSlotContainer.transform.parent.Find("InventoryCamera").GetComponent<Camera>();

        // Initialize Merchant
        merchant = GameObject.Find("Merchant").GetComponent<MerchantShop>();
        merchantObj = GameObject.Find("Merchant");
        merchantCamera = merchantObj.GetComponent<GlobalInventory>().backpackSlotContainer.transform.parent.Find("MerchantCamera").GetComponent<Camera>();
    }

    public void CursorToMouse() // Move the mouse and anything attached to it towards the cursor.
    {
        if (player.cameraState != PlayerScript.CameraState.normal)
        {
            crosshair.transform.position = Input.mousePosition;
            itemDetails.transform.position = Input.mousePosition;
        }
        else
        {
            crosshair.transform.localPosition = new Vector3(0, 0, 0);
            itemDetails.transform.position = Input.mousePosition;
        }
    }

    public void InspectItem(ItemScript item)
    {
        // Set name and description
        itemName.text = item.itemName;
        if (player.cameraState == PlayerScript.CameraState.shop && item.canBe_crafted)
        {
            itemDescription.text = item.itemCraftReqs;
        }
        else
        {
            itemDescription.text = item.itemDescription;
        }

        for (int x = 0; x < player.GetComponent<GlobalInventory>().backpack.Count; x++)
        {
            if (player.GetComponent<GlobalInventory>().backpack[x] != null)
            {
                if (player.GetComponent<GlobalInventory>().backpack[x].GetComponent<ItemScript>().itemName == item.itemName)
                {
                    itemCount.text = player.GetComponent<GlobalInventory>().backpackItemCount[x].ToString();
                }
            }
        }

        int i = 0;
        
        #region Algorithm for item
        if (player.cameraState != PlayerScript.CameraState.shop)
        {
            if(item.canBe_equipped)
            {
                bind_equip.SetActive(true);
                bind_equip.transform.localPosition = keybindStartPos + new Vector3(0, ((keybindButtonOffset - 10) * i), 0);
                i += 1;
            }
            else
            {
                bind_equip.SetActive(false);
            }

            if(item.canBe_consumed)
            {
                bind_consume.SetActive(true);
                bind_consume.transform.localPosition = keybindStartPos + new Vector3(0, ((keybindButtonOffset - 10) * i), 0);
                i += 1;
            }
            else
            {
                bind_consume.SetActive(false);
            }

            if(item.canBe_dropped)
            {
                bind_drop.SetActive(true);
                bind_drop.transform.localPosition = keybindStartPos + new Vector3(0, ((keybindButtonOffset - 10) * i), 0);
                i += 1;
            }
            else
            {
                bind_drop.SetActive(false);
            }
        }
        if (item.canBe_crafted && player.cameraState == PlayerScript.CameraState.shop)
        {
            bind_drop.SetActive(false);
            bind_consume.SetActive(false);
            bind_equip.SetActive(false);

            bind_craft.SetActive(true);
            bind_craft.transform.localPosition = keybindStartPos + new Vector3(0, 0, 0);
        }
        else
        {
            bind_craft.SetActive(false);
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        CursorToMouse();    // Update cursor pos
        
        #region Raycasting items

        #region Player Inventory
        if(player.cameraState == PlayerScript.CameraState.inventory)   
        {
            // Raycast settings
            RaycastHit hit;
            Ray ray = inventoryCamera.ScreenPointToRay(Input.mousePosition);

            // UI Functionality
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.GetComponent<ItemScript>() != null)
                {
                    itemDetails.gameObject.SetActive(true);
                    InspectItem(hit.transform.gameObject.GetComponent<ItemScript>());
                    
                    // Key press - Equip
                    if (Input.GetKeyDown(KeyCode.E) && hit.transform.gameObject.GetComponent<ItemScript>().canBe_equipped)  // equip
                    {
                        GameObject newItem = Instantiate(hit.transform.gameObject);
                        newItem.GetComponent<Pickup>().PickUp();
                        player.GetComponent<GlobalInventory>().DropItem(hit.transform.parent.name);
                    }

                    // Key press - Drop
                    if (Input.GetKeyDown(KeyCode.Q) && hit.transform.gameObject.GetComponent<ItemScript>().canBe_dropped) //drop
                    {
                        player.GetComponent<GlobalInventory>().DropItem(hit.transform.parent.name);
                    }
                }
                else
                {
                    itemDetails.gameObject.SetActive(false);
                }
            }
        }
        #endregion

        #region Merchant Inventory
        if (player.cameraState == PlayerScript.CameraState.shop)
        {
            // Raycast settings
            RaycastHit _hit;
            Ray _ray = merchantCamera.ScreenPointToRay(Input.mousePosition);

            // UI Functionality
            if(Physics.Raycast(_ray, out _hit))
            {
                if(_hit.transform.GetComponent<ItemScript>() != null)
                {
                    itemDetails.gameObject.SetActive(true);
                    InspectItem(_hit.transform.gameObject.GetComponent<ItemScript>());

                    if (Input.GetKeyDown(KeyCode.R) && _hit.transform.gameObject.GetComponent<ItemScript>().canBe_crafted)
                    {
                        _hit.transform.gameObject.GetComponent<ItemScript>().Craft(playerObj.GetComponent<GlobalInventory>());
                        print("crafting finished");
                    }
                }
                else
                {
                    itemDetails.gameObject.SetActive(false);
                }
            }
        }

        if (player.cameraState == PlayerScript.CameraState.normal)
        {
            itemDetails.gameObject.SetActive(false);
        }
        #endregion
        #endregion
    
        #region Update monsters caught
        monsters_caught.text = "Monsters Caught: " + player.MonsterCount + "/7";
        #endregion
    }
}