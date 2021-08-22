using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject[] patrolPoints;
    public GameObject Player;
    private NavMeshAgent Mob;
    public float EnemySightRange = 4.0f;

    private int destinationIndex = 0;
    private GameObject destination;
    
    // Start is called before the first frame update
    void Start()
    {
        Mob = GetComponent<NavMeshAgent>();

        chooseDestination();
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        
        if (distance < EnemySightRange)
        {
            chasePlayer();
        }
        else
        {
            patrol();
        }

    }

    void patrol()
    {
        SetDestination();
        
        if (ArrivedAtDestination())
        {
            chooseDestination();
        }
    }

    void chasePlayer()
    {
        transform.LookAt(Player.transform);
        Mob.SetDestination(Player.transform.position);
    }

    void chooseDestination()
    {
        destinationIndex = (destinationIndex + 1) % patrolPoints.Length;
        destination = patrolPoints[destinationIndex];
    }

    void SetDestination()
    {
        Mob.SetDestination(new Vector3(
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
