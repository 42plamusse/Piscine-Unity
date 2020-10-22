using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject prefab;
    public KeyCode keyCode;
    public Transform bottomLine;
    public Transform topLine;

    private GameObject cube;
    private float timeBeforeNextSpawn;
    private float precision;
    private void Update()
    {
        if (!cube && timeBeforeNextSpawn <= 0)
        {
            cube = Instantiate(prefab);
            timeBeforeNextSpawn = Random.Range(2.0f, 3.0f);
        }
        if (
            cube &&
            cube.transform.position.y < topLine.localPosition.y &&
            (Input.GetKeyDown(keyCode) ||
            cube.transform.position.y < bottomLine.localPosition.y - 1f))
        {
            Destroy(cube);
            precision = cube.transform.position.y - bottomLine.localPosition.y;
            if (precision >= 0) print("Precision: " + precision);
            else print("Missed key: " + keyCode);
        }
        timeBeforeNextSpawn -= Time.deltaTime;
    }
}
