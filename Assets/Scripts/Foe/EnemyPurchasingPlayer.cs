using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPurchasingPlayer : MonoBehaviour
{
    private NavMeshAgent Mob;

    public GameObject Player;

    public float MobDistanceRun = 4.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Mob = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        
        // Run towards player

        if (distance < MobDistanceRun)
        {
            Mob.SetDestination(Player.transform.position);
        }
    }
}
