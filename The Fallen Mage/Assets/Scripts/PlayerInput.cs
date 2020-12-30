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

    void Update()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float sideways = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(sideways, 0f, forward);

        bool running = Input.GetKey(KeyCode.LeftShift);
        if (forward != 0 || sideways != 0)
        {
            if (running)
            {
                anim.Play("Run");
            }
            else
            {
                anim.Play("Walk");
            }
        }
        else
        {
            anim.Play("idle");
        }
        
        movement *= (running ? runningSpeed : speed) * Time.deltaTime;
        ControlCamera();
        transform.Translate(movement);
    }

    void ControlCamera()
    {
        Vector2 mouseInp = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector3 rotation = Vector3.right * -mouseInp.y;

        transform.Rotate(transform.up, mouseInp.x);
        
        if (cameraLookPoint.rotation.eulerAngles.x + rotation.x < 50 ||
            cameraLookPoint.rotation.eulerAngles.x + rotation.x > 310)
        {
            cameraLookPoint.Rotate(rotation);
        }
    }
}
