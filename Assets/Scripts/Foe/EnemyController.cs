using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    public GameObject[] patrolPoints;
    
    private NavMeshAgent agent;
    private GameObject player;
    private int destinationIndex = 0;
    private GameObject destination;
    
    // Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked = false;
    public GameObject projectilePrefab;
    public GameObject projectileStart;
    
    // States
    [FormerlySerializedAs("EnemySightRange")] public float sightRange = 4.0f;
    public float attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();

        chooseDestination();
    }

    // Update is called once per frame
    void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        playerInSightRange = distanceToPlayer <= sightRange;
        playerInAttackRange = distanceToPlayer <= attackRange;

        if (!playerInSightRange)
        {
            Patrol();
        } else
        {
            ChasePlayer();
        }
        
        if (playerInAttackRange)
        {
            AttackPlayer();
        }

    }

    void Patrol()
    {
        SetDestination();
        
        if (ArrivedAtDestination())
        {
            chooseDestination();
        }
    }

    void ChasePlayer()
    {
        transform.LookAt(player.transform);
        agent.SetDestination(player.transform.position);
    }

    void AttackPlayer()
    {
        // Debug.Log("alreadyAttacked" + alreadyAttacked);
        if (!alreadyAttacked)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward * 1f + transform.up * 2f, transform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 35f, ForceMode.Impulse);
            // rb.AddForce(transform.up * 5f, ForceMode.Impulse);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    
    void chooseDestination()
    {
        destinationIndex = (destinationIndex + 1) % patrolPoints.Length;
        destination = patrolPoints[destinationIndex];
    }

    void SetDestination()
    {
        agent.SetDestination(new Vector3(
            destination.transform.position.x,
            transform.position.y,
            destination.transform.position.z));
    }

    bool ArrivedAtDestination()
    {
        if (Vector3.Distance(transform.position, new Vector3(
            destination.transform.position.x,
            transform.position.y,
            destination.transform.position.z)) < 0.1f)
        {
            return true;
        }

        return false;
    }
}
