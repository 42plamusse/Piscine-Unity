using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public Color color;
    public Vector3 direction;
    public float speed;
    public float timeInterval;

    float timeNextBullet;
    // Start is called before the first frame update
    void Start()
    {
        timeNextBullet = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeNextBullet)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.speed = speed;
            bulletScript.direction = direction;
            bulletScript.player = player;
            bulletScript.color = transform.parent.
                GetComponent<SpriteRenderer>().color;
            timeNextBullet = Time.time + timeInterval;
        }
    }
}
