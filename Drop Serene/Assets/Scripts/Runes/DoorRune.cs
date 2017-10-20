using UnityEngine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class DoorRune : LightableObject
{
	public float defaultIntensity = .1F;
    public float lightOnIntensity = .3F;
	public Color deactivatedColor;
    public Color runeLit;
    public Color runeDark;

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

        if (!isActive && isLit)
        {
            activationCounter += Time.deltaTime;
            Debug.Log(activationCounter);
            currentColor = Color.Lerp(deactivatedColor, runeLit, secondsToActivate);
            
            Debug.Log(currentColor);
            if (activationCounter >= secondsToActivate)
            {
                isActive = true;
                OnActivate();
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
		isActive = true;
		if (gameObject.GetComponent<LinkedRune>())
		{
			if (gameObject.GetComponent<LinkedRune>().allLinked)
			{
				if (gameObject.GetComponent<LinkedRune>().checkAllLinked() && isActive)
					isLinkedActive = true;
				else
					isLinkedActive = false;
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