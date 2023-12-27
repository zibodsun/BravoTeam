using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.GraphicsBuffer;

public class Gorilla : MonoBehaviour
{
    public bool isRoaming = true;

    public NavMeshAgent agent;
    public float wanderDistance;
    public int numBananas;
    public Transform bananaAttachPoint;

    public Transform attackPosition;
    private Vector3 targetPosition;

    private float _waitTime;
    private float _waitTimer;

    private MultiAimConstraint multiAimConstraint;
    private RigBuilder rb;
    private Animator animator;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        _waitTime = 5f;
        numBananas = 0;
        multiAimConstraint = GetComponentInChildren<MultiAimConstraint>();
        rb = GetComponentInChildren<RigBuilder>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = agent.desiredVelocity.magnitude;
        animator.SetFloat("speed", speed);

        // Random NavMesh position
        if (_waitTimer < _waitTime)
        {
            _waitTimer += Time.deltaTime;
        }
        else if(isRoaming)
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
            other.GetComponent<XRGrabInteractable>().enabled = false;
            other.transform.SetParent(bananaAttachPoint);
            other.transform.localPosition = new Vector3(0, 0, 0);
            GainBanana();
        }
    }
    // add a banana to this gorilla
    public void GainBanana() {
        numBananas++;
        GetComponent<Animator>().Play("Jump");
        GetComponent<Collider>().enabled = false;

        // multiAimConstraint.data.sourceObjects.Clear();
    }
    // check if this gorilla has been given any bananas
    public bool HasBanana() {
        if (numBananas > 0) {
            return true;
        }
        return false;
    }

    IEnumerator Walk(Vector3 target) {
        var step = 1 * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        Debug.Log("Moving " + this.gameObject.name);
        yield return null;
    }

    public void AttackHuman() {
        // Human human = (Human)FindAnyObjectByType(typeof(Human));
        // StartCoroutine(Walk(attackPosition.position));
        // multiAimConstraint.data.sourceObjects.Add(new WeightedTransform(human.transform, 1));
        GetComponent<RigBuilder>().enabled = true;
        agent.SetDestination(attackPosition.position);
        isRoaming = false;
    }
}
