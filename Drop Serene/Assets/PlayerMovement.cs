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

	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    { 
        verticalVelocity -= gravity * Time.deltaTime;
        if (verticalVelocity < -gravity * .75f) verticalVelocity = -gravity * .75f;
        if (Input.GetButtonDown("Jump") && grounded(1.1F))
        {
            verticalVelocity = jumpSpeed;
        }
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(moveVector * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            controller.Move(transform.right * Time.deltaTime * movementSpeed);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            controller.Move(-transform.right * Time.deltaTime * movementSpeed);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            controller.Move(transform.forward * Time.deltaTime * movementSpeed);
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            controller.Move(-transform.forward * Time.deltaTime * movementSpeed);
        }
    }

    bool grounded(float value)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
            Debug.Log(hit.distance);
            if (hit.distance < value) return true;
        }
        return false;
    }
}
