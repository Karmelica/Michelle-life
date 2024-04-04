using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI queueText;
    public TextMeshProUGUI queueObjective;

    [Header("Objects")]
    public GameObject trGuitar;
    public GameObject trPills;
    public GameObject trDance;
    public GameObject trPhone;
    public GameObject zero;

    private NavMeshAgent meshAgent;
    [SerializeField] private bool isWorking;
    private float guitarCd = 0;
    private float pillsCd = 0;
    private readonly float danceCd = 1;
    private float phoneCd = 0;

    private readonly Queue<GameObject> positionQueue = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guitar"))
        {
            isWorking = true;
            StartCoroutine(Refill(imgGuitar, guitarCd = 5f + 3.8f));
            animator.SetTrigger("Guitar");
        }
        if (other.CompareTag("Pills"))
        {
            isWorking = true;
            StartCoroutine(Refill(imgPills, pillsCd = 10f + 2.3f));
            imgDance.fillAmount -= 0.21f;
            animator.SetTrigger("Pills");

        }
        if (other.CompareTag("Dance"))
        {
            isWorking = true;
            StartCoroutine(Refill(imgDance, 5));
            animator.SetTrigger("Dance");
        }
        if (other.CompareTag("Phone"))
        {
            isWorking = true;
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

    public void Bar(Image bar, float ratio, GameObject newNeed, float cd)
    {
        if (bar.fillAmount > 0.05 && cd < 0)
        {
            bar.fillAmount -= Time.deltaTime * 0.05f / ratio;
        }
        if (bar.fillAmount < 0.33)
        {
            AddDestiToQueue(newNeed);
        }
    }

    public void AddDestiToQueue(GameObject nextDest)
    {
        if(!positionQueue.Contains(nextDest))
        {
            positionQueue.Enqueue(nextDest);
            UpdateQueueText();
        }
    }

    private void UpdateQueueText()
    {
        queueText.text = string.Empty;

        foreach(GameObject gamObj in positionQueue) {
            queueText.text += gamObj.name + ", ";
        }
    }

    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!isWorking)
        {
            if (positionQueue.Count == 0)
            {
                AddDestiToQueue(zero);
            }

            if (positionQueue.Count > 0 && !meshAgent.hasPath)
            {
                GameObject amogus = positionQueue.Dequeue();

                queueObjective.text = "Current objective: " + amogus.name;

                meshAgent.SetDestination(amogus.transform.position);

                UpdateQueueText();
            }
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