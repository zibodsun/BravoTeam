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

    public GorillaSceneManager sceneManager;
    public Transform targetTree;
    public Transform leavePosition;
    public NavMeshAgent agent;
    public Rigidbody chainsaw;

    public State state;
    public State tempState;

    Animator animator;
    private float _speed;
    private bool _cutting;
    private bool _injured;

    public AudioSource chainsawStart;
    public AudioSource chainsawWhirl;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager.Play(3);
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
        animator.SetBool("cutting", _cutting);
        animator.SetBool("injured", _injured);

        if (state != tempState)  // when the state changes
        {
            switch (state)
            {
                case State.Walking:
                    // play walking animation
                    break;
                case State.Cutting:
                    _cutting = true;
                    break;
                case State.Escaping:
                    // play escape animation
                    _injured = true;    // triggers the Angry animation
                    break;

            }
        }   

        // When the destination is reached
        if (!agent.pathPending && state == State.Walking)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude <= 0.3f)
                {
                    tempState = state;
                    state = State.Cutting;
                    sceneManager.humanStartedCutting = true;
                    chainsawStart.Play();
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Banana")
        {
            StartCoroutine(Escape());
        }
    }
    public void ApproachTree() {
        Vector3 offset = new Vector3(0, 0, 1.1f);
        agent.SetDestination(targetTree.position + offset);
        tempState = state;
        state = State.Walking;
    }
    // human gets hit by banana
    IEnumerator Escape() {
        yield return new WaitForSeconds(2f);
        // animaition play
        Debug.Log("Escaping");
        tempState = state;
        state = State.Escaping;
        StartCoroutine(Wait());
    }
    // "angry" animation stops playing
    IEnumerator Wait() {
        yield return new WaitForSeconds(5f);
        agent.SetDestination(leavePosition.position);
        agent.speed = agent.speed * 0.9f;       // slows down the agent to simulate injury
    }

    public void DropChainsaw() {
        chainsawWhirl.Stop();
        chainsaw.transform.SetParent(null);
        chainsaw.GetComponent<BoxCollider>().enabled = true;
        chainsaw.useGravity = true;
    }

    public void ChainsawPlay() {
        chainsawWhirl.gameObject.SetActive(true);
    }
}
