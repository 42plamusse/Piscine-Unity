using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public Vector3 startPos;

    private float speed;
    private void Awake()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        transform.localPosition = startPos;
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * speed * Time.fixedDeltaTime);
    }
}