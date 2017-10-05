using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LightableObject))]
public class LinkedRune : MonoBehaviour 
{

	public bool mutuallyExclusive;
	public bool isMutuallyExclusive;
	public bool allLinked;
	public bool runeIsActive;

	public List<GameObject> linkedRunes;
	// Use this for initialization
	void Start () 
	{
		runeIsActive = false;
		Debug.Log (linkedRunes);
	}
	
	public void checkMutuallyExclusive()
	{
		foreach (GameObject rune in linkedRunes)
		{
			foreach (LightableObject runeType in rune.GetComponents<LightableObject>())
			{
				if (runeType.isActive)
				{
					runeType.isActive = false;
				}
			}
		}
	}

	public bool checkAllLinked()
	{
		bool allinked = true;
		foreach (GameObject rune in linkedRunes)
		{
			foreach (LightableObject runeType in rune.GetComponents<LightableObject>())
			{
				if (!runeType.isActive)
				{
					allinked = false;
				}
			}
		}
		return allinked;
	}
}
