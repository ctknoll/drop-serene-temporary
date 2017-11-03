using UnityEngine;

public class InvestigateState : State
{
	Vector3 location;
	Flashlight light;

    override public void OnStateEnter()
    {
        Debug.Log("Entered investigate state");
        location = controller.alertLocation;
		light = controller.player.gameObject.GetComponentInChildren<Flashlight>();
    }

    override public void OnStateUpdate()
    {        
		controller.agent.destination = location;
        if(!controller.alertLocation.Equals(location) && !controller.alertLocation.Equals(controller.vec3Null))
        {
            //HE MUST DECIDE WHO TO FOLLOW!
            if(Random.Range(0f, 1f) >= .9f)
            {
                location = controller.alertLocation;
            }
        }
    }    

    override public void OnStateExit()
    {
		location = controller.vec3Null;
		Debug.Log("Exit investigate state");
    }

    public override void EvaluateTransition()
    {
		//if Light && LoS -> Chase
		if (LightingUtils.inLineOfSight(controller.gameObject, controller.player.gameObject) && light.lightStatus) controller.currentState = controller.chaseState;

        //if inLightSource && LoS -> Chase
        if (LightingUtils.inLineOfSight(controller.gameObject, controller.player.gameObject) && controller.player.GetComponent<PlayerMovement>().isInLight) controller.currentState = controller.chaseState;

        //Keep disabled: if Proximity && LoS -> Chase


        //if Reached Trigger Location -> Roam
        if ((location - controller.agent.transform.position).magnitude < 1) controller.currentState = controller.roamState;

		//if Trigger Location Unreachable -> Roam        
        if (controller.history.Count > 4 && controller.history[controller.history.Count - 1] == controller.history[controller.history.Count - 4] &&
                controller.history[controller.history.Count - 4] == controller.history[controller.history.Count - 16]) controller.currentState = controller.roamState;            
    }

}