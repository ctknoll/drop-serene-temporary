using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamestateUtilities : MonoBehaviour {

    SetStartOptions startOptions;
    CanvasGroup pauseMenu = null;
    public bool isPaused;

    public static readonly string[] levels = {"Level 1", "Level 2"};

	// Use this for initialization
	void Start ()
    {
        startOptions = GetComponent<SetStartOptions>();

        if (inGame())
        {
            pauseMenu = GameObject.Find("PlayerUI").GetComponentInChildren<CanvasGroup>();            
            isPaused = true;
            TogglePauseMenu();
        }        
	}
	
	// Update is called once per frame
	void Update ()
    {        
        if(!inGame())
            Cursor.lockState = CursorLockMode.None;
        if (Input.GetButtonDown("Escape") && inGame())
        {            
            TogglePauseMenu();            
        }
	}

    public void Pause()
    {
        Time.timeScale = 0F;
    }

    public void Unpause()
    {
        Time.timeScale = 1F;
    }

    public void Restart()
    {
        Unpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadSceneWhereLastDied()
    {
        LoadScene(DetectPlayerDeath.lastDiedSceneName);
    }

	public void LoadScene(string sceneName)
	{
        Unpause();
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

            if (isPaused)
                Pause();
            else
                Unpause();

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
