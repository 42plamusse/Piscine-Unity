using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Camera m_Camera;
    public PlayerScript followedPlayer;
    public float maxSize = 7f;
    public float speed = 1f;

    float initialSize;
    bool toggleZoom = true;
    void Start()
    {
        initialSize = m_Camera.orthographicSize;
    }
    void Update()
    {
        Vector3 newPos = followedPlayer.transform.position;
        transform.position = new Vector3(newPos.x, newPos.y + 1, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Z))
            toggleZoom = !toggleZoom;

        if (toggleZoom)

        {
            if (m_Camera.orthographicSize > initialSize)
                m_Camera.orthographicSize -= (speed * Time.deltaTime);
        }
        else
        {
            if (m_Camera.orthographicSize < maxSize)
                m_Camera.orthographicSize += (speed * Time.deltaTime);
        }

    }

    public void changeFollowedPlayer(PlayerScript player)
    {
        followedPlayer = player;
    }
}
