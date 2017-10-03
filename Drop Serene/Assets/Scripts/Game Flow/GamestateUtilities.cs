using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamestateUtilities : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if(Input.GetButtonDown("Escape"))
        {
            // Bring up game menu that pauses the game, go to menu and restart options
        }
	}

    public void Pause()
    {
        if(Time.timeScale == 0F)
        {
            Time.timeScale = 1F;
        }
        else
        {
            Time.timeScale = 0F;
        }        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
