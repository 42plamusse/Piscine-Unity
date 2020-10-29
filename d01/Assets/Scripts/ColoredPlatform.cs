using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredPlatform : MonoBehaviour
{
    public Collider2D[] ignored = new Collider2D[2];
    Collider2D platformCollider;
    // Start is called before the first frame update
    void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(ignored[0], platformCollider);
        Physics2D.IgnoreCollision(ignored[1], platformCollider);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
