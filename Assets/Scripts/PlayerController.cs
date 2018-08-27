using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float sensitivity_x;
    public float sensitivity_y;

    Transform cam;
    float y_rotation;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        y_rotation = 0;
        rb = GetComponent<Rigidbody>();
        cam = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        MovementControl();
    }

    private void LateUpdate()
    {
        RotateControl();
    }

    void MovementControl()
    {
        float inx = Input.GetAxis("Horizontal");
        float inz = Input.GetAxis("Vertical");

        Vector3 velocity = (inx * transform.right + inz * transform.forward) * speed + rb.velocity.y * Vector3.up;
        rb.velocity = velocity;
    }

    void RotateControl()
    {
        float iny = Input.GetAxis("Mouse X");
        float inx = Input.GetAxis("Mouse Y");

        y_rotation += iny;
        if (y_rotation > 360)
            y_rotation -= 360;
        else if (y_rotation < 0)
            y_rotation += 360;
        transform.eulerAngles = new Vector3(0, y_rotation, 0);
        Vector3 camrotation = cam.localEulerAngles;

        if (Mathf.Abs(camrotation.x - inx * sensitivity_y) < 90 || Mathf.Abs(camrotation.x - inx * sensitivity_y) > 270)
        {
            cam.localEulerAngles = new Vector3(camrotation.x - inx * sensitivity_y, 0, 0);
        }
    }

}
