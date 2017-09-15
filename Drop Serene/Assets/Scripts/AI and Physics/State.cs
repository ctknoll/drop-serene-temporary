using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : ScriptableObject
{
    protected EnemyStateController controller;
    abstract public void OnStateEnter();
    abstract public void OnStateUpdate();
    abstract public void OnStateExit();
    abstract public void EvaluateTransition(); 
    
    public static State CreateState(string stateType, EnemyStateController controller)
    {
        State state = null;
        switch(stateType)
        {
            case "RoamState": state = ScriptableObject.CreateInstance<RoamState>();
                break;
            case "InvestigateState": state = ScriptableObject.CreateInstance<InvestigateState>();
                break;
            case "ChaseState": state = ScriptableObject.CreateInstance<ChaseState>();
                break;            
        }

        state.controller = controller;
        return state;
    }
}
