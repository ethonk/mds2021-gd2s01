using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interact
{
    public class InteractionController : MonoBehaviour
    {
        #region Variables
            [Header("Refrences")]
            public CharacterMotor m_PlayerMotor;
            public Camera Maincam;
            public GameObject m_Player;
            public PlayerScript m_PlayerScript;

            [Header ("Data")]
            public InteractionInputData interactionInputData;
            public InteractionData interactionData;
            
            [Space]
            [Header ("UI")]
            [SerializeField] private InteractionUI uiPanel;

            [Space]
            [Header ("Ray Settings")]
            public float rayDistance;
            public float raySphereRadius;
            public LayerMask interactibleLayer;

            #region Private
                private Camera m_Camera;
                private bool m_interacting;
                private float m_holdTimer = 0f;
                RaycastHit _hit;
            #endregion

        #endregion
        
        #region Built-in methods
            void Awake()
            {
                m_Camera = FindObjectOfType<Camera>();
            }

            public void Start()
            {
                // finds player at the start
                m_Player = GameObject.Find("Player");
                m_PlayerScript = m_Player.GetComponent<PlayerScript>();
            }

            void Update()
            {
                CheckForInteractible(); // checks if player is pointing at interactible
                CheckForInteractibleInput();    // if it is an interactible, do something
            }
        #endregion

        #region Custom Methods
            void CheckForInteractible()
            {
                Ray _ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
                RaycastHit _hitInfo;

                bool _hitSomething = Physics.SphereCast(_ray, raySphereRadius, out _hitInfo, rayDistance, interactibleLayer);

                if (_hitSomething) // self explanatory, if the ray hits something
                {
                    InteractibleBase _interactible = _hitInfo.transform.GetComponent<InteractibleBase>(); // checks if interactable has the component interactible base on it
                    _hit = _hitInfo;

                    if(_interactible != null) // if the interactible is not null
                    {
                        if(interactionData.IsEmpty()) // if the interactible data is empty / if there is a slot for an interactible
                        {
                            interactionData.Interactible = _interactible; // interaction data is set to this new interactible

                            //merchant specific
                            if (_hitInfo.transform.gameObject.CompareTag("Merchant"))
                            {
                                 uiPanel.SetTooltip("Merchant press (E) to interact");
                            }
                            else
                            {
                                uiPanel.SetTooltip(_hitInfo.transform.gameObject.name); // else just use the default name
                            }

                            if (m_PlayerScript.cameraState == PlayerScript.CameraState.shop)
                            {
                                 uiPanel.SetTooltip("Press (E) to exit");
                            }

                            else if (_hitInfo.transform.gameObject.GetComponent<ItemScript>() != null)
                            {
                                uiPanel.SetTooltip(_hitInfo.transform.gameObject.GetComponent<ItemScript>().itemName); // sets UI to whatever the ItemScript name of the gameObject the raycast gets is
                            }
                        }
                        else // if there is an interactible in the interactible slot
                        {
                            if(!interactionData.isSameInteractible(_interactible)) // check if its not the same
                            {
                                interactionData.Interactible = _interactible; // override the current interactible data

                                //merchant specific
                                if (_hitInfo.transform.gameObject.CompareTag("Merchant")) // same thing
                                {
                                    uiPanel.SetTooltip("Merchant press (E) to interact");
                                }

                                if (m_PlayerScript.cameraState == PlayerScript.CameraState.shop)
                                {
                                    uiPanel.SetTooltip("Press (E) to exit");
                                }

                                if (_hitInfo.transform.gameObject.GetComponent<ItemScript>() != null) // same thing
                                {
                                uiPanel.SetTooltip(_hitInfo.transform.gameObject.GetComponent<ItemScript>().itemName);
                                }
                                else
                                {
                                uiPanel.SetTooltip(_hitInfo.transform.gameObject.name);
                                }
                            }
                        }
                    }
                }
                else // if we don't hit anything
                {
                    uiPanel.HideBar();  // hides UI
                    uiPanel.ResetUI();  // Resets UI
                    interactionData.ResetData();
                }

                //ray used for debugging
                //Debug.DrawRay(_ray.origin, _ray.direction * rayDistance, _hitSomething ? Color.green : Color.red);
            }

            bool CheckForDestroyObj()
            {
                if (interactionData.Interactible.DestroyObj)
                {
                    print("Bruh:");
                    return true;
                }
                else
                {
                    print("Bruh2");
                    return false;
                }
            }

            // checks if the player is locked down, on most locks
            void CheckForLocks()
            {
                if (Maincam.gameObject.activeInHierarchy && Cursor.lockState == CursorLockMode.None && !m_interacting)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    print("Cursor off");

                }
    
                if (Maincam.gameObject.activeInHierarchy && m_PlayerMotor.playerLock && !m_interacting)
                {
                    print( "Lock off" );
                    m_PlayerMotor.playerLock = false;
                }

                if (Maincam.gameObject.activeInHierarchy && m_Player.GetComponent<MouseLook>().m_CameraLock && !m_interacting)
                {
                    m_Player.GetComponent<MouseLook>().m_CameraLock = false;
                }

                // if player main camera is up
                if (Maincam.gameObject.activeInHierarchy)
                {
                    m_Player.GetComponent<PlayerScript>().cameraState = PlayerScript.CameraState.normal;
                }
            }

            void CheckForInteractibleInput()
            {
                if(interactionData.IsEmpty())
                    return;
                 

                if (interactionInputData.InteractClicked)
                {
                    m_interacting = true;
                    m_holdTimer = 0f;
                }

                if(interactionInputData.InteractReleased)
                {
                    m_interacting = false;
                    m_holdTimer = 0f;
                    uiPanel.HideBar();  // hides when interact key is released
                }

                if(m_interacting)
                {
                    if(!interactionData.Interactible.IsInteractible)
                        return;
                    

                    if(interactionData.Interactible.HoldInteract)
                    {
                        uiPanel.ShowBar();  // only shows bar in HoldInteract
                        m_holdTimer += Time.deltaTime;

                        float heldPercentage = m_holdTimer / interactionData.Interactible.HoldDuration;
                        uiPanel.UpdateProgressBar(heldPercentage);

                        if(heldPercentage > 1f)
                        {
                            uiPanel.HideBar();
 
                            if (CheckForDestroyObj())
                            {
                                interactionData.Interact();
                                Destroy(_hit.transform.gameObject);
                                m_interacting = false;
                                
                            }
                            else
                            {
                                interactionData.Interact();
                                m_interacting = false;
                                CheckForLocks(); // check if the player is locked, and if meets conditions, unlock
                            }
                        }
                    }
                    else
                    {
                        if (CheckForDestroyObj())
                        {
                            interactionData.Interact();
                            Destroy(_hit.transform.gameObject);
                            m_interacting = false;
                        }
                        else
                        {
                            interactionData.Interact();
                            m_interacting = false;
                            CheckForLocks(); // check if the player is locked, and if meets conditions, unlock
                        }
                    }
                }
            }

        #endregion
    }
}
