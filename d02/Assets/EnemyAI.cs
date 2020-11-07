using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public CityHallAI cityHallAI;
    public EnemyController controller;
    GameObject[] humanUnits;
    public bool defending = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!defending || gameObject.GetComponent<EnemyController>().target == null)
            FindClosestTarget();
    }
    void FindClosestTarget()
    {
        humanUnits = GameObject.FindGameObjectsWithTag("Human");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject human in humanUnits)
        {
            Vector3 diff = human.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = human;
                distance = curDistance;
            }
        }
        GetComponent<EnemyController>().target = closest;

    }

    public void Defend()
    {
        defending = true;
    }
    public void Attack()
    {
        defending = false;
    }
}
