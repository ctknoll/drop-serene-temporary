using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamestateUtilities : MonoBehaviour {

    CanvasGroup pauseMenu = null;
    public bool isPaused;

    public readonly string[] levels = {"Level 1", "Level 2"};

	// Use this for initialization
	void Start ()
    {        
        if(inGame())
        {
            pauseMenu = GameObject.Find("PlayerUI").GetComponentInChildren<CanvasGroup>();                
            isPaused = false;
            TogglePauseMenu();
        }        
	}
	
	// Update is called once per frame
	void Update ()
    {        
        Debug.Log(inGame());
        if (Input.GetButtonDown("Escape") && inGame())
        {
            Pause();
            TogglePauseMenu();
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

    public bool inGame()
    {
        int pos = Array.IndexOf(levels, SceneManager.GetActiveScene().name);
        if (pos < 0)
            return false;
        return true;
    }

    void TogglePauseMenu()
    {
        if(pauseMenu)
        {
            pauseMenu.interactable = isPaused;

            foreach (Transform g in pauseMenu.GetComponentInChildren<Transform>())
            {
                g.gameObject.SetActive(isPaused);
            }

            isPaused = !isPaused;
        }        
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
