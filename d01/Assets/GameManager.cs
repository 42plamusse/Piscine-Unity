using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int m_PlayersNbr;
    public int m_PlayersReadyExit;
    // Start is called before the first frame update
    void Start()
    {
        m_PlayersReadyExit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (m_PlayersReadyExit == m_PlayersNbr)
        {
            print("YOU WIN !!!");
        }
    }

    public void SetPlayersReadyExit(int newPlayersReadyExit)
    {
        m_PlayersReadyExit = newPlayersReadyExit;
    }
}
