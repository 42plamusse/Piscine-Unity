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
    public bool isGameOver = false;


    float force;
    float timeSinceShot;
    float ballSpeed;
    int score = -15;

    private void Start()
    {
        print("Score: " + score);
    }
    private void Update()
    {
        if (isGameOver) return;
        if (transform.position.y >= 4.7 || transform.position.y <= -4.7 ){
            transform.Translate(-direction * ballSpeed);
            direction *= -1;
        }
        IsInHole();
    }

    private void FixedUpdate()
    {
        if (isGameOver) return;
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
                score += 5;
                print("Score: " + score);

            }
        }
    }

    void IsInHole()
    {
        if(transform.position.y > 3.8 && transform.position.y < 4.2)
        {
            if(ballSpeed < 0.02f)
            {
                if (score > 0) print("You Lose !");
                else print("You win !");
                print("Final score: " + score);
                isGameOver = true;
            }
        }
    }

    public void ShootBall(float putForce)
    {
        shot = true;
        force = putForce;
    }
}
