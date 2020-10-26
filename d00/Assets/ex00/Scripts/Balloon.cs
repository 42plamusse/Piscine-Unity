using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float blowForce = .02f;
    public float losesAir = .5f;
    public float maxTimeBlowing = 1.0f;
    public float minBaloonScale = 0.2f;
    public float maxBaloonScale = 3f;


    bool blow = false;
    bool started = false;
    float timeSinceStartInSeconds = 0;
    float timeBlowing = 0f;

    private void Update()
    { 

        if (!blow && Input.GetKeyDown("space"))
        {
            if (!started) started = true;
            blow = true;
        }
        if (!started) return;
        timeSinceStartInSeconds += Time.deltaTime;
        if (transform.localScale.x < minBaloonScale ||
            transform.localScale.x > maxBaloonScale)
        {
            print("Balloon life time : " +
                Mathf.RoundToInt(timeSinceStartInSeconds).ToString() + "s");
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!started) return;
        if(blow && timeBlowing < maxTimeBlowing)
        {
            timeBlowing += Time.fixedDeltaTime;
            transform.localScale = new Vector3(transform.localScale.x + blowForce,
                transform.localScale.y + blowForce, transform.localScale.z);
        }
        else
        {
            timeBlowing = 0f;
            blow = false;
            transform.localScale = new Vector3(transform.localScale.x - losesAir,
               transform.localScale.y - losesAir, transform.localScale.z);
        }
    }
}
