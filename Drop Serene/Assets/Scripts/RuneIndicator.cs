using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneIndicator : MonoBehaviour {


    private MeshRenderer indicatorMesh;

    public LightableObject linkedRune;

    public Color deactivatedColor;
    public Color activatedColor;

	// Use this for initialization
	void Start () {
        indicatorMesh = this.gameObject.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (linkedRune.isActive)
        {
            indicatorMesh.material.SetColor("_EmissionColor", activatedColor);
        }else
        {
            indicatorMesh.material.SetColor("_EmissionColor", deactivatedColor);
        }
	}
}
