using UnityEngine;

public abstract class LightableObject : MonoBehaviour
{
    Renderer rend;    
    public bool isLit = false;
    public bool isActive = false;
	public bool isLinkedActive = false;

    [HideInInspector]
    public Material runeMaterial;
	public Color currentColor;
    public float currentIntensity = 0F;
    
    public abstract void OnActivate();
    public abstract void OnDeactivate();
    public abstract void LightOn();
    public abstract void LightOff();

    public virtual void Start()
    {
        rend = GetComponent<Renderer>();
        runeMaterial = rend.material;
    }

    public virtual void Update()
    {
		rend.material.SetColor("_EmissionColor", currentColor * currentIntensity);
    }
}
