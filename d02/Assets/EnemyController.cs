using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public Vector3 moveTowardsPos;
    public AudioSource source;
    public AudioClip hit;
    public GameObject target;
    public bool fighting = false;
    public float hitRate;
    public float hitDamage;
    public float hp;
    public Animator animator;

    public float initialHp;
    SpriteRenderer spriteRenderer;
    float elapsed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        initialHp = hp;
        moveTowardsPos = transform.position;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!fighting && transform.position != moveTowardsPos)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                moveTowardsPos, speed * Time.deltaTime);
        }
        else
            UpdateAnimator(Vector3.zero);
        if (target)
        {
            moveTowardsPos = target.transform.position;
            if (transform.position != moveTowardsPos)
                UpdateAnimator(moveTowardsPos - transform.position);
            elapsed += Time.deltaTime;

            if (fighting && elapsed >= hitRate)
            {
                source.clip = hit;
                source.Play();
                UnitController enemy =
                    target.gameObject.GetComponent<UnitController>();
                Building building =
                    target.gameObject.GetComponent<Building>();
                if (building)
                {
                    building.hp -= hitDamage;
                    print(building.tag + " Building [" + building.hp +
                        "/" + building.initialHp + "]HP has been attacked.");
                }
                else if (enemy)
                {
                    enemy.hp -= hitDamage;
                    print( enemy.tag + " Unit [" + enemy.hp +
                        "/" + enemy.initialHp + "]HP has been attacked.");
                }
                elapsed = 0f;
            }

        }

        if (hp <= 0) Destroy(gameObject);

    }
    public void UpdateAnimator(Vector3 direction)
    {
        if (animator == null) return;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (target &&
            collision == target.GetComponent<Collider2D>() &&
            collision.CompareTag("Human"))
        {
            animator.SetBool("Attack", true);
            fighting = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Human"))
        {
            animator.SetBool("Attack", false);
            fighting = false;
        }
    }
}
