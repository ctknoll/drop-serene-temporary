using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{    
    Flashlight light;    
    float originalSpeed;

    override public void OnStateEnter()
    {        
        Debug.Log("Entered chase state");  
		if(!controller.stateAudio.isPlaying) {
			controller.stateAudio.PlayOneShot (controller.growlSound, 1);
		}
        light = controller.player.gameObject.GetComponentInChildren<Flashlight>();
        controller.agent.SetDestination(controller.player.transform.position);
        originalSpeed = controller.agent.speed;
        controller.agent.speed = controller.chaseSpeed;
    }

    override public void OnStateUpdate()
    {        
        controller.agent.SetDestination(controller.player.transform.position);        
    }

    override public void OnStateExit()
    {
		if(!controller.stateAudio.isPlaying) {
			controller.stateAudio.PlayOneShot (controller.moanSound, 0.2f);
		}
        controller.agent.speed = originalSpeed;        
		controller.alertLocation = controller.player.transform.position;
		Debug.Log("Exit chase state");
    }

    public override void EvaluateTransition()
    {
		//if !LoS && (!Light || Distance) -> Investigate
		if (!LightingUtils.inLineOfSight(controller.gameObject, controller.player.gameObject) && (!light.lightStatus || controller.distanceToPlayer < controller.distance))
        {
            controller.currentState = controller.investigateState;
        }
    }
}