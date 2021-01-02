using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float sprintingSpeed = 10f;

    Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //take input
        bool sprinting = Input.GetKey(KeyCode.LeftShift);
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        //move transform
        movement *= (sprinting ? sprintingSpeed : speed) * Time.deltaTime;
        transform.Translate(movement);

        //rotate transform
        transform.Rotate(transform.up, mouseInput.x);

        //animate
        animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
    }

}
