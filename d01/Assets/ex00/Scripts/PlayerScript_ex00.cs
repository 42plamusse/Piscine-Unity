using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript_ex00 : MonoBehaviour
{
    public CameraController cameraController;
    public KeyCode keycode;
    public float jumpForce;
    public float speed;

    Rigidbody2D rb;
    bool selected = false;
    bool jump = false;
    float horizontalMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(keycode))
            cameraController.changeFollowedPlayer(this);
        if (cameraController.followedPlayer == this) selected = true;
        else selected = false;
        if (!selected) return;
        print(keycode);
        if (Input.GetKeyDown(KeyCode.Space)) jump = true;
        horizontalMove = Input.GetAxis("Horizontal");

    }
    private void FixedUpdate()
    {
        if (jump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }
        else
        {
            rb.MovePosition(transform.position +
                transform.right * horizontalMove * speed * Time.fixedDeltaTime);
        }
    }
}
