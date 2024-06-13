using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotator : MonoBehaviour
{
    private Transform cameraPos;

    private void Start()
    {
        cameraPos = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cameraPos.position);
    }
}
