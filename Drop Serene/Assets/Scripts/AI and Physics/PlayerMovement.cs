using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public Slider staminaMeter;
	public Image staminaMeterFillImage;
	public Color staminaColor = Color.gray;
	public Color exhaustedColor = Color.red;
    [HideInInspector]
    public float stamina;
    public float staminaDrain;
    public float staminaRecovery;

    [HideInInspector]
    private bool exhausted;
    public float exhaustedMultiplier;

    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
        stamina = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject camera = GameObject.Find("Main Camera");
        transform.rotation = new Quaternion(camera.transform.rotation.x, camera.transform.rotation.y, 0, transform.rotation.w);
        playerJumpAndGravity();
        playerMovement();
        staminaManagement();
    }


    void playerMovement()
    {
        float sprintModifier = isSprinting ? sprintMultiplier : 1F;
        float exhaustedModifier = exhausted ? exhaustedMultiplier : 1F;
        isSprinting = Input.GetButton("Fire3") && !exhausted ? true : false;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            controller.Move(transform.right * Time.deltaTime * movementSpeed * sprintModifier * exhaustedModifier);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            controller.Move(-transform.right * Time.deltaTime * movementSpeed * sprintModifier * exhaustedModifier);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            controller.Move(transform.forward * Time.deltaTime * movementSpeed * sprintModifier * exhaustedModifier);
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            controller.Move(-transform.forward * Time.deltaTime * movementSpeed * sprintModifier * exhaustedModifier);
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

    void staminaManagement()
    {
        if(isSprinting)
        {
            if (stamina > 0) stamina -= staminaDrain * Time.deltaTime;
            if (stamina <= 0)
            {
                stamina = 0;
                exhausted = true;
            }
        }
        else if(stamina < 1)
        {
            stamina += staminaRecovery * Time.deltaTime;
            if (stamina >= 1)
            {
                stamina = 1;
                exhausted = false;
				staminaMeterFillImage.color = staminaColor;
            }
        }

		staminaMeter.value = stamina;
		if (exhausted) 
		{
			staminaMeterFillImage.color = Color.Lerp (exhaustedColor, staminaColor, staminaMeter.value);
		} 
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
