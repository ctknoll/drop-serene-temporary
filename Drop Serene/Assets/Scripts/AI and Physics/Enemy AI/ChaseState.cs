using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    Transform player;
    Flashlight light;
    
    float originalSpeed;
    

    override public void OnStateEnter()
    {
        
        Debug.Log("Entered chase state");
        player = GameObject.Find("Player").GetComponent<Transform>();
        light = player.gameObject.GetComponentInChildren<Flashlight>();
        controller.agent.SetDestination(player.position);
        originalSpeed = controller.agent.speed;
        controller.agent.speed = player.GetComponent<PlayerMovement>().movementSpeed * player.GetComponent<PlayerMovement>().sprintMultiplier + 0.5f;



    }

    override public void OnStateUpdate()
    {        
        controller.agent.SetDestination(player.position);        
    }

    override public void OnStateExit()
    {
        controller.agent.speed = originalSpeed;
        Debug.Log("Exit chase state");
		controller.alertLocation = player.transform.position;
    }

    public override void EvaluateTransition()
    {		
        if (!LightingUtils.inLineOfSight(controller.gameObject, player.gameObject) && !light.lightStatus)
        {
            controller.currentState = controller.investigateState;
        }
    }
}