using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyStateController : MonoBehaviour 
{
	public NavMeshAgent agent;
    public Transform[] roamGoalNodes;

    State currentState;
    State expectedState;

    RoamState roamState;
    InvestigateState investigateState;
    ChaseState chaseState;    

    // Use this for initialization
    void Start () 
	{
        agent = GetComponent<NavMeshAgent>();

        roamState = (RoamState)State.CreateState("RoamState", this);
        investigateState = (InvestigateState)State.CreateState("InvestigateState", this);
        chaseState = (ChaseState)State.CreateState("ChaseState", this);

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
