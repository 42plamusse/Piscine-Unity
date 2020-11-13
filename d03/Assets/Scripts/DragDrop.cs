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
    public KeyCode keyCode;
    public KeyCode keyCodeAlt;
    private RectTransform rectTransform;
    private Vector2 initialPos;

    private int towerCost;
    private bool followMouse = false;
    private bool dragging = false;
    SpriteRenderer spriteRenderer;
    public Transform rangeCircle;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPos = rectTransform.anchoredPosition;
    }

    private void Start()
    {
        towerScript towerScript = tower.GetComponent<towerScript>();
        towerCost = towerScript.energy;
        spriteRenderer = GetComponent<SpriteRenderer>();
        print(rangeCircle.localScale);
        rangeCircle.localScale *= towerScript.range / 2;
    }

    private void Update()
    {
        if (towerCost > gameManager.gm.playerEnergy)
            spriteRenderer.color = Color.gray;
        else
            spriteRenderer.color = Color.white;
        if (Input.GetKeyDown(keyCode) || Input.GetKeyDown(keyCodeAlt))
            followMouse = true;
        else if (Input.anyKeyDown || gameManager.gm.ended)
        {
            if (Input.GetMouseButtonDown(0) && followMouse)
                putTower();
            else
            {
                dragging = false;
                followMouse = false;
            }
            rectTransform.anchoredPosition = initialPos;
        }
        if (followMouse)
            FollowingMouse();
        rangeCircle.gameObject.SetActive(followMouse || dragging);
    }

    void putTower()
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
    }
    void FollowingMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        rectTransform.transform.position = worldPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (towerCost >
            gameManager.gm.playerEnergy || followMouse)
        {
            eventData.pointerDrag = null;
        }
        else
            dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging || followMouse)
            eventData.pointerDrag = null;
        else
            rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
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
