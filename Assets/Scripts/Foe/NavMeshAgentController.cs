using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    public GameObject[] patrolPoints;
    private int destinationIndex = 0;
    private GameObject destination;
    
    // Start is called before the first frame update
    void Start()
    {
        SetDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (ArrivedAtDestination())
        {
            destinationIndex = (destinationIndex + 1) % patrolPoints.Length;
            SetDestination();
        }
    }

    void SetDestination()
    {
        Debug.Log("asdfpoiu");
        destination = patrolPoints[destinationIndex];
        GetComponent<NavMeshAgent>().SetDestination(new Vector3(
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
