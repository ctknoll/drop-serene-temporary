using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{    
    Flashlight light;
    
    float originalSpeed;    

    override public void OnStateEnter()
    {        
        Debug.Log("Entered chase state");        
        light = controller.player.gameObject.GetComponentInChildren<Flashlight>();
        controller.agent.SetDestination(controller.player.transform.position);
        originalSpeed = controller.agent.speed;
        controller.agent.speed = controller.player.GetComponent<PlayerMovement>().movementSpeed * controller.player.GetComponent<PlayerMovement>().sprintMultiplier + 0.5f;
    }

    override public void OnStateUpdate()
    {        
        controller.agent.SetDestination(controller.player.transform.position);        
    }

    override public void OnStateExit()
    {
        controller.agent.speed = originalSpeed;
        Debug.Log("Exit chase state");
		controller.alertLocation = controller.player.transform.position;
    }

    public override void EvaluateTransition()
    {
		//if !Light && (!LoS || Distance) - distance is 20-30 meters
		if (!LightingUtils.inLineOfSight(controller.gameObject, controller.player.gameObject) && (!light.lightStatus || Vector3.Distance(controller.transform.position, controller.player.transform.position) < 25F))
        {
            controller.currentState = controller.investigateState;
        }
    }
}