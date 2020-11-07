using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHallAI : MonoBehaviour
{
    int unitsAttacking = 0;
    int unitsDefending = 0;
    GameObject lastDefender;
    GameObject lastAttacker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (unitsAttacking > unitsDefending)
        {
            FindClosestDefender();
        }
    }

    void FindClosestDefender()
    {
        GameObject[] orcs = GameObject.FindGameObjectsWithTag("Orc");
        if (orcs.Length == 0) return;
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject orc in orcs)
        {
            if (orc.GetComponent<EnemyController>() != null &&
                orc.GetComponent<EnemyAI>().defending == false)
            {

            Vector3 diff = orc.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = orc;
                distance = curDistance;
            }
            }
        }
        closest.GetComponent<EnemyAI>().Defend();
        EnemyController controller = closest.GetComponent<EnemyController>();
        controller.fighting = false; // reset
        controller.animator.SetBool("Attack", false); // reset
        controller.target = lastAttacker;
        lastDefender = closest;
        unitsDefending++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Human"))
        {
            lastAttacker = collision.gameObject;
            unitsAttacking++;
            print(unitsAttacking);
        }
    }   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Human"))
        {
            print("EXIT");
            unitsAttacking--;
            lastDefender.GetComponent<EnemyAI>().Attack();
        }
    }
}
