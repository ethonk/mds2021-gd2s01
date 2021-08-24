using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthanMouseLook : MonoBehaviour
{
    public Transform p_Model;

    public float m_Sensitivity = 200.0f;

    float xRotation = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        #region view control
        
        float mouseX = Input.GetAxis("Mouse X")*m_Sensitivity*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")*m_Sensitivity*Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f); //max rotation is 90 degrees up and down

        //rotate
        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        p_Model.Rotate(Vector3.up*mouseX);
        #endregion
    }
}