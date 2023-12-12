using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Gorilla : MonoBehaviour
{
    public bool isWalking = true;

    public NavMeshAgent agent;
    public float wanderDistance;
    private Vector3 targetPosition;

    private float _waitTime;
    private float _waitTimer;

    // Start is called before the first frame update
    void Start()
    {
        _waitTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {

        // Random NavMesh position
        if (_waitTimer < _waitTime)
        {
            _waitTimer += Time.deltaTime;
        }
        else if(isWalking)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderDistance;
            randomDirection += transform.position;
            NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, wanderDistance, NavMesh.AllAreas);
            targetPosition = navHit.position;
            agent.SetDestination(targetPosition);
            _waitTimer = 0f;
        }   
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Banana") {
            GetComponent<Animator>().Play("Jump");
        }
    }
}
