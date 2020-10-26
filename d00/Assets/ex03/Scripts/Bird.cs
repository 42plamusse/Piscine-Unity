using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float speed;
    public float pipeSpeed = 1f;
    public float jumpForce;
    public float addJumpTime;

    float jumpTime;
    int score = 0;
    float timeAlive = 0f;
    bool gameOver = false;


    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (transform.position.y < -2.9f) gameOver = true;

        if (gameOver) return;

        if (transform.position.y < 4f &&
            jumpTime <=0 &&
            Input.GetKeyDown(KeyCode.Space))
        {
            jumpTime = addJumpTime;
        }
        else if(jumpTime > 0)
        {
            jumpTime -= Time.deltaTime;
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * speed *  Time.deltaTime);
        }
    }
    public void GameOver()
    {
        gameOver = true;
        print("Score: " + score);
        print("Time: " + Mathf.RoundToInt(timeAlive));
    }
    public void UpdateScore()
    {
        score += 5;
        pipeSpeed += 0.5f;
    }
}
