using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
    public Ball ball;
    public float minForce, maxForce;

    public float putForce;

    bool charging = false;

    private void Start()
    {
        putForce = 0f;
    }

    private void Update()
    {
        if (ball.isGameOver) return;
        if (!ball.shot && Input.GetKey(KeyCode.Space))
        {
            putForce = Mathf.Clamp(putForce + 20 * Time.deltaTime, minForce, maxForce);
            charging = true;
            if(putForce < maxForce)
            {
                transform.Translate(-ball.direction * Time.deltaTime);
            }
        } else
        {
            charging = false;
        }
        if (!charging && putForce != 0f)
        {
            this.GetComponent<Renderer>().enabled = false;
            ball.ShootBall(putForce);
            putForce = 0f;
        }
    }

    public void ChangePos()
    {
        transform.position = new Vector3(
            transform.position.x,
            ball.transform.position.y + 0.5f + (-ball.direction.y * .2f),
            transform.position.z);
        this.GetComponent<Renderer>().enabled = true;
    }
}
