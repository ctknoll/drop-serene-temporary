using UnityEngine;
using UnityEngine.AI;

public class InvestigateState : State
{
    public InvestigateState(EnemyStateController controller)
    {
        this.controller = controller;
    }

    override public void OnStateEnter()
    {
        Debug.Log("Entered investigate state");        
    }

    override public void OnStateUpdate()
    {
        Debug.Log("Update investigate state");
    }    

    override public void OnStateExit()
    {
        Debug.Log("Exit investigate state");
    }

    public override void EvaluateTransition()
    {

    }

}