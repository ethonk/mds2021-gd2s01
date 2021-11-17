using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billCharMotor : MonoBehaviour
{
    [Header("Attached Components")]
    public CharacterController m_Controller;
    public billMouseLook m_Look;

    [Header("Motion Values")]
    public float m_MoveSpeed = 10.0f;
    public float m_Gravity = 40.0f;
    public float m_JumpSpeed = 12.0f;

    [Header("Current State")]
    public Vector3 m_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
    public bool m_Grounded = false;

 

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody otherBody = hit.gameObject.GetComponent<Rigidbody>();

        if (otherBody)
        {
            Vector3 pushVelocity = m_Velocity;
            pushVelocity.y = 0.0f;
            otherBody.velocity = m_Velocity;

        }

    }

    // Update is called once per frame
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
        if (Input.GetKey(KeyCode.W))
        {
            z += 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            z -= 1.0f;
        }

        if (m_Grounded && Input.GetKey(KeyCode.Space))
        {
            m_Velocity.y = m_JumpSpeed;
        }

        Vector3 inputMove = new Vector3(x, 0.0f, z);
        inputMove = Quaternion.Euler(0.0f, m_Look.m_Spin, 0.0f) * inputMove;

        m_Velocity.x = inputMove.x * m_MoveSpeed;
        m_Velocity.y -= m_Gravity * Time.deltaTime;
        m_Velocity.z = inputMove.z * m_MoveSpeed;

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

        if ((m_Controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            m_Velocity.y = -1.0f;
        }



    }
}