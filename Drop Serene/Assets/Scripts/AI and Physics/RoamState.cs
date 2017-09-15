using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamState : State 
{
	public GameObject[] nodes;
	public GameObject currentGoal;

    public RoamState(EnemyStateController controller)
    {
        this.controller = controller;
    }

	override public void OnStateEnter()
	{
		Debug.Log ("Entered roam state");
		//Go to nearest node
	}

	override public void OnStateUpdate()
	{		
		//go to node
		//or choose new node
		currentGoal = chooseNode();
	}

	GameObject chooseNode()
	{
		return new GameObject();
	}

	override public void OnStateExit()
	{
		Debug.Log ("Exit roam state");
	}

    public override void EvaluateTransition()
    {
        
    }

}
