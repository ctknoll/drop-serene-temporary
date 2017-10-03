using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class DirectionalRune : LightableObject
{
    public float defaultIntensity = .1F;
    public float lightOnIntensity = .3F;
    public Color deactivatedColor;
    public Color runeLit;
    public Color runeDark;

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
		currentIntensity = defaultIntensity;
		currentColor = deactivatedColor;
        base.Update();
        GetComponent<Rigidbody>().isKinematic = true;
    }
	
	// Update is called once per frame
	public override void Update ()
    {
        if(isActive)
        {
            if (!isLit)
            {
				currentColor = runeDark; 
				currentIntensity = defaultIntensity;
            }
            else
            {
				currentColor = runeLit;
				currentIntensity = lightOnIntensity;
            }

            
        }
		if (!isActive)
		{
			currentColor = deactivatedColor;
			currentIntensity = defaultIntensity;
		}

		base.Update();
	}

    public override void OnActivate()
    {
		if (gameObject.GetComponent<LinkedRune> ())
		{
			if (gameObject.GetComponent<LinkedRune> ().allLinked)
			{
				if (gameObject.GetComponent<LinkedRune> ().runeIsActive)
					isActive = true;
			}
			else
				isActive = true;
		}
		else
			isActive = true;
    }

    public override void OnDeactivate() 
	{
		isActive = false;
	}

    public override void LightOn()
    {
        isLit = true;
		if (gameObject.GetComponent<LinkedRune> ())
		{
			if (gameObject.GetComponent<LinkedRune> ().mutuallyExclusive)
			{
				gameObject.GetComponent<LinkedRune> ().checkMutuallyExclusive();
			}				
		}
        if(!isActive)
        {            
            OnActivate();
        }
    }

    public override void LightOff()
	{
		isLit = false;
	}
}