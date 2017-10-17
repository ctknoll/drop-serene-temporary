/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[System.Serializable]
public class VoidEvent : UnityEvent {}

[System.Serializable]
public class MoveableObjectEvent : UnityEvent<GameObject, Vector3> {}

public class Rune : LightableObject 
{
	public enum RuneType {Void, MoveableObject}

	public RuneType activateType, deactivateType, lightOnType, lightOffType;

	public VoidEvent voidActivateEvent, voidDeactivateEvent, voidLightOnEvent, voidLightOffEvent;
	public MoveableObjectEvent objectActivateEvent, objectDeactivateEvent, objectLightOnEvent, objectLightOffEvent;

	GameObject runeGameObject;
	Vector3 runeVec3;

	void Start() 
	{
		base.Start();

	}
	
	// Update is called once per frame
	void Update() 
	{
	}

	public override void LightOn()
	{
		
	}

	public override void LightOff()
	{
	}

	public override void OnActivate()
	{
	}

	public override void OnDeactivate()
	{
	}

}
	
// Follow answers.unity3d.com/questions/417837/change-inspector-variabes-depending-on-enum
[CustomEditor(typeof(Rune)), CanEditMultipleObjects]
public class RuneEditor : Editor
{

//	public SerializedProperty 
//	public override void OnEnable()
//	{
//	}

	public override void OnInspectorGUI()
	{
		Rune myRune = target as Rune;
	}

	public void tryHideElement(bool cond)
	{
		using (var condScope = new EditorGUILayout.FadeGroupScope (Convert.ToSingle (cond)))
		{
			
		}
	}
}*/