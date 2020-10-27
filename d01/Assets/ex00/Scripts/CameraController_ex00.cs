using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraController_ex00 : MonoBehaviour
{
    public PlayerScript_ex00 followedPlayer;

    void Update()
    {
        Vector3 newPos = followedPlayer.transform.position;
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void changeFollowedPlayer(PlayerScript_ex00 player)
    {
        followedPlayer = player;
    }
}
