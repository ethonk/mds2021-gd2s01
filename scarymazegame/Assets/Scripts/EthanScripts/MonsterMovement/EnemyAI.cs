//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : EnemyAI.cs
// Description : Controls where and when the monster moves. Calls attacks and actions.
// Author : Ethan Velasco Uy
// Mail : ethan.uy@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip monsterCry;

    [Header("Player and Agent")]
    public NavMeshAgent agent;
    public Transform player;

    [Header("Layer Masks")]
    public LayerMask groundLayer, playerLayer;

    [Header("Patrol Variables")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attack Variables")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    [Header("AI States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, playerInCoverageRange; // Attacking based
    public bool debuffed;
    public bool infatuated;
    public bool firstEncounter = true;

    [Header("Trap Interaction Values")]
    public float debuffCooldown;

    [Header("AI Variables")]
    public Vector3 startPosition;
    public float areaCoverage;

    private void Awake()
    {
        // Initialize player and navmesh agent.
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        // Initialize starting position of monster.
        startPosition = transform.position;
    }

    #region == FUNCTIONS ==

    #region Trap Effects
    public IEnumerator PlayJumpscare(float _time)
    {
        yield return new WaitForSeconds(_time);
        GetComponent<AudioSource>().PlayOneShot(GetComponent<MonsterDetails>().jsSound);
    }

    public void Debuff_Damage(float normalDamage)
    {
        if (!debuffed)
        {
            print("damage!");
            // Start cooldown
            StartCoroutine(DebuffCooldown(debuffCooldown));

            // Apply Damage
            GetComponent<MonsterDetails>().health -= normalDamage;
        }
    }

    public IEnumerator Debuff_Slow(float slowDamage, float slowPercentage, float slowTime)
    {
        // Apply debuff cooldown
        if (!debuffed)
        {
            print("slow!");
            // Start cooldown
            StartCoroutine(DebuffCooldown(debuffCooldown));

            // Apply Damage
            GetComponent<MonsterDetails>().health -= slowDamage;

            // Apply Slow
            GetComponent<MonsterDetails>().currentSpeed *= slowPercentage;
            yield return new WaitForSeconds(slowTime);
            GetComponent<MonsterDetails>().currentSpeed = GetComponent<MonsterDetails>().speed;
        }
    }

    public void Debuff_Infatuation(float infatuationDamage, float infatuationTime)
    {
        // Apply debuff cooldown
        if (!debuffed)
        {
            // Start cooldown
            StartCoroutine(DebuffCooldown(debuffCooldown));

            // Apply Damage
            GetComponent<MonsterDetails>().health -= infatuationDamage;
            
            // Apply infatuation
            StartCoroutine(Infatuation_Cooldown(infatuationTime));
        }
    }

    public IEnumerator Infatuation_Cooldown(float infatuationTime)
    {
            infatuated = true;
            yield return new WaitForSeconds(infatuationTime);
            infatuated = false;
    }

    #endregion

    public IEnumerator DebuffCooldown(float cooldown)
    {
        debuffed = true;
        yield return new WaitForSeconds(cooldown);
        debuffed = false;
    }

    private void Update()
    {   
        // Adapt speed
        GetComponent<NavMeshAgent>().speed = GetComponent<MonsterDetails>().currentSpeed;

        //Check for sight, attack and coverage range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        playerInCoverageRange = Physics.CheckSphere(startPosition, areaCoverage, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)                                                        // Player is NOT in sight range and NOT attack range, Patrol.
        {
            MoveToStart();
            firstEncounter = true;
        }
        if (playerInSightRange && !playerInAttackRange && !player.GetComponent<CharacterMotor>().playerLock)     // Player IS in sight range and NOT in attack range, Chase.
        {
            if (firstEncounter == true)
            {
                firstEncounter = false;
                StartCoroutine(PlayJumpscare(0.3f));
            }
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange && !player.GetComponent<CharacterMotor>().playerLock)      // Player IS in sight range and IS in attack range, Attack.
        {
            AttackPlayer();
        }
        if (!playerInCoverageRange)                                                                              // Player IS NOT in monster's coverage.
        {
            MoveToStart();
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
            walkPointSet = true;
        }
    }

    private void MoveToStart()
    {
        agent.SetDestination(startPosition);
    }

    private void Patroling()
    {
        // Find a walk point if walk point is not set.
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        // If set, set the AI's next destination to walk point.
        if (walkPointSet)
        {
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

    private bool AttackPlayer()
    {
        if (infatuated) return false;               // Fail attack if monster is infatuated
        // Make enemy not move while attacking.
        agent.SetDestination(transform.position);
        // Face enemy to the player.
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // roll
            int roll = Random.Range(0, 2);
            print(roll);
            // random attacks
            switch (roll)
            {
                case 0: // slash attack
                    GetComponent<Attacks>().SlashAttack(player.GetComponent<PlayerScript>());
                    break;
                case 1: // eat attack
                    GetComponent<Attacks>().EatAttack(player.transform, transform.Find("Torso").Find("BodyParts").Find("Head").Find("EatPart"));
                    break;
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        return true;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        // == DRAWING GIZMOS FOR SCENE VIEW ==
        // Attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        // Sight range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        // Area coverage
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, areaCoverage);
    }
    #endregion
}
