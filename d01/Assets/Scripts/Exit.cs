using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameManager m_Manager;
    public Transform m_Player;

    BoxCollider2D m_Collider;
    Vector3 m_Point;
    bool m_ReadyToExit;
    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<BoxCollider2D>();
        m_ReadyToExit = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_Point = m_Player.position;
        if (!m_ReadyToExit && m_Collider.bounds.Contains(m_Point))
        {
            m_ReadyToExit = true;
            m_Manager.SetPlayersReadyExit(m_Manager.m_PlayersReadyExit + 1);
        }
        else if (m_ReadyToExit && !m_Collider.bounds.Contains(m_Point))
        {
            m_ReadyToExit = false;
            m_Manager.SetPlayersReadyExit(m_Manager.m_PlayersReadyExit - 1);
        }
    }
}
