using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int m_PlayersNbr;
    public int m_PlayersReadyExit;
    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        m_PlayersReadyExit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            GameOver();
        if (m_PlayersReadyExit == m_PlayersNbr || Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    public void SetPlayersReadyExit(int newPlayersReadyExit)
    {
        m_PlayersReadyExit = newPlayersReadyExit;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
