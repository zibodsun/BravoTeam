using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Human : MonoBehaviour
{
    public Transform targetTree;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ApproachTree();
    }

    // Update is called once per frame
    void Update()
    {

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
    }
    public void Escape() {
        // animaition play
        Debug.Log("Escaping");
    }
}
