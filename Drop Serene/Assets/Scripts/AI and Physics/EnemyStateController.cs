using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour 
{
	public NavMeshAgent agent;

	State currentState;
    State expectedState;

    public RoamState roamState;
    public InvestigateState investigateState;
    public ChaseState chaseState;

	// Use this for initialization
	void Start () 
	{
        roamState = new RoamState(this);
        investigateState = new InvestigateState(this);
        chaseState = new ChaseState(this);

        currentState = roamState;
        expectedState = roamState;
        currentState.OnStateEnter ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentState.OnStateUpdate ();

        if(!expectedState.Equals(currentState))
        {
            expectedState.OnStateExit();
            currentState.OnStateEnter();
            expectedState = currentState;
        }
	}
}
