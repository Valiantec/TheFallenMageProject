using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float sprintingSpeed = 10f;
    [SerializeField] GameObject ability1;
    [SerializeField] BoxCollider rangeCollider;
    [SerializeField] int damage = 50;
    Animator animator;
    RangeSensor rangeSensor;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rangeSensor = GetComponentInChildren<RangeSensor>();
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

        //Abilities
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (rangeSensor.enemies.Count > 0)
            {
                animator.SetTrigger("Cast");
                Golem target = rangeSensor.enemies[0];
                if (target.health - damage <= 0)
                    rangeSensor.enemies.RemoveAt(0);
                Instantiate(ability1, target.transform.position, Quaternion.identity);
                target.TakeDamage(damage);
            }
        }
    }

}
