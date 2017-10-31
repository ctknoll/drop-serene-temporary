using UnityEngine;
using System;
using UnityEngine.AI;
using System.Collections.Generic;

public class RoamState : State 
{    
    public Transform currentGoal;    
    Flashlight light;
    public Vector3 goalPos;
    public Vector3 lastGoal;    

    override public void OnStateEnter()
	{
        controller.history.Clear();
        light = controller.player.gameObject.GetComponentInChildren<Flashlight>();
        Debug.Log ("Entered roam state");

        if (!controller.agent.isOnNavMesh)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(controller.gameObject.transform.position, out hit, 10, NavMesh.AllAreas))
                controller.gameObject.transform.position = hit.position;
        }

        goalPos = localSearch(controller.gameObject.transform.position, 10, 100);
        Debug.Log("Goal Position: " + goalPos);
        controller.agent.destination = goalPos;
    }

	override public void OnStateUpdate()
	{
        Debug.DrawLine(goalPos, controller.gameObject.transform.position, Color.red);
        if (controller.history.Count > 4)
        {
            if(controller.history[controller.history.Count - 1] == controller.history[controller.history.Count - 4] &&
                controller.history[controller.history.Count - 4] == controller.history[controller.history.Count - 16])
            {
                goalPos = localSearch(controller.gameObject.transform.position, 10, 100);
                Debug.Log("New Viable Goal Position: " + goalPos);
                controller.agent.destination = goalPos;
            }
        }
        if (Vector3.Magnitude(controller.agent.transform.position - goalPos) < 1.5F)
        {
            Vector3 tmpGoal = goalPos;
            goalPos = localSearch(controller.gameObject.transform.position, 10, 100);
            while(goalPos == controller.vec3Null)
            {
                goalPos = localSearch(controller.gameObject.transform.position, 10, 100);
            }
            lastGoal = tmpGoal;
            Debug.Log("New Goal Position: " + goalPos);
            controller.agent.destination = goalPos;
        }	
    }

	override public void OnStateExit()
	{
		Debug.Log ("Exit roam state");
	}

    public override void EvaluateTransition()
    {		
		//if Noise -> Investigate
        if (!controller.alertLocation.Equals(controller.vec3Null)) controller.currentState = controller.investigateState;

		//if Light && LoS -> Chase
        if (LightingUtils.inLineOfSight(controller.gameObject, controller.player.gameObject) && light.lightStatus) controller.currentState = controller.chaseState;

		//Keep disabled: if Proximity && LoS -> Chase
    }

    public Vector3 localSearch(Vector3 pos, int radius, int casts)
    {
        List<Vector4> pointsList = new List<Vector4>();
        for(int i = 0; i < casts; i++)
        {
            Vector3 randomPosition = (UnityEngine.Random.insideUnitSphere * radius) + pos;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, 1, NavMesh.AllAreas))
                /*if(LightingUtils.inLineOfSight(hit.position, controller.gameObject))*/
                    pointsList.Add(new Vector4(hit.position.x, hit.position.y, hit.position.z, (float) localSearchValue(hit.position)));
        }
        if (pointsList.Count == 0)
            return localSearch(pos, radius, casts);
        pointsList.Sort((x, y) => (int)(10000 * x.w - 10000* y.w));
        pointsList.Reverse();
        Debug.Log(pointsList[0].w + " " + pointsList[1].w);
        Vector3 newPoint = new Vector3(pointsList[0].x, pointsList[0].y, pointsList[0].z);
        if (radius > 1 || casts >= 8)
            return localSearch(newPoint, radius / 4, (int)(casts / 4));
        NavMeshHit hit2;
        NavMesh.SamplePosition(newPoint, out hit2, 5, NavMesh.AllAreas);
        return hit2.position;
    }

    public double localSearchValue(Vector3 point)
    {
        //angle between lastGoal and newGoal to player should be greater than 90 (he should avoid backtracking)
        //distance to the new goal should be as far as possible
        //encourage verticality, otherwise he'll never find the strairs (??? unknown if true)
        //distance to goal should be as far from walls as possible
        Vector3 playerPos = controller.gameObject.transform.position;
        double totalDistance = Vector3.Magnitude(point - playerPos);
        double yDiff = Mathf.Abs(point.y - controller.gameObject.transform.position.y);
        double minDistanceToWall = Mathf.Infinity;
        float angleVal = Vector3.Angle(playerPos - point, playerPos - lastGoal);
        //angleVal = (float) Sigmoid(Mathf.Sqrt(angleVal) - 9);
        Collider[] checkColliders = Physics.OverlapSphere(point, 10);
        if(checkColliders.Length == 0)
        {
            return totalDistance * angleVal * 10;
        }
        foreach(Collider c in checkColliders)
        {
            float dist = Vector3.Magnitude(point - c.ClosestPointOnBounds(point));
            if(dist < minDistanceToWall) minDistanceToWall = dist;
        }
        double value = (totalDistance * angleVal * minDistanceToWall / 10);
        double yMitigator = 4 * SigDer(yDiff - 4);
//        if (UnityEngine.Random.Range(0, 100) > 95)
//            Debug.Log("Value: " + value + ", Mitigator: " + yMitigator);
        return value - yMitigator;
    }

    public double Sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }

    public double SigDer(double x)
    {
        return Sigmoid(x) * (1 - Sigmoid(x));
    }
}
