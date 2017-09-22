using UnityEngine;

public abstract class LightableObject : MonoBehaviour
{
    Renderer rend;
    public Material runeMaterial;
    public Color emissionColor;
    public bool isLit = false;
    public bool isActive = false;
    public float intensity = 0F;
    
    public abstract void OnActivate();
    public abstract void OnDeactivate();

    public virtual void Start()
    {
        rend = GetComponent<Renderer>();
        runeMaterial = rend.material;
        //emissionColor = Color.green;
    }

    public virtual void Update()
    {
        rend.material.SetColor("_EmissionColor", emissionColor * intensity);
    }
}
