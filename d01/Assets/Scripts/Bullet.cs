using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    public Color color;
    public Vector3 direction;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = transform.parent.position;
        GetComponent<SpriteRenderer>().color =
            new Color(color.r, color.g, color.b);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
            gameManager.GameOver();
        else if (collision.gameObject.layer == 10)
            Destroy(gameObject);
    }

}
