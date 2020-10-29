using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public PlayerScript followedPlayer;

    void Update()
    {
        Vector3 newPos = followedPlayer.transform.position;
        transform.position = new Vector3(newPos.x, newPos.y + 1, transform.position.z);
    }

    public void changeFollowedPlayer(PlayerScript player)
    {
        followedPlayer = player;
    }
}
