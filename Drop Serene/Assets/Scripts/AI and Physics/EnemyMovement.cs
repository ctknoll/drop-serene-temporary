using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
	NavMeshAgent agent;
    public float movementSpeed;
    [HideInInspector]
    public float verticalVelocity = 0;
    public float gravity;
    public float jumpSpeed;

    // Use this for initialization
    void Start()
    {
		agent = GetComponent<NavMeshAgent> ();
    }

    // Update is called once per frame
    void Update()
    {
        //playerJumpAndGravity();
        playerMovement();
    }

    void playerMovement()
    {        
		agent.destination = GameObject.Find ("Player").transform.position;
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
		agent.Move(moveVector * Time.deltaTime);
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
