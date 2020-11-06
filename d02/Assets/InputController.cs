using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //public UnitController footman;
    List<UnitController> footmen = new List<UnitController>();
    // Start is called before the first frame update
    void Start()
    {
        //footmen.Add(footman);
        //footman.selected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (!hit.collider) return;
            if (hit.collider.CompareTag("Footman")) {
                if (!Input.GetKey(KeyCode.LeftControl)) {
                    footmen.Clear();
                }
                UnitController footman =
                    hit.collider.gameObject.GetComponent<UnitController>();
                if(footmen.IndexOf(footman) < 0)
                {
                    footmen.Add(footman);
                    footman.selected = true;
                }
            }
            else if (hit.collider.CompareTag("Tile")){
                MoveSelection(worldPos);
            }
        }
        if (Input.GetMouseButtonDown(1))
            footmen.Clear();
    }

    void MoveSelection(Vector3 worldPos)
    {
        foreach (UnitController footman in footmen)
        {
            Vector3 targetDir = worldPos - footman.transform.position;
            footman.UpdateAnimator(targetDir);
            footman.source.Play();
            footman.moveTowardsPos = worldPos;
        }
    }
}
