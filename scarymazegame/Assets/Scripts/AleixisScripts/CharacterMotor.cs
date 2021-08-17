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
    public float m_DJumpMultiplier = 0.5f;

    public int m_JumpCount = 0;


    [Header("Current State")]
    public Vector3 m_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 inputMove;
    public bool m_Grounded = false;
    public bool isSprinting = false;
    public bool DJump = false;
    public bool WallJump = false;


    void Update()
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
            WallJump = false;
            DJump = true; 

            if (Input.GetButtonDown("Jump"))
            {
                m_Velocity.y = m_JumpSpeed;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && DJump)
            {
                m_Velocity.y = m_JumpSpeed * m_DJumpMultiplier;
                DJump = false;
            }
        }
        
        Vector3 inputMove = new Vector3(x,0.0f, z);
        inputMove = Quaternion.Euler(0.0f,m_Look.m_Spin,0.0f) * inputMove;

        m_Velocity.x = inputMove.x * m_MoveSpeed;
        m_Velocity.y -= m_Gravity * Time.deltaTime;
        m_Velocity.z = inputMove.z * m_MoveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_Velocity.x = inputMove.x * m_MoveSpeed * m_SprintSpeed;
            m_Velocity.z = inputMove.z * m_MoveSpeed * m_SprintSpeed;
            isSprinting = true;
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

    }

    private void OnControllerColliderHit(ControllerColliderHit wall)
    {
        WallJump = true;
        if(!m_Controller.isGrounded && wall.normal.y < 0.1f && WallJump)
        {
            if (Input.GetButtonDown("Jump"))
            {
                m_Velocity.y = m_JumpSpeed;
                WallJump = false;
                DJump = true;
            }
            
        }
    }
}
