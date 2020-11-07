using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public UnitSpawner unitSpawner;
    public float hp;
    public bool isCityHall = false;
    public float initialHp;

    float timeSinceDead;
    bool playing;
    // Start is called before the first frame update
    void Start()
    {
        initialHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            timeSinceDead += Time.deltaTime;
            Death();
        }
    }
    void Death()
    {

        if (!playing)
            GetComponent<AudioSource>().Play();
        playing = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        if (timeSinceDead >= 1.0f)
        {
            if (isCityHall)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                if (unitSpawner.gameObject.CompareTag("Orc"))
                    print("The Human Team wins");
                else
                    print("The Orc Team wins");
                Time.timeScale = 0;
            }
            else
                unitSpawner.spawnRate += 2.5f;
            Destroy(gameObject);

        }
    }
}
