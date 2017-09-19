﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateController : MonoBehaviour 
{
	
    public Transform[] roamGoalNodes;

    public State currentState;
    State expectedState;

    [HideInInspector]
    public NavMeshAgent agent;
    public RoamState roamState;
    public InvestigateState investigateState;
    public ChaseState chaseState;

    public readonly Vector3 vec3Null = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
    public Vector3 alertLocation;
	public GameObject foundPlayer;

    // Use this for initialization
    void Start () 
	{
        agent = GetComponent<NavMeshAgent>();
        alertLocation = vec3Null;

        roamState = (RoamState)State.CreateState("RoamState", this);
        investigateState = (InvestigateState)State.CreateState("InvestigateState", this);
        chaseState = (ChaseState)State.CreateState("ChaseState", this);

        currentState = roamState;
        expectedState = roamState;
        currentState.OnStateEnter();
	}
	
	// Update is called once per frame
	void Update () 
	{
		foundPlayer = checkForLitPlayer();
		currentState.OnStateUpdate();
        currentState.EvaluateTransition();
        if(!expectedState.Equals(currentState))
        {
            expectedState.OnStateExit();
            currentState.OnStateEnter();
            expectedState = currentState;
            alertLocation = vec3Null;
			foundPlayer = null;
        }
	}

	public GameObject checkForLitPlayer()
	{
		bool flashlightLit = GameObject.Find("Spotlight").GetComponent<Flashlight> ().lightStatus;
		GameObject player = GameObject.Find("Player");
		if (!Physics.Linecast (agent.transform.position, player.transform.position, 510) && flashlightLit)
			return player;
		return null;
	}

    public void heardNoise(Vector3 location) {alertLocation = location;}
}
