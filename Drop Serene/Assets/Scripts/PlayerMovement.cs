using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public float movementSpeed;
    [HideInInspector]
    public float verticalVelocity = 0;
    public float gravity;
    public float jumpSpeed;

    public bool isSprinting;
    public float sprintMultiplier;

	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = GameObject.Find("Main Camera").transform.rotation;
        playerJumpAndGravity();
        playerMovement();
    }


    void playerMovement()
    {
        float sprintModifier = isSprinting ? sprintMultiplier : 1F;
        isSprinting = Input.GetButton("Fire3") ? true : false;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            controller.Move(transform.right * Time.deltaTime * movementSpeed * sprintModifier);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            controller.Move(-transform.right * Time.deltaTime * movementSpeed * sprintModifier);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            controller.Move(transform.forward * Time.deltaTime * movementSpeed * sprintModifier);
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            controller.Move(-transform.forward * Time.deltaTime * movementSpeed * sprintModifier);
        }
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

    public bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            if (hit.distance - (GetComponent<CapsuleCollider>().height / 2) < .1F) return true;
        }
        return false;
    }
}
