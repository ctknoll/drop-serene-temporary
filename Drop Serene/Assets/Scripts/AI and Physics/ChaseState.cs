using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    public ChaseState(EnemyStateController controller)
    {
        this.controller = controller;
    }

    override public void OnStateEnter()
    {
        Debug.Log("Entered chase state");        
    }

    override public void OnStateUpdate()
    {
        Debug.Log("Update chase state");
    }

    override public void OnStateExit()
    {
        Debug.Log("Exit chase state");
    }

    public override void EvaluateTransition()
    {

    }

}