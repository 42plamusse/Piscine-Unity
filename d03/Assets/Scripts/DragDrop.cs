using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour,
    IBeginDragHandler,
    IEndDragHandler,
    IDragHandler
{
    public gameManager gameManager;
    public GameObject tower;
    private RectTransform rectTransform;
    private Vector2 initialPos;

    private int towerCost;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPos = rectTransform.anchoredPosition;
    }

    private void Start()
    {
        towerCost = tower.GetComponent<towerScript>().energy;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (towerCost > gameManager.gm.playerEnergy)
            spriteRenderer.color = Color.gray;
        else
            spriteRenderer.color = Color.white;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (towerCost >
            gameManager.gm.playerEnergy)
        {
            eventData.pointerDrag = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPos, Vector2.zero);
        bool canPlace = false;
        Transform emptySpot = default;
        foreach (RaycastHit2D hit in hits)
        {
            Collider2D collider = hit.collider;
            if (collider.gameObject.CompareTag("tower"))
            {
                canPlace = false;
                break;
            }
            else if (collider.gameObject.CompareTag("empty"))
            {
                emptySpot = collider.gameObject.transform;
                canPlace = true;
            }
        }
        if (canPlace && towerCost <=
            gameManager.gm.playerEnergy)
        {
            Instantiate(tower, emptySpot.position, Quaternion.identity);
            gameManager.gm.playerEnergy -= towerCost;
        }
        rectTransform.anchoredPosition = initialPos;
    }
}
