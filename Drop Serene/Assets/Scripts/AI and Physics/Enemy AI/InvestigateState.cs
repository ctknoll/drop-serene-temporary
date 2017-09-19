﻿using UnityEngine;

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
        Debug.Log("Update investigate state");
        controller.agent.Move(location);
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
        Debug.Log("Exit investigate state");
    }

    public override void EvaluateTransition()
    {
        //If he's at the location of the last noise...
        if ((location - controller.agent.transform.position).magnitude < 1)
        {
            controller.currentState = controller.roamState;
        }
    }

}