﻿using UnityEngine;
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
        Debug.DrawLine(goalPos, controller.gameObject.transform.position);
        if (Vector3.Magnitude(controller.agent.transform.position - goalPos) < 2F)
        {
            Vector3 tmpGoal = goalPos;
            goalPos = localSearch(controller.gameObject.transform.position, 10, 100);
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
        if (!controller.alertLocation.Equals(controller.vec3Null)) controller.currentState = controller.investigateState;
		//if (controller.foundPlayer != null) controller.currentState = controller.chaseState;

        //enter chase if in line of sight and in light, EVER
        if (LightingUtils.inLineOfSight(controller.gameObject, controller.player.gameObject) && light.lightStatus) controller.currentState = controller.chaseState;

		if (LightingUtils.inLineOfSight (controller.gameObject, controller.player.gameObject) && Vector3.Distance (controller.transform.position, controller.player.transform.position) < 4F)
			controller.currentState = controller.chaseState;
    }

    public Vector3 localSearch(Vector3 pos, int radius, int casts)
    {
        List<Vector4> pointsList = new List<Vector4>();
        for(int i = 0; i < casts; i++)
        {
            Vector3 randomPosition = (UnityEngine.Random.insideUnitSphere * radius) + pos;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, 1, NavMesh.AllAreas))
                if(LightingUtils.inLineOfSight(hit.position, controller.gameObject))
                    pointsList.Add(new Vector4(hit.position.x, hit.position.y, hit.position.z, localSearchValue(hit.position)));
        }
        if (pointsList.Count == 0)
            return localSearch(pos, radius, casts);
        pointsList.Sort((x, y) => (int)(x.w - y.w));
        pointsList.Reverse();
        Vector3 newPoint = new Vector3(pointsList[0].x, pointsList[0].y, pointsList[0].z);
        if (radius > 1 || casts >= 4)
            return localSearch(newPoint, radius / 4, (int)(casts / 4));
        NavMeshHit hit2;
        NavMesh.SamplePosition(newPoint, out hit2, 5, NavMesh.AllAreas);
        return hit2.position;
    }

    public float localSearchValue(Vector3 point)
    {
        //angle between lastGoal and newGoal to player should be greater than 90 (he should avoid backtracking)
        //distance to the new goal should be as far as possible
        //encourage verticality, otherwise he'll never find the strairs (??? unknown if true)
        //distance to goal should be as far from walls as possible
        Vector3 playerPos = controller.gameObject.transform.position;
        float totalDistance = Vector3.Magnitude(point - playerPos);
        float yDiff = Mathf.Abs(point.y - controller.gameObject.transform.position.y);
        float minDistanceToWall = Mathf.Infinity;
        float angleToLastGoal = Mathf.Abs(Vector3.Angle(playerPos, point) - Vector3.Angle(playerPos, lastGoal));
        Collider[] checkColliders = Physics.OverlapSphere(point, 10);
        if(checkColliders.Length == 0)
        {
            return totalDistance + (angleToLastGoal / 10) + 10;
        }
        foreach(Collider c in checkColliders)
        {
            float dist = Vector3.Magnitude(point - c.ClosestPointOnBounds(point));
            if(dist < minDistanceToWall) minDistanceToWall = dist;
        }
        return totalDistance + (angleToLastGoal / 10) + minDistanceToWall;
    }

}
