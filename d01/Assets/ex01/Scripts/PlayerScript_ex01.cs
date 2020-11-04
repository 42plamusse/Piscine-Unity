using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript_ex01 : MonoBehaviour
{
    public CameraController_ex01 cameraController;
    public KeyCode keycode;
    public KeyCode altKeyCode;
    public float jumpForce;
    public float speed;
    public LayerMask platformLayerMask;
    public float extraHeightLayerMask;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    bool selected = false;
    bool jump = false;
    float horizontalMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(keycode) || Input.GetKeyDown(altKeyCode))
            cameraController.changeFollowedPlayer(this);
        if (cameraController.followedPlayer == this)
        {
            gameObject.layer = 9; //Player
            rb.mass = 1;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            selected = true;
        }
        else
        {
            gameObject.layer = 8; // Platforms
            rb.mass = 10;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX |
                RigidbodyConstraints2D.FreezeRotation;
            selected = false;
        }
        if (!selected) return;
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            jump = true;
        horizontalMove = Input.GetAxis("Horizontal");

    }
    private void FixedUpdate()
    {
        if (!selected) return;
        if (jump)
        {
            Vector3 jumpVelocity =
                Vector3.up * jumpForce * Time.fixedDeltaTime;
            jumpVelocity.x = rb.velocity.x;
            rb.velocity = jumpVelocity;
            //rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

        }
        jump = false;
        Vector3 velocity =
            transform.right * horizontalMove * speed * Time.fixedDeltaTime;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            extraHeightLayerMask,
            platformLayerMask
            );
        return raycastHit2D.collider != null;
    }
}
