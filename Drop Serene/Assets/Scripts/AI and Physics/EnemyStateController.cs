using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour 
{
	public NavMeshAgent agent;
	State currentState;

	// Use this for initialization
	void Start () 
	{
        currentState = new RoamState();
        currentState.OnStateEnter (agent);		
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentState.OnStateUpdate (agent);
		evaluateTransition ();
	}

	void evaluateTransition()
	{

	}
}
