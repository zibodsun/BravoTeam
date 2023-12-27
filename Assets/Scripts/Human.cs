using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Human : MonoBehaviour
{
    public enum State
    {
        Walking,
        Cutting,
        Escaping
    }

    public Transform targetTree;
    public NavMeshAgent agent;

    public State state;
    public State tempState;

    Animator animator;
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ApproachTree();
        
    }

    // Update is called once per frame
    void Update()
    {
        // walking animation
        _speed = agent.desiredVelocity.magnitude;
        animator.SetFloat("speed", _speed);

        if (state != tempState)  // when the state changes
        {
            switch (state)
            {
                case State.Walking:
                    // play walking animation
                    break;
                case State.Cutting:
                    animator.Play("BeginCutting");
                    break;
                case State.Escaping:
                    // play escape animation
                    break;

            }
        }   

        // When the destination is reached
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    tempState = state;
                    state = State.Cutting;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Banana")
        {
            Escape();
        }
    }
    public void ApproachTree() {
        agent.SetDestination(targetTree.position);
        tempState = state;
        state = State.Walking;
    }
    public void Escape() {
        // animaition play
        Debug.Log("Escaping");
    }
}
