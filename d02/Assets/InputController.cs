using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public AudioClip move;
    List<UnitController> footmen = new List<UnitController>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (!hit) return;
            if (hit.collider.CompareTag("Human"))
            {
                UnitController footman =
                    hit.collider.gameObject.GetComponent<UnitController>();
                if (footman == null) return;
                if (!Input.GetKey(KeyCode.LeftControl))
                {
                    footmen.Clear();
                }
                if (footmen.IndexOf(footman) < 0)
                {
                    footmen.Add(footman);
                    footman.selected = true;
                    GetComponent<AudioSource>().Play();
                }
            }
            else if (hit.collider.CompareTag("Orc"))
                Attack(hit.collider.gameObject);
            else if (hit.collider.CompareTag("Tile"))
            {
                MoveSelection(worldPos);
            }

        }
        if (Input.GetMouseButtonDown(1))
            footmen.Clear();
    }

    void MoveSelection(Vector3 worldPos)
    {
        bool soundPlayed = false;
        foreach (UnitController footman in footmen)
        {
            if (!soundPlayed)
            {
                footman.source.clip = move;
                footman.source.Play();
                soundPlayed = true;
            }
            Vector3 targetDir = worldPos - footman.transform.position;
            footman.UpdateAnimator(targetDir);
            footman.moveTowardsPos = worldPos;
            footman.target = null;
            footman.fighting = false;
        }
    }

    void Attack(GameObject target) {
        foreach (UnitController footman in footmen)
        {
            footman.fighting = false; // reset
            footman.animator.SetBool("Attack", false); // reset
            footman.target = target;
        }
    }
}
