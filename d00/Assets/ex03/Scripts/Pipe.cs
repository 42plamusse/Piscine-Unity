using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Pipe otherPipe;
    public Bird bird;
    public float speed;
    public Vector3 initialPos;
    public Vector3 offset;

    float collesionStartx;
    bool birdInside;
    bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = initialPos + offset;
        collesionStartx = 1.1f;
        birdInside = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
        transform.Translate(Vector3.left * bird.pipeSpeed * Time.deltaTime);
        if (transform.position.x < -initialPos.x) transform.position = initialPos;
        if (transform.position.x < collesionStartx &&
            transform.position.x > -collesionStartx)
        {
            birdInside = true;
            float birdY = bird.gameObject.transform.position.y;
            if (birdY >= 1.76 || birdY <= -1.12)
            {
                gameOver = true;
                bird.GameOver();
                otherPipe.GameOver();
            }
        }
        if (birdInside && transform.position.x < -collesionStartx)
        {
            bird.UpdateScore();
            birdInside = false;

        }
    }

    public void GameOver()
    {
        gameOver = true;
    }
}
