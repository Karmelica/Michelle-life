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
    private int priority;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Guitar"))
        {
            needBar[0].fillAmount += Time.deltaTime * 0.2f;
            animator.SetTrigger("Guitar");
        }
        if (other.CompareTag("Pills"))
        {
            needBar[1].fillAmount += Time.deltaTime * 0.2f;
            animator.SetTrigger("Pills");
            animator.SetBool("onDrugs", true);
        }
        if (other.CompareTag("Dance"))
        {
            needBar[2].fillAmount += Time.deltaTime * 0.2f;
            animator.SetTrigger("Dance");
        }
        if (other.CompareTag("Phone"))
        {
            needBar[3].fillAmount += Time.deltaTime * 0.2f;
            animator.SetTrigger("Phone");
        }
    }

    public void Bar(Image bar, float ratio, int i)
    {
        if(bar.fillAmount > 0.05)
        {
            bar.fillAmount -= Time.deltaTime * 0.05f / ratio;
        }
        if(bar.fillAmount < 0.33 && !meshAgent.hasPath)
        {
            priority = i;
            meshAgent.SetDestination(needs[i].position);
            meshAgent.isStopped = false;
            animator.SetBool("isStopped", false);
        }
        if(bar.fillAmount >= 1)
        {
            animator.SetTrigger("BarFull");
        }
    }

    public void Destination()
    {
        Vector2 distance = needs[priority].position - transform.position;

        if(distance.magnitude <= 1f)
        {
            meshAgent.isStopped = true;
            meshAgent.ResetPath();
            animator.SetBool("isStopped", true);
        }
    }

    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Bar(needBar[0], needPriority[0], 0);
        Bar(needBar[1], needPriority[1], 1);
        Bar(needBar[2], needPriority[2], 2);
        Bar(needBar[3], needPriority[3], 3);

        Destination();

    }
}