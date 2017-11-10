using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class DoorRune : LightableObject
{
    [Header("Light Options")]
	public float defaultIntensity = .1F;
    public float lightOnIntensity = .3F;
	public Color deactivatedColor;
    public Color runeLit;
    public Color runeDark;
    public GameObject runeRing;

    [Header("Platform options")]
	public GameObject[] platforms;
	public float moveSpeed;
	public Vector3 directionToMove;

	private GameObject platObj;
	private List<Vector3> startPos;
	private List<Vector3> endPos;
	private List<Vector3> currentPos;
	private List<IEnumerator> moveTowards;
	private List<IEnumerator> moveReturn;
	private List<float> moveTime;
	private bool locked;
	private bool moving;
    private AudioSource doorSounds;
    private AudioSource runeSounds;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
		currentIntensity = defaultIntensity;
		currentColor = deactivatedColor;
        GetComponent<Rigidbody>().isKinematic = true;
		locked = false;
		moveTime = new List<float>();
		startPos = new List<Vector3>();
		endPos = new List<Vector3>();
		currentPos = new List<Vector3>();
		moveTowards = new List<IEnumerator>();
		moveReturn = new List<IEnumerator>();
        doorSounds = GameObject.Find("Door Sounds").GetComponent<AudioSource>();
        runeSounds = GameObject.Find("Rune Sounds").GetComponent<AudioSource>();

        runeRing = Resources.Load("Rune Ring") as GameObject;
        Debug.Log("Rune Ring: " + runeRing);
    }

    // Update is called once per frame
    public override void Update()
    {
		bool tmpActive;
		if (gameObject.GetComponent<LinkedRune>() && gameObject.GetComponent<LinkedRune>().allLinked)
		{
			tmpActive = isLinkedActive;
		}
		else
			tmpActive = isActive;
		if (tmpActive && !locked && !moving)
		{
			locked = true;
			moving = true;
			int i = 0;
			currentPos.Clear();
			foreach (GameObject platform in platforms)
			{
				if(startPos.Count < platforms.Length) startPos.Add(platform.transform.position);
				if(endPos.Count < platforms.Length) endPos.Add(platform.transform.position + directionToMove);
				moveTime.Add((directionToMove.magnitude / moveSpeed));
				IEnumerator ienum = MoveOut(platform, i);
				moveTowards.Add(ienum);
				StartCoroutine(ienum);
				i++;
			}
		} 
		else if (!tmpActive && locked && !moving) 
		{
			locked = false;
			moving = true;
			int i = 0;
			currentPos.Clear();
			foreach (GameObject platform in platforms)
			{
				IEnumerator ienum = MoveBack(platform, i);
				moveReturn.Add(ienum);
				StartCoroutine(ienum);
				i++;
			}
		}
        if (isActive)
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
        else   //!active
        {
            if(activationCounter == 0)
            {
                currentColor = deactivatedColor;
                currentIntensity = defaultIntensity;
            }
            if (isLit && activationCounter < secondsToActivate)    
                activationCounter += Time.deltaTime;            
            currentColor = Color.Lerp(deactivatedColor, runeLit, activationCounter/(2*secondsToActivate));
            currentIntensity = Mathf.Lerp(defaultIntensity/2, lightOnIntensity / 1.5f, activationCounter/secondsToActivate);            
            
            if (activationCounter >= secondsToActivate)
            {
                activationCounter = secondsToActivate;
                isActive = true;
                OnActivate();
            }
        }
        base.Update();
    }

    public override void OnActivate()
    {
		isActive = true;
        runeSounds.Play();
        LinkedRune linkedRune = gameObject.GetComponent<LinkedRune>();

        if (linkedRune)
		{
			if (linkedRune.allLinked)
			{
				if (linkedRune.checkAllLinked() && isActive) // Activate all linked runes
                {
                    isLinkedActive = true;                    
                }
					
				else
					isLinkedActive = false;
			}
		}
        else   // Activate single rune        
        {
            doorSounds.Play();
            Instantiate(runeRing, transform);
            GetComponentsInChildren<SpriteRenderer>()[1].material.SetColor("_EmissionColor", runeLit);
        }

        if(isLinkedActive)  //Activate multiple runes
        {
            doorSounds.Play();

            //Add ring to this rune
            Instantiate(runeRing, transform);
            GetComponentsInChildren<SpriteRenderer>()[1].material.SetColor("_EmissionColor", runeLit);

            //Add ring to other linked runes
            foreach (GameObject rune in linkedRune.linkedRunes)
            {
                Instantiate(runeRing, rune.transform);
                rune.GetComponentsInChildren<SpriteRenderer>()[1].material.SetColor("_EmissionColor", runeLit);
            }
        }
        
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
        
        if(isActive)
        {
            currentColor = runeLit;
            currentIntensity = lightOnIntensity;
        }
    }

    public override void LightOff()
    {
        isLit = false;
        currentColor = runeDark;
        currentIntensity = defaultIntensity;
    }

	public IEnumerator MoveOut(GameObject platform, int index)
	{
		float panStart = Time.time;
		currentPos.Add(platform.transform.position);
		moveTime[index] = ((currentPos[index] - endPos[index]).magnitude / moveSpeed);
		while ((Time.time < panStart + moveTime[index]) && isActive)
		{
			platform.transform.position = Vector3.Lerp(currentPos[index], endPos[index], (Time.time - panStart) / moveTime[index]);
			yield return null;
		}
		moving = false;
	}

	public IEnumerator MoveBack(GameObject platform, int index)
	{
		float panStart = Time.time;
		currentPos.Add(platform.transform.position);
		moveTime[index] = ((currentPos[index] - startPos[index]).magnitude / moveSpeed);
		while (Time.time < panStart + moveTime[index] && !isActive)
		{
			platform.transform.position = Vector3.Lerp(currentPos[index], startPos[index], (Time.time - panStart) / moveTime[index]);
			yield return null;
		}
		moveTime.Clear();
		moveTowards.Clear();
		moveReturn.Clear();
		moving = false;
	}
}