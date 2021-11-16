using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billMouseLook : MonoBehaviour
{
    public float m_Spin = 0.0f;
    public float m_Tilt = 0.0f;
    public float m_Lean = 0.0f;

    public float m_Sensitivity = 2.0f;
    public Vector2 m_TiltExtents = new Vector2(-85.0f, 85.0f);

    public bool m_CursorLocked = true;

    void LockCursor()
    {
        Cursor.visible = !m_CursorLocked; //if visible, not visible and vice versa.
        Cursor.lockState = m_CursorLocked ? CursorLockMode.Locked : CursorLockMode.None; //bool ? if,true : if,false
    }

    void Start()
    {
        LockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_CursorLocked = !m_CursorLocked;
            LockCursor();
        }
        if (!m_CursorLocked)
        {
            return;
        }

        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        m_Spin += x * m_Sensitivity;
        m_Tilt -= y * m_Sensitivity;

        m_Tilt = Mathf.Clamp(m_Tilt, m_TiltExtents.x, m_TiltExtents.y); //Clamp = gives it a max and min limit

        transform.localEulerAngles = new Vector3(m_Tilt, m_Spin, m_Lean);
    }
}