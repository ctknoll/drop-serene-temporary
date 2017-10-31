using UnityEngine;

public class InvestigateState : State
{
	Vector3 location;

    override public void OnStateEnter()
    {
        Debug.Log("Entered investigate state");
        location = controller.alertLocation;      
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
        //If he's at the location of the last noise... OR location is unreachable
        if ((location - controller.agent.transform.position).magnitude < 1) controller.currentState = controller.roamState;
        if (controller.history.Count > 4 && controller.history[controller.history.Count - 1] == controller.history[controller.history.Count - 4] &&
                controller.history[controller.history.Count - 4] == controller.history[controller.history.Count - 16]) controller.currentState = controller.roamState;
            //chase state with los && proximity or los && light
            //if (LightingUtils.inLineOfSight (controller.gameObject, controller.player.gameObject) && Vector3.Distance (controller.transform.position, controller.player.transform.position) < 4F)
            //	controller.currentState = controller.chaseState;
    }

}