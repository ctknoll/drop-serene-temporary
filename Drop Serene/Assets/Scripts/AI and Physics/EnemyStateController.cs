using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour 
{
	public NavMeshAgent agent;
	StateMachineBehaviour currentState;

	// Use this for initialization
	void Start () 
	{
		currentState = new RoamState ().OnStateEnter (this);		
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentState.OnStateUpdate (this);
		evaluateTransition ();
	}

	void evaluateTransition()
	{

	}
}
