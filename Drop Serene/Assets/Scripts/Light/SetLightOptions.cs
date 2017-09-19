using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLightOptions : MonoBehaviour {

    public Slider lightSlider;
    AmbientLightDefaults lightScript;
    public float minLightLevel = .02F;
    public float maxLightLevel = .4F;

	// Use this for initialization
	void Start () {
        lightScript = GameObject.Find("__MASTER__").GetComponent<AmbientLightDefaults>();
        lightSlider.value = lightScript.intensity;
        lightSlider.minValue = minLightLevel;
        lightSlider.maxValue = maxLightLevel;
	}

    public void SetLightLevel()
    {
        lightScript.intensity = lightSlider.value;
    }

    /*public void StartGame()
    {
        PlayerPrefs.SetFloat("LightLevel", lightScript.intensity);
        SceneManager.LoadScene("Level 1");
    }*/

    public void StartLevelOne()
    {
        PlayerPrefs.SetFloat("LightLevel", lightScript.intensity);
        SceneManager.LoadScene("Level 1");
    }

    public void StartLevelTwo()
    {
        PlayerPrefs.SetFloat("LightLevel", lightScript.intensity);
        SceneManager.LoadScene("Level 2");
    }
}
