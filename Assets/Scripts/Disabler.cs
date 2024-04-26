using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    public MeshRenderer m_MeshRenderer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CameraColi"))
        {
            m_MeshRenderer.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CameraColi"))
        {
            m_MeshRenderer.enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }
}
