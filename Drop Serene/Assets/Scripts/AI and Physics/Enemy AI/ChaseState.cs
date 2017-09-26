using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    Transform player;
    Flashlight light;
    override public void OnStateEnter()
    {
        Debug.Log("Entered chase state");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        light = player.gameObject.GetComponentInChildren<Flashlight>();
        controller.agent.SetDestination(player.position);
    }

    override public void OnStateUpdate()
    {
        Debug.Log("Update chase state");
        controller.agent.SetDestination(player.position);        
    }

    override public void OnStateExit()
    {
        Debug.Log("Exit chase state");
    }

    public override void EvaluateTransition()
    {
        if (!LightingUtils.inLineOfSight(controller.gameObject, player.gameObject) && !light.lightStatus)
        {
            controller.currentState = controller.investigateState;
        }
    }
}