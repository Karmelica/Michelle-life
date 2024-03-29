using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Laluna : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;

    [Header("Objects")]
    public Transform objekt;

    private NavMeshAgent meshAgent;
    private Rigidbody rb;

    void Start()
    {
        rb= GetComponent<Rigidbody>();
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.SetDestination(objekt.position);
    }

    void Update()
    {
        animator.SetFloat("Vel", rb.velocity.magnitude);
        Debug.Log(rb.velocity.magnitude);
        
    }
}
