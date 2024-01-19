using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.GraphicsBuffer;

public class Gorilla : MonoBehaviour
{
    public GorillaSceneManager sceneManager;
    public bool isRoaming = true;

    public NavMeshAgent agent;
    public float wanderDistance;
    public int numBananas;
    public Transform bananaAttachPoint;

    public Transform attackPosition;        // Place outside of wander distance to avoid issues
    private Vector3 targetPosition;

    public float throwForce;

    private float _waitTime;
    private float _waitTimer;

    private MultiAimConstraint multiAimConstraint;
    public RigBuilder rigBuilder;
    private Animator animator;
    private float speed;
    private bool _startedAttacking;
    private bool _angry;
    private bool _throw;
    private Transform humanTransform;

    Transform banana;
    Rigidbody rb;

    public AudioSource breath;
    public AudioSource roar;

    // Start is called before the first frame update
    void Start()
    {
        _waitTime = Random.Range(5f,10f);
        numBananas = 0;
        multiAimConstraint = GetComponentInChildren<MultiAimConstraint>();
        rigBuilder = GetComponentInChildren<RigBuilder>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = agent.desiredVelocity.magnitude;
        animator.SetFloat("speed", speed);
        animator.SetBool("angry", _angry);
        animator.SetBool("throw", _throw);

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

        // when attack position is reached
        if (reachedLocation() && sceneManager.humanStartedCutting)
        {
            _startedAttacking = true;
            humanTransform = FindFirstObjectByType<Human>().transform;
            transform.LookAt(humanTransform);
            _angry = true;
            roar.Play();
            StartCoroutine(RandomiseThrowTime());
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // When banana is given to the gorilla
        if (other.tag == "Banana") {
            other.GetComponent<XRGrabInteractable>().enabled = false;
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.SetParent(bananaAttachPoint);
            other.transform.localPosition = new Vector3(0, 0, 0);
            GainBanana();
        }
    }
    // add a banana to this gorilla
    public void GainBanana() {
        numBananas++;
        GetComponent<Animator>().Play("Jump");
        breath.Play();
        GetComponent<Collider>().enabled = false;
    }
    // check if this gorilla has been given any bananas
    public bool HasBanana() {
        if (numBananas > 0) {
            return true;
        }
        return false;
    }

    IEnumerator RandomiseThrowTime() {
        yield return new WaitForSeconds(Random.Range(1f,3f));
        _throw = true;
    }

    public void AttackHuman() {
        rigBuilder.enabled = true;
        agent.SetDestination(attackPosition.position);
        isRoaming = false;
    }

    public void ThrowBanana() {
        banana = bananaAttachPoint.Find("Banana");
        rb = banana.GetComponent<Rigidbody>();
        banana.transform.SetParent(null);
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce( (humanTransform.position - transform.position) * throwForce * 100);    // add force towards the direction of the human
    }

    bool reachedLocation() {
        if (_startedAttacking) {
            return false;
        }
        return Vector3.Distance(attackPosition.position, transform.position) <= 0.1f;
    }
}
