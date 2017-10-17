﻿using UnityEngine;
using System;

public class RoamState : State 
{    
    public Transform currentGoal;    
    Flashlight light;

    override public void OnStateEnter()
	{        
        light = controller.player.gameObject.GetComponentInChildren<Flashlight>();
        Debug.Log ("Entered roam state");

        if (controller.agent.isOnNavMesh)
        {
     //       Debug.Log(controller.agent.transform.position);
        }
              
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
		//if node unreachable
        //if at node, choose new node
        if (Vector3.Magnitude(controller.agent.transform.position - currentGoal.position) < 1F)
        {
            chooseNode();
        }	
	}

	bool nodeUnreachable()
	{
		return false;
	}

	void chooseNode()
	{
        int index = (Array.IndexOf(controller.roamGoalNodes, currentGoal) + 1) % controller.roamGoalNodes.Length;
        currentGoal = controller.roamGoalNodes[index];
        controller.agent.destination = currentGoal.position;
		Debug.Log("Chose new goal " + currentGoal.name);
	}

	override public void OnStateExit()
	{
		Debug.Log ("Exit roam state");
	}

    public override void EvaluateTransition()
    {
        if (!controller.alertLocation.Equals(controller.vec3Null)) controller.currentState = controller.investigateState;
		//if (controller.foundPlayer != null) controller.currentState = controller.chaseState;

        //enter chase if in line of sight and in light, EVER
        if (LightingUtils.inLineOfSight(controller.gameObject, controller.player.gameObject) && light.lightStatus) controller.currentState = controller.chaseState;

		if (LightingUtils.inLineOfSight (controller.gameObject, controller.player.gameObject) && Vector3.Distance (controller.transform.position, controller.player.transform.position) < 4F)
			controller.currentState = controller.chaseState;
    }

}
