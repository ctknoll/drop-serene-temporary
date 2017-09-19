using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class LutManager : MonoBehaviour {
    PlayerMovement playerMovement;
    PostProcessingBehaviour postProcess;

	// Use this for initialization
	void Start () 
	{
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        postProcess = gameObject.GetComponent<PostProcessingBehaviour>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        PostProcessingProfile current = postProcess.profile;
        PostProcessingProfile newProfile = Instantiate(current);
        newProfile.name = postProcess.profile.name;
        var settings = newProfile.userLut.settings;
        settings.contribution = Mathf.Min(1, Mathf.Max(0, 1 - playerMovement.stamina));
        newProfile.userLut.settings = settings;
        postProcess.profile = newProfile;
	}
}
