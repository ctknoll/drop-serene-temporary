using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : ScriptableObject 
{	
	abstract public void OnStateEnter (NavMeshAgent agent);
	abstract public void OnStateUpdate(NavMeshAgent agent);
	abstract public void OnStateExit(NavMeshAgent agent);
}
