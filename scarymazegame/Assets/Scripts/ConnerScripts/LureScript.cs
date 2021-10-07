using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class LureScript : MonoBehaviour
{
    //audio
    public AudioClip monsterCry;

    public NavMeshAgent agent;

    public Transform player;
    public Transform playerBait;

    public LayerMask groundLayer, playerLayer;


    // UI
    public GameObject monsterNameLayout;


    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks = 2f;
    bool alreadyAttacked;



    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    bool Taunted;



    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        playerBait = GameObject.Find("PlayerBait").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    #region == FUNCTIONS ==

    void ShowMonsterNameUI()
    {
        monsterNameLayout.SetActive(true);
        monsterNameLayout.transform.Find("MonsterName").GetComponent<TextMeshProUGUI>().text = GetComponent<MonsterDetails>().monsterName;
    }

    private void Update()
    {
        //Check for sigyht and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        Taunted = Physics.CheckSphere(transform.position, sightRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)                                                        // Player is NOT in sight range and NOT attack range, Patrol.
        {
            Patroling();
        }
        if (Taunted)
        {
            ShowMonsterNameUI();
            AttackLure();
        }
        if (!Taunted && playerInSightRange && !playerInAttackRange && !player.GetComponent<CharacterMotor>().playerLock)     // Player IS in sight range and NOT in attack range, Chase.
        {
            ShowMonsterNameUI();
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange && !player.GetComponent<CharacterMotor>().playerLock)      // Player IS in sight range and IS in attack range, Attack.
        {
            AttackPlayer();
        }
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range.
        float randX = Random.Range(-walkPointRange, walkPointRange);
        float randZ = Random.Range(-walkPointRange, walkPointRange);

        // Set new random walkpoint.
        walkPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);

        if (Physics.Raycast(-walkPoint, -transform.up, 2f, groundLayer))
        {
            print("found walkpoint!");
            walkPointSet = true;
        }
    }

    private void Patroling()
    {
        // Find a walk point if walk point is not set.
        if (!walkPointSet)
        {
            print("search walk point");
            SearchWalkPoint();
        }
        // If set, set the AI's next destination to walk point.
        if (walkPointSet)
        {
            print("walking to point");
            agent.SetDestination(walkPoint);
        }

        // Calculate when walk point is reached.
        Vector3 distanceToWalkPoint = transform.position - walkPoint;   // Get distance of AI from Destination
        // If it reaches its walk point, walkPointSet = false, triggering an immediate search for the next walk point.
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void ChasePlayer()
    {
        // Play monster cry
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(monsterCry);
        }

        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Make enemy not move while attacking.
        agent.SetDestination(transform.position);
        // Face enemy to the player.
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            GetComponent<Attacks>().EatAttack(player.transform, transform.Find("Torso").Find("BodyParts").Find("Head").Find("EatPart"));
            print("damage player");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void AttackLure()
    {
        // Make enemy not move while attacking.
        agent.SetDestination(transform.position);
        // Face enemy to the player.
        transform.LookAt(playerBait);

        
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize Attack and Sightrange.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    #endregion
}
