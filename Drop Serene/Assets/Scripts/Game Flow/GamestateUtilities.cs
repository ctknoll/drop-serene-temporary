using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamestateUtilities : MonoBehaviour {

    CanvasGroup pauseMenu = null;
    public bool isPaused;

    public static readonly string[] levels = {"Level 1", "Level 2"};

	// Use this for initialization
	void Start ()
    {        
        if(inGame())
        {
            pauseMenu = GameObject.Find("PlayerUI").GetComponentInChildren<CanvasGroup>();                
            isPaused = true;
            TogglePauseMenu();
        }        
	}
	
	// Update is called once per frame
	void Update ()
    {        
        Debug.Log(inGame());
        if(!inGame())
            Cursor.lockState = CursorLockMode.None;
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
        int pos = Array.IndexOf(GamestateUtilities.levels, SceneManager.GetActiveScene().name);
        if (pos < 0)
            return false;
        return true;
    }

    public static bool inGame(string level)
    {
        int pos = Array.IndexOf(GamestateUtilities.levels, level);
        if (pos < 0)
            return false;
        return true;
    }

    void TogglePauseMenu()
    {
        if(pauseMenu)
        {
            isPaused = !isPaused;
            pauseMenu.interactable = isPaused;

            foreach (Transform g in pauseMenu.GetComponentInChildren<Transform>())
            {
                g.gameObject.SetActive(isPaused);
            }
            
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
