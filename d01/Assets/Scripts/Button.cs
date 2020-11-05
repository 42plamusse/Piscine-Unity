using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Transform rotationAnchor;
    public Collider2D gateCollider;
    public float zAxisRotation;

    bool isPressed = false;
    bool rotate = false;
    float initialZaxisRotation;
    // Start is called before the first frame update
    void Start()
    {
        initialZaxisRotation = rotationAnchor.transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            gateCollider.enabled = false;
            if (isPressed &&
                rotationAnchor.transform.eulerAngles.z == initialZaxisRotation)
                rotationAnchor.Rotate(0f, 0f, zAxisRotation, Space.World);
            else if (rotationAnchor.transform.eulerAngles.z ==
                initialZaxisRotation + zAxisRotation)
                rotationAnchor.Rotate(0f, 0f, -zAxisRotation, Space.Self);
            rotate = false;
            gateCollider.enabled = true;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isPressed) {
            isPressed = true;
            rotate = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
            isPressed = false;
            rotate = true;
    }
}
