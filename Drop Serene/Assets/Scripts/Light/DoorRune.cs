using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class DoorRune : LightableObject
{
    public GameObject door;
    public float defaultIntensity = .1F;
    public float lightOnIntensity = .3F;
    public Color runeLit;
    public Color runeDark;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        intensity = defaultIntensity;
        emissionColor = runeDark;
        base.Update();
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (door.activeSelf && isActive)// && !LightingUtils.inLineOfSight(door, gameObject))
        {
            door.SetActive(false);
            Debug.Log("Disabled door");
        }

        if(emissionColor != runeDark)
            base.Update();
    }

    public override void OnActivate()
    {
        isActive = true;
    }

    public override void OnDeactivate() { }

    public override void LightOn()
    {
        isLit = true;
        emissionColor = runeLit;
        intensity = lightOnIntensity;

        if (!isActive)
        {
            OnActivate();
        }
    }

    public override void LightOff()
    {
        isLit = false;
    }
}