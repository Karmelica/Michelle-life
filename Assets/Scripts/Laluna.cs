using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Laluna : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Image danceOff;
    public Image pills;
    public Image talkShit;
    public Image dissTrack;


    [Header("Objects")]
    public Transform objekt;

    private NavMeshAgent meshAgent;

    public void Bar()
    {

    }

    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.SetDestination(objekt.position);
    }

    void Update()
    {


        Vector3 distance = objekt.position - transform.position;
        if(distance.magnitude > 2)
        {
            meshAgent.isStopped = false;
            animator.SetBool("isStopped", meshAgent.isStopped);
            meshAgent.SetDestination(objekt.position);
        }
        else
        {
            animator.SetBool("isStopped", meshAgent.isStopped);
            meshAgent.isStopped = true;
        }
        
    }
}
