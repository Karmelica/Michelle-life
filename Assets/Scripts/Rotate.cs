using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform Michelle;
    private float mouseX;

    private void CameraInput()
    {
        mouseX = Input.GetAxis("Mouse X");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Michelle.position;

        if(Input.GetMouseButton(1))
        {
            CameraInput();
            transform.Rotate(Vector3.up, mouseX * 5f);
        }

    }
}
