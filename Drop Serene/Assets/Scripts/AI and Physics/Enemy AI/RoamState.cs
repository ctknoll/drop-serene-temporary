using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamState : State 
{    
    public Transform currentGoal;

    override public void OnStateEnter()
	{
		Debug.Log ("Entered roam state");        
        float shortestPath = 1000F;

        foreach(Transform node in controller.roamGoalNodes)
        {            
            controller.agent.SetDestination(node.position);
            if(!controller.agent.pathStatus.Equals("invalid") && shortestPath > controller.agent.remainingDistance)
            {
                shortestPath = controller.agent.remainingDistance;
                currentGoal = node;
            }            
        }

        Debug.Log("Chose goal node at " + currentGoal.position);
        controller.agent.destination = currentGoal.position;
	}

	override public void OnStateUpdate()
	{		
		//if at node, choose new node
        if(Vector3.Magnitude(controller.agent.transform.position - currentGoal.position) < 1F)
        {
            chooseNode();
        }		
	}

	void chooseNode()
	{
        controller.agent.destination = controller.roamGoalNodes[controller.roamGoalNodes.Length - 1].position;
        Debug.Log("Chose new goal");
	}

	override public void OnStateExit()
	{
		Debug.Log ("Exit roam state");
	}

    public override void EvaluateTransition()
    {
        
    }

}
