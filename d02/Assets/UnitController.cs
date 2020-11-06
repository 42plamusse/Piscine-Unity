using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public float speed;
    public bool selected;
    public Vector3 moveTowardsPos;
    public AudioSource source;

    Animator animator;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        moveTowardsPos = transform.position;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (selected && transform.position != moveTowardsPos)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                moveTowardsPos, speed * Time.deltaTime);
        }
        else
            UpdateAnimator(Vector3.zero);

    }
    public void UpdateAnimator(Vector3 direction)
    {
        float angle = Vector3.Angle(direction, transform.right);
        int WalkX, WalkY = 0;
        if (angle <= 45 && angle != 0)
        {
            // going right
            WalkX = 1;
            spriteRenderer.flipX = false;

        }
        else if (angle >= 135)
        {
            //walking left
            WalkX = -1;
            spriteRenderer.flipX = true;
        }
        else
        {
            WalkX = 0;
            spriteRenderer.flipX = false;
            WalkY = direction.y < 0 ? -1 :
                direction.y > 0 ? 1 : 0;
        }
        if (animator)
        {
            animator.SetInteger("WalkX", WalkX);
            animator.SetInteger("WalkY", WalkY);
        }
    }
}
