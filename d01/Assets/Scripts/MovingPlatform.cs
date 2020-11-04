using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float distance;

    Vector3 initialPos;
    Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        endPos = initialPos + direction * distance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            endPos, speed * Time.deltaTime);
        if(transform.position == endPos)
        {
            Vector3 savePos = endPos;
            endPos = initialPos;
            initialPos = savePos;
        }
    }
}
