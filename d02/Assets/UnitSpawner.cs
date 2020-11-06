using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unit;
    public float spawnRate;

    float nextSpawn;
    // Start is called before the first frame update
    void Start()
    {
        nextSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            Instantiate(unit, transform);
            nextSpawn = Time.time + spawnRate;
        }
    }
}
