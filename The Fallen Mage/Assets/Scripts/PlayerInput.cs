using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed = 5f;
    public float runningSpeed = 10f;
    public Animation anim;
    public Rigidbody rb;
    public Transform cameraLookPoint;
    float forward;
    float sideways;
    Vector3 movement;
    Vector2 mouseInp = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        forward = Input.GetAxisRaw("Vertical");
        sideways = Input.GetAxisRaw("Horizontal");
        movement = new Vector3(sideways, 0f, forward);
        bool running = Input.GetKey(KeyCode.LeftShift);
        if (running)
        {
            anim.Play("Run");
        }
        else if (forward != 0 || sideways != 0 && !running)
        {
            anim.Play("Walk");
        }
        
        
        movement = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f) * movement;
        movement *= running ? runningSpeed : speed;
        rb.velocity = movement;
        //rb.AddForce(movement, ForceMode.VelocityChange);
        ControlCamera();
    }

    void ControlCamera()
    {
        mouseInp = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        transform.Rotate(transform.up, mouseInp.x);
        //cameraLookPoint.Rotate(Vector3.right * -mouseInp.y);
        //transform.Rotate(Vector3.right * -mouseInp.y);
    }
}
