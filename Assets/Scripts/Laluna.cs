using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Laluna : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Image[] needBar;
    public float[] needPriority;


    [Header("Objects")]
    public Transform[] needs;

    private NavMeshAgent meshAgent;

    public void Bar(Image bar, float ratio, int i)
    {
        if(bar.fillAmount > 0)
        {
            bar.fillAmount -= Time.deltaTime * 0.05f / ratio;
        }
        if(bar.fillAmount < 0.2) {
            meshAgent.SetDestination(needs[i].position);
            
            Vector3 distance = needs[i].position - transform.position;
            if (distance.magnitude >= 1.5)
            {
                meshAgent.isStopped = false;
                animator.SetBool("isStopped", meshAgent.isStopped);
            }
            else
            {
                meshAgent.isStopped = true;
                animator.SetBool("isStopped", meshAgent.isStopped);
            }
        }

    }

    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        for(int i = 0; i<needBar.Length; i++)
        {
            Bar(needBar[i], needPriority[i], i);
        }
    }
}
