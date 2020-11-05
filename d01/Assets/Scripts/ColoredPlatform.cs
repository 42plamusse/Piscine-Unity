using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredPlatform : MonoBehaviour
{
    public GameObject[] ignored = new GameObject[2];
    Collider2D platformCollider;
    // Start is called before the first frame update
    void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(ignored[0].GetComponent<Collider2D>(),
            platformCollider);
        Physics2D.IgnoreCollision(ignored[1].GetComponent<Collider2D>(),
            platformCollider);
    }
}
