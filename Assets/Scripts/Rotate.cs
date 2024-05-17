using System.Net;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform Michelle;
    private float mouseX;
    private float mouseY;
    public Camera cam;

    private void CameraInput()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = -Input.GetAxis("Mouse Y");
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Michelle.position;

        if (Input.GetMouseButton(1))
        {
            CameraInput();
            transform.Rotate(Vector3.up, mouseX * 5f, Space.World);

            transform.Rotate(Vector3.right, mouseY * 5f);

            //transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.eulerAngles.x, -20f, 60f), transform.eulerAngles.y, transform.eulerAngles.z);
            //20, -67
        }

        float zoom = Input.GetAxis("Mouse ScrollWheel") * 20f;

        cam.fieldOfView -= zoom;

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 25, 80);


        Ray ray = new Ray(transform.position, transform.forward * -3f);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            cam.transform.localPosition = new Vector3(0, 0, -hit.distance);
        }
        else
        {
            cam.transform.localPosition = new Vector3(0, 0, -3f);
        }
    }
}
