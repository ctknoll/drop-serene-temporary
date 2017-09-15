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
}
