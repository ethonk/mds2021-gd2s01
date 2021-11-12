using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    private PlayerScript player;
    private Camera inventoryCamera;

    [Header("Merchant")]
    public MerchantShop merchant;
    private Camera merchantInventoryCamera;

    [Header("UI")]
    public GameObject crosshair;
    public GameObject itemDetails;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemCount;
    public GameObject bind_equip;
    public GameObject bind_consume;
    public GameObject bind_drop;

    [Header("Values")]
    public Vector3 keybindStartPos = new Vector3(0, 5, 0);
    public float keybindButtonOffset = -26.0f;


    void Start()
    {
        // Initialize Player
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        inventoryCamera = player.GetComponent<GlobalInventory>().backpackSlotContainer.transform.parent.Find("InventoryCamera").GetComponent<Camera>();

        // Initialize Merchant
        merchant.transform.Find("InventoryCamera").GetComponent<Camera>();
    }

    public void CursorToMouse() // Move the mouse and anything attached to it towards the cursor.
    {
        if (player.cameraState == PlayerScript.CameraState.inventory)
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
        itemDescription.text = item.itemDescription;

        // Set get slot of item
        int itemSlot = (int)char.GetNumericValue(item.transform.parent.name[item.transform.parent.name.Length-1]);
        // Set item count with slot
        itemCount.text = player.GetComponent<GlobalInventory>().backpackItemCount[itemSlot].ToString();

        int i = 0;
        
        #region Algorithm for item
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
                    {
                        itemDetails.gameObject.SetActive(false);
                    }
                }
            }
        }
        #endregion

        #region Merchant Inventory
        if(player.cameraState == PlayerScript.CameraState.shop)
        {
            // Raycast settings
            RaycastHit hit;
            Ray ray = merchantInventoryCamera.ScreenPointToRay(Input.mousePosition);

            // UI Functionality
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.GetComponent<ItemScript>() != null)
                {
                    itemDetails.gameObject.SetActive(true);
                    InspectItem(hit.transform.gameObject.GetComponent<ItemScript>());
                }
                else
                {
                    {
                        itemDetails.gameObject.SetActive(false);
                    }
                }
            }
            }
        }
        #endregion

        else
        {
            itemDetails.gameObject.SetActive(false);
        }
        #endregion
    }
}
