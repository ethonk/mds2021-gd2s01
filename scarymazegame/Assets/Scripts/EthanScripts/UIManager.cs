using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    public PlayerScript player;
    public Camera inventoryCamera;

    [Header("UI")]
    public GameObject crosshair;
    public GameObject itemDetails;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public GameObject bind_equip;
    public GameObject bind_consume;
    public GameObject bind_drop;

    [Header("Values")]
    public Vector3 keybindStartPos = new Vector3(0, 5, 0);
    public float keybindButtonOffset = -26.0f;

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

        int i = 0;
        
        // Algorithm for item
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

    // Update is called once per frame
    void Update()
    {
        CursorToMouse();    // Update cursor pos
        
        // Raycasting items
        if(player.cameraState == PlayerScript.CameraState.inventory)
        {
            RaycastHit hit;
            Ray ray = inventoryCamera.ScreenPointToRay(Input.mousePosition);

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
}
