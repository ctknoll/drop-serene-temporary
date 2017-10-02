using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDebug : MonoBehaviour {

    bool activated = false;
    public Material xrayMaterial;
    Material defaultMat;

	// Use this for initialization
	void Awake () {
        defaultMat = this.gameObject.GetComponent<MeshRenderer>().material;

    }
	
	// Update is called once per frame
	void Update () {

        

        if (Input.GetKeyDown(KeyCode.F6))
        {
            activated = !activated;
            if (activated)
            {
                this.gameObject.GetComponent<MeshRenderer>().material = xrayMaterial;
            }else
            {
                this.gameObject.GetComponent<MeshRenderer>().material = defaultMat;
            }

        }
	}
}
