using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Club club;
    public Transform hole;
    public float inertia;

    public bool shot = false;
    public Vector3 direction = Vector3.up;


    float force;
    float timeSinceShot;
    float ballSpeed;

    private void Update()
    {
        if (transform.position.y >= 4.7 || transform.position.y <= -4.7 ){
            transform.Translate(-direction * ballSpeed);
            direction *= -1;
        }
    }
    private void FixedUpdate()
    {
        if (shot && force != 0)
        {
            timeSinceShot += Time.fixedDeltaTime;
            ballSpeed = force / inertia / timeSinceShot * Time.fixedDeltaTime;
            if (ballSpeed > 0.01f)
            {
                transform.Translate(direction * ballSpeed);
            }
            else
            {
                timeSinceShot = 0;
                shot = false;
                if (transform.position.y < hole.position.y) direction = Vector3.up;
                else direction = Vector3.down;
                force = 0f;
                club.ChangePos(); 
            }
        }
    }

    public void ShootBall(float putForce)
    {
        shot = true;
        force = putForce;
    }
}
