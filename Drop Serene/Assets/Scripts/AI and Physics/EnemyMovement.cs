using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    CharacterController controller;
    public float movementSpeed;
    [HideInInspector]
    public float verticalVelocity = 0;
    public float gravity;
    public float jumpSpeed;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerJumpAndGravity();
        playerMovement();
    }

    void playerMovement()
    {
        Vector3 movementDirection = GameObject.Find("Player").transform.position - transform.position;        
        transform.rotation = Quaternion.LookRotation(movementDirection);       
        //keep y rotation to 0?

        controller.Move(transform.forward * Time.deltaTime * movementSpeed);       
    }

    void playerJumpAndGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        if (verticalVelocity < -gravity * .75F) verticalVelocity = -gravity * .75F;
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            verticalVelocity = jumpSpeed;
        }
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(moveVector * Time.deltaTime);
    }

    bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            if (hit.distance - (GetComponent<CapsuleCollider>().height / 2) < .1F) return true;
        }
        return false;
    }
}
