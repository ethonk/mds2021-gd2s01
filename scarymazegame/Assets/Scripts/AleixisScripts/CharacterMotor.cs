using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
   [Header("Attached Components")]
    public CharacterController m_Controller;
    public MouseLook m_Look;

    [Header("Motion Values")]
    public float m_MoveSpeed = 6.0f;
    public float m_Gravity = 32.0f;
    public float m_JumpSpeed = 10.0f;
    public float m_SprintSpeed = 2.0f;

    [Header("Current State")]
    public Vector3 m_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 inputMove;
    public bool m_Grounded = false;
    public bool isSprinting = false;

    [Header("Interactive Variables")]
    public float Range = 100f;
    public bool playerLock = false;

    void Update()
    {
        if (!playerLock)
        {
            float x = 0.0f;
            if (Input.GetKey(KeyCode.A))
            {
                x -= 1.0f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                x += 1.0f;
            }

            float z = 0.0f;
            if (Input.GetKey(KeyCode.S))
            {
                z -= 1.0f;
            }
            if (Input.GetKey(KeyCode.W))
            {
                z += 1.0f;
            }   
            
            if (m_Grounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    m_Velocity.y = m_JumpSpeed;
                }
            }
            
            Vector3 inputMove = new Vector3(x,0.0f, z);
            inputMove = Quaternion.Euler(0.0f,m_Look.m_Spin,0.0f) * inputMove;

            m_Velocity.x = inputMove.x * m_MoveSpeed;
            m_Velocity.y -= m_Gravity * Time.deltaTime;
            m_Velocity.z = inputMove.z * m_MoveSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (GetComponent<PlayerScript>().stamina > 0.0f)
                {
                    m_Velocity.x = inputMove.x * m_MoveSpeed * m_SprintSpeed;
                    m_Velocity.z = inputMove.z * m_MoveSpeed * m_SprintSpeed;
                    isSprinting = true;
                }
            }
            else
            {
                isSprinting = false;
            }
            
            m_Controller.Move(m_Velocity * Time.deltaTime);

            if ((m_Controller.collisionFlags & CollisionFlags.Below) != 0)
            {
                m_Velocity.y = -1.0f;
                m_Grounded = true;
            }
            else
            {
                m_Grounded = false;
            }

            if((m_Controller.collisionFlags & CollisionFlags.Above) != 0)
            {
                m_Velocity.y = -1.0f;
            }

            if((m_Controller.collisionFlags & CollisionFlags.Sides) != 0)
            {
                m_Velocity.x = 0.0f;
                m_Velocity.z = 0.0f;
            }

            // Sprint handler
            if (isSprinting)
            {
                // Loop sprint sound
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().PlayOneShot(GetComponent<PlayerScript>().sprintSound);
                }

                GetComponent<PlayerScript>().stamina -= 0.1f;
            }
            else
            {
                if (GetComponent<PlayerScript>().stamina <GetComponent<PlayerScript>().stamina_max)
                {
                    GetComponent<PlayerScript>().stamina += 0.2f;
                }
            }

            // Interactive controls
        }
    }
}
