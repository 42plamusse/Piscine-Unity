using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unzoom : MonoBehaviour
{
    public float maxSize = 7f;
    public float speed = 1f;
    public Camera m_Camera;

    float initialSize;
    bool unzoom;
    CameraController cameraController;
    Collider2D cameraCollider;
    void Start()
    {
        unzoom = false;
        initialSize = m_Camera.orthographicSize;
        cameraController = m_Camera.GetComponent<CameraController>();
        cameraCollider = cameraController.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unzoom)
        {
            if (m_Camera.orthographicSize < maxSize)
                m_Camera.orthographicSize += (speed * Time.deltaTime);
        }
        else
        {
            if (m_Camera.orthographicSize > initialSize)
                m_Camera.orthographicSize -= (speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == cameraCollider)
            unzoom = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == cameraCollider)
            unzoom = false;
    }
}
