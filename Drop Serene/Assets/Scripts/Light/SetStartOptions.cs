using UnityEngine.UI;
using UnityEngine;

public class SetStartOptions : MonoBehaviour {

    public Slider lightSlider;
    AmbientLightDefaults lightScript;
    public float minLightLevel = .02F;
    public float maxLightLevel = .4F;
    public bool invertMouse = false;

	// Use this for initialization
	void Start ()
    {
        lightScript = GameObject.Find("__MASTER__").GetComponent<AmbientLightDefaults>();
        lightSlider.value = lightScript.intensity;
        lightSlider.minValue = minLightLevel;
        lightSlider.maxValue = maxLightLevel;
	}

    public void SetLightLevel()
    {
        lightScript.intensity = lightSlider.value;
    }

    public void SetMouseInversion()
    {
        invertMouse = !invertMouse;
    }

    public void SetPlayerPrefs()
    {
        PlayerPrefs.SetFloat("LightLevel", lightScript.intensity);
        PlayerPrefs.SetInt("InvertMouse", invertMouse ? 1 : 0);
    }    
}
