using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController_ex01 : MonoBehaviour
{
    public PlayerScript_ex01 followedPlayer;

    void Update()
    {
        Vector3 newPos = followedPlayer.transform.position;
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }

    public void changeFollowedPlayer(PlayerScript_ex01 player)
    {
        followedPlayer = player;
    }
}
