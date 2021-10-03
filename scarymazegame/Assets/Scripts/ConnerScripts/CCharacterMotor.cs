using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterMotor : MonoBehaviour
{
    public float m_SprintTimer = 0.0f;
    public CharacterController m_Controller;
    public MouseLook m_Look;

    public float m_MoveSpeed = 6.0f;
    public float m_Gravity = 40.0f;
    public float m_JumpSpeed = 12.0f;
    public float m_GroundedLenience = 0.25f;
    public float m_Acceleration = 5.0f;
    public AnimationCurve m_FrictionCurve;
    public float m_AirMomentumMultiplier = 0.0f;
    public float m_SprintMod = 3.0f;
    public bool m_Sprinting;
    public float m_Stamina = 100.0f;

    public Vector3 m_Velocity = Vector3.zero;
    public bool m_Grounded = false;
    public float m_GroundedTimer = 0.0f;
    public float m_DistanceTravelled = 0.0f;

    public Transform m_FeetPosition;
   
    public float m_FootstepLength = 0.5f;

    public float m_JumpFootstepTimer = 0.0f;
    public float m_JumpFootstepCooldown = 0.5f;

    public bool m_IsRightFoot = true;

    
    public Transform pCamera;


    void Update()
    {

        float x = 0.0f;
        x -= Input.GetKey(KeyCode.A) ? 1.0f : 0.0f;
        x += Input.GetKey(KeyCode.D) ? 1.0f : 0.0f;
        float z = 0.0f;
        z -= Input.GetKey(KeyCode.S) ? 1.0f : 0.0f;
        z += Input.GetKey(KeyCode.W) ? 1.0f : 0.0f;

       

        if (m_GroundedTimer > 0.0f && Input.GetKey(KeyCode.Space))
        {
            m_Velocity.y = m_JumpSpeed;
            m_GroundedTimer = 0.0f;
            m_Grounded = false;
        }


        Vector3 inputMove = new Vector3(x, 0.0f, z);
        inputMove = Quaternion.Euler(0.0f, m_Look.m_Spin, 0.0f) * inputMove;
        if (inputMove.magnitude > 1.0f)
        {
            inputMove.Normalize();
        }

        float cacheY = m_Velocity.y;
        m_Velocity.y = 0.0f;

        float mag = m_Velocity.magnitude;

        float airMod = m_Grounded ? 1.0f : m_AirMomentumMultiplier;
        m_Velocity += inputMove * m_Acceleration * Time.deltaTime * airMod;
        m_Velocity -= m_Velocity.normalized * m_Acceleration * m_FrictionCurve.Evaluate(mag) * Time.deltaTime * airMod;

        m_Velocity.y = cacheY;
        m_Velocity.y -= m_Gravity * Time.deltaTime;
        m_Sprinting = Input.GetKey(KeyCode.LeftShift);
    

        if (!m_Sprinting && m_Stamina < 100.0f)
        {
            m_SprintTimer -= 1 * Time.deltaTime;
            if (m_SprintTimer <= 0.0f)
            {
                
                m_SprintTimer = 0.0f;
            }
            if (m_SprintTimer == 0.0f)
            {
                m_Stamina += 10 * Time.deltaTime;
            }
            
        }
        if (m_Stamina > 100.0f)
        {
            m_Stamina = 100.0f;
        }

        float SprintMd = 1.0f;
        if (m_Sprinting && m_Stamina > 0.0f)
        {
            SprintMd = m_SprintMod;
            m_Stamina -= 10 * Time.deltaTime;
            m_SprintTimer = 3.0f;
        }

        Vector3 trueVelocity = m_Velocity;
        trueVelocity.x *= m_MoveSpeed * SprintMd;
        trueVelocity.z *= m_MoveSpeed * SprintMd;

        Vector3 oldPos = transform.position;
        m_Controller.Move(trueVelocity * Time.deltaTime);
        Vector3 actualMove = transform.position - oldPos;

        m_JumpFootstepTimer -= Time.deltaTime;
        m_GroundedTimer -= Time.deltaTime;

        if ((m_Controller.collisionFlags & CollisionFlags.Below) != 0)
        {
            if (!m_Grounded && m_JumpFootstepTimer < 0.0f)
            {
                m_JumpFootstepTimer = m_JumpFootstepCooldown;
                m_DistanceTravelled = 0.0f;
                
            }

            m_Velocity.y = -1.0f;
            m_GroundedTimer = m_GroundedLenience;
            m_Grounded = true;
        }
        else
        {
            m_Grounded = false;
        }

        if (m_Grounded)
        {
            actualMove.y = 0.0f;
            m_DistanceTravelled += actualMove.magnitude;
            if (m_DistanceTravelled > m_FootstepLength)
            {
                m_DistanceTravelled -= m_FootstepLength;
               
            }
        }

    }
}