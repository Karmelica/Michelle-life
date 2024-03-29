using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Laluna : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Image imgGuitar;
    public Image imgPills;
    public Image imgDance;
    public Image imgPhone;


    [Header("Objects")]
    public Transform trGuitar;
    public Transform trPills;
    public Transform trDance;
    public Transform trPhone;

    private NavMeshAgent meshAgent;
    private bool isWorking;
    private float guitarCd = 0;
    private float pillsCd = 0;
    private float danceCd = 0;
    private float phoneCd = 0;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Guitar"))
        {
            if(imgGuitar.fillAmount < 0.99f)
            {
                isWorking = true;
                imgGuitar.fillAmount += Time.deltaTime * 0.25f;
                animator.SetTrigger("Guitar");
            }
            else
            {
                guitarCd = 5f;
                isWorking = false;
            }
        }
        if (other.CompareTag("Pills"))
        {
            if(imgPills.fillAmount < 0.99f)
            {
                isWorking = true;
                imgPills.fillAmount += Time.deltaTime;
                animator.SetTrigger("Pills");
            }
            else
            {
                pillsCd = 5f;
                isWorking = false;
            }
        }
        if (other.CompareTag("Dance"))
        {
            if (imgDance.fillAmount < 0.99f)
            {
                isWorking = true;
                imgDance.fillAmount += Time.deltaTime * 0.2f;
                animator.SetTrigger("Dance");
            }
            else
            {
                danceCd = 5f;
                isWorking = false;
            }
        }
        if (other.CompareTag("Phone"))
        {
            if (imgPhone.fillAmount < 0.99f)
            {
                isWorking = true;
                imgPhone.fillAmount += Time.deltaTime * 0.02f;
                animator.SetTrigger("Phone");
            }
            else
            {
                phoneCd = 5f;
                isWorking = false;
            }
        }
    }

    public void Bar(Image bar, float ratio, Transform tr, float cd)
    {
        if(bar.fillAmount > 0.05 && !isWorking && cd < 0)
        {
            bar.fillAmount -= Time.deltaTime * 0.05f / ratio;
        }
        if(bar.fillAmount < 0.33)
        {
            Destination(tr);
        }
    }

    public void Destination(Transform transformNeed)
    {
        Vector3 distance = transformNeed.position - transform.position;

        Debug.Log(distance.magnitude);

        if (!isWorking)
        {
            if (distance.magnitude > 1f)
            {
                meshAgent.SetDestination(transformNeed.position);
                meshAgent.isStopped = false;
                animator.SetBool("isStopped", false);
            }
        }
        else
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
        Bar(imgGuitar, 1, trGuitar, guitarCd);
        Bar(imgPills, 5, trPills, pillsCd);
        Bar(imgDance, 15, trDance, danceCd);
        Bar(imgPhone, 20, trPhone, phoneCd);

        phoneCd -= Time.deltaTime;
        guitarCd -= Time.deltaTime;
        danceCd -= Time.deltaTime;
        pillsCd -= Time.deltaTime;
    }
}