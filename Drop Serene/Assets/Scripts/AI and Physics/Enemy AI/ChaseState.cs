using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    Transform player;
    Flashlight light;
    override public void OnStateEnter()
    {
        Debug.Log("Entered chase state");
        player = GameObject.Find("Player").GetComponent<Transform>();
        light = player.gameObject.GetComponentInChildren<Flashlight>();
        controller.agent.SetDestination(player.position);
    }

    override public void OnStateUpdate()
    {        
        controller.agent.SetDestination(player.position);        
    }

    override public void OnStateExit()
    {
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