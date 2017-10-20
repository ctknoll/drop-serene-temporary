using UnityEngine;

public class RuneIndicator : MonoBehaviour {

    private Renderer indicatorMesh;

    public LightableObject linkedRune;

    public Color deactivatedColor;
    public Color activatedColor;

	// Use this for initialization
	void Start ()
    {
        indicatorMesh = this.gameObject.GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (linkedRune.isActive)
        {
            indicatorMesh.material.SetColor("_EmissionColor", activatedColor);
        }else
        {
            indicatorMesh.material.SetColor("_EmissionColor", deactivatedColor);
        }
	}
}
