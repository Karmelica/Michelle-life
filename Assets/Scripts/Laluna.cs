using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Laluna : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb;
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
    private readonly float danceCd = 1;
    private float phoneCd = 0;
    private Image currentNeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guitar"))
        {
            isWorking = true;
            currentNeed = imgGuitar;
            animator.SetTrigger("Guitar");
            guitarCd = 5f + 3.8f;
        }
        if (other.CompareTag("Pills"))
        {
            isWorking = true;
            currentNeed = imgPills;
            animator.SetTrigger("Pills");
            pillsCd = 10f + 2.3f;
            imgDance.fillAmount -= 0.21f;

        }
        if (other.CompareTag("Dance"))
        {
            isWorking = true;
            currentNeed = imgDance;
            animator.SetTrigger("Dance");
        }
        if (other.CompareTag("Phone"))
        {
            isWorking = true;
            currentNeed = imgPhone;
            animator.SetTrigger("Phone");
            phoneCd = 5f + 21f;
        }
    }

    public void ChangeBool()
    {
        currentNeed.fillAmount = 1f;
        isWorking = false;
    }

    public void Bar(Image bar, float ratio, Transform tr, float cd)
    {
        if (bar.fillAmount > 0.05 && cd < 0)
        {
            bar.fillAmount -= Time.deltaTime * 0.05f / ratio;
        }
        if (bar.fillAmount < 0.33)
        {
            Destination(tr);
        }
    }

    public void Destination(Transform transformNeed)
    {
        Vector3 distance = transformNeed.position - transform.position;

        //Debug.Log(distance.magnitude);

        if (!isWorking)
        {
            if (distance.magnitude > 1f)
            {
                meshAgent.SetDestination(transformNeed.position);
                meshAgent.isStopped = false;
            }
        }
        else
        {
            meshAgent.isStopped = true;
            meshAgent.ResetPath();
        }
    }

    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        if (!isWorking && !meshAgent.hasPath)
        {
            meshAgent.SetDestination(Vector3.zero);
        }

        animator.SetFloat("Vel", meshAgent.velocity.magnitude);

        //Debug.Log(meshAgent.velocity.magnitude);

        Bar(imgGuitar, 1, trGuitar, guitarCd);
        Bar(imgPills, 3, trPills, pillsCd);
        Bar(imgDance, 15, trDance, danceCd);
        Bar(imgPhone, 20, trPhone, phoneCd);

        phoneCd -= Time.deltaTime;
        guitarCd -= Time.deltaTime;
        pillsCd -= Time.deltaTime;

        //Debug.Log(meshAgent.destination);
    }
}