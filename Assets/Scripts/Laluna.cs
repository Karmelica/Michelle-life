using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Laluna : MonoBehaviour
{
    [Header("Components")]
    public Camera cam;
    public Rigidbody rb;
    public Animator animator;
    public Image imgGuitar;
    public Image imgPills;
    public Image imgDance;
    public Image imgPhone;
    public TextMeshProUGUI queueText;
    public TextMeshProUGUI queueObjective;
    public LayerMask ground;

    [Header("Objects")]
    public GameObject trGuitar;
    public GameObject trPills;
    public GameObject trDance;
    public GameObject trPhone;
    public GameObject zero;

    private NavMeshAgent meshAgent;
    private bool isWorking;
    private float pillsTime;
    private float guitarCd = 0;
    private float pillsCd = 0;
    private float danceCd = 0;
    private float phoneCd = 0;
    private bool needGuitar, needPills, needDance, needPhone;

    private readonly Queue<GameObject> positionQueue = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guitar"))
        {
            isWorking = true;
            meshAgent.ResetPath();
            StartCoroutine(Refill(imgGuitar, guitarCd = 5f + 3.8f));
            animator.SetTrigger("Guitar");
        }
        if (other.CompareTag("Pills"))
        {
            isWorking = true;
            meshAgent.ResetPath();
            StartCoroutine(Refill(imgPills, pillsCd = 10f + 5f));
            imgDance.fillAmount -= 0.21f;
            animator.SetTrigger("Pills");
            pillsTime = 20f;
            animator.SetBool("OnPills", true);

        }
        if (other.CompareTag("Dance"))
        {
            isWorking = true;
            meshAgent.ResetPath();
            StartCoroutine(Refill(imgDance, 5));
            animator.SetTrigger("Dance");
        }
        if (other.CompareTag("Phone"))
        {
            isWorking = true;
            meshAgent.ResetPath();
            StartCoroutine(Refill(imgPhone, phoneCd = 5f + 21f));
            animator.SetTrigger("Phone");
        }
    }

    public void ChangeBool()
    {
        isWorking = false;
    }

    public IEnumerator Refill(Image need, float cd)
    {
        while (need.fillAmount < 1f && isWorking)
        {
            yield return new WaitForFixedUpdate();
            need.fillAmount += 0.01f / cd * 5f;
        }
    }

    public void Bar(Image bar, float ratio, GameObject newNeed, float cd, ref bool need)
    {
        if (bar.fillAmount > 0.05 && cd < 0)
        {
            bar.fillAmount -= Time.deltaTime * 0.05f / ratio;
        }
        if (bar.fillAmount < 0.33f)
        {
            AddDestiToQueue(newNeed, ref need);
        }
        if (bar.fillAmount >= 0.5f)
        {
            need = false;
        }
    }

    public void AddDestiToQueue(GameObject nextDest, ref bool need)
    {
        if (!positionQueue.Contains(nextDest) && !need)
        {
            need = true;
            positionQueue.Enqueue(nextDest);
            UpdateQueueText();
        }
    }

    private void Walking()
    {
        if (!isWorking)
        {
            if (positionQueue.Count == 0 && !meshAgent.hasPath)
            {
                if (meshAgent.hasPath)
                {
                    meshAgent.ResetPath();
                }
                meshAgent.SetDestination(zero.transform.position);
                
            }
            if (positionQueue.Count > 0 && !meshAgent.hasPath)
            {
                meshAgent.SetDestination(positionQueue.Dequeue().transform.position);

                UpdateQueueText();
            }
        }
    }

    private void UpdateQueueText()
    {
        queueText.text = string.Empty;

        foreach (GameObject gamObj in positionQueue)
        {
            queueText.text += gamObj.name + ", ";
        }

    }

    void Start()
    {
        //colli = GetComponent<Collider>();
        meshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000, ground))
            {
                Vector3 pos = new(hit.point.x, 1f, hit.point.z);
                zero.transform.position = pos;
            }
        }

        Walking();

        animator.SetFloat("Vel", meshAgent.velocity.magnitude);

        Bar(imgGuitar, 1, trGuitar, guitarCd, ref needGuitar);
        Bar(imgPills, 3, trPills, pillsCd, ref needPills);
        Bar(imgDance, 20, trDance, danceCd, ref needDance);
        Bar(imgPhone, 15, trPhone, phoneCd, ref needPhone);

        pillsTime -= Time.deltaTime;
        phoneCd -= Time.deltaTime;
        guitarCd -= Time.deltaTime;
        pillsCd -= Time.deltaTime;
        danceCd -= Time.deltaTime;

        if(pillsTime <= 0)
        {
            animator.SetBool("OnPills", false);
        }
    }

}