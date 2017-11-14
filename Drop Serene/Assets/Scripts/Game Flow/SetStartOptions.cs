using UnityEngine.UI;
using UnityEngine;

public class SetStartOptions : MonoBehaviour {

    public Slider lightSlider;
    public Toggle invertMouse;
    AmbientLightDefaults lightScript;
    public float minLightLevel = .02F;
    public float maxLightLevel = .4F;

	// Use this for initialization
	void Start ()
    {
        lightScript = GameObject.Find("__MASTER__").GetComponent<AmbientLightDefaults>();
        lightSlider.value = lightScript.intensity;
        lightSlider.minValue = minLightLevel;
        lightSlider.maxValue = maxLightLevel;
        invertMouse.isOn = PlayerPrefs.GetInt("InvertMouse", 0) == 0 ? false : true;
	}

    public void SetLightLevel()
    {
        lightScript.intensity = lightSlider.value;
    }

    public void SetMouseInversion()
    {
        //invertMouse = !invertMouse;
    }

    public void SetPlayerPrefs()
    {
        PlayerPrefs.SetFloat("LightLevel", lightScript.intensity);
        PlayerPrefs.SetInt("InvertMouse", invertMouse.isOn ? 1 : 0);
    }    
}
