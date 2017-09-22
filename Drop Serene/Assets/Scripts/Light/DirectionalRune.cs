
using UnityEngine;

public class DirectionalRune : LightableObject
{
    public float defaultIntensity = .3F;
    public float lightOnIntensity = .1F;    

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        isActive = true;
        intensity = defaultIntensity;
    }
	
	// Update is called once per frame
	public override void Update ()
    {
		if(!isLit)
        {
            //emissionColor = Color.green; //* Mathf.LinearToGammaSpace(defaultIntensity);
            intensity = defaultIntensity;
        }
        else
        {
            //emissionColor = Color.blue;// * Mathf.LinearToGammaSpace(lightOnIntensity);
            intensity = lightOnIntensity;
        }
        
        base.Update();
	}

    public override void OnActivate(){}

    public override void OnDeactivate(){}
}