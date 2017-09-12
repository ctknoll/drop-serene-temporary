using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamState : State 
{
	public GameObject[] nodes;
	public GameObject currentGoal;

	override public void OnStateEnter(NavMeshAgent agent)
	{
		Debug.Log ("Entered roam state");
		//Go to nearest node
	}

	override public void OnStateUpdate(NavMeshAgent agent)
	{		
		//go to node
		//or choose new node
		currentGoal = chooseNode();
	}

	GameObject chooseNode()
	{
		return;
	}

	override public void OnStateExit(NavMeshAgent agent)
	{
		Debug.Log ("Exit roam state");
	}

}
