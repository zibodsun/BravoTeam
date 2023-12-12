using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Gorilla : MonoBehaviour
{
    public float wanderDistance;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Random NavMesh position

        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderDistance;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, wanderDistance, NavMesh.AllAreas);
        // targetPosition = navHit.position;
        // agent.SetDestination(targetPosition);
        // _waitTimer = 0f;
    }
}
