using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamestateUtilities : MonoBehaviour {

    SetStartOptions startOptions;
    CanvasGroup pauseMenu = null;
    private static bool isPaused;

    public static readonly string[] notInGame = {"Start", "Settings", "Won", "Dead"};

	// Use this for initialization
	void Start ()
    {
        startOptions = GetComponent<SetStartOptions>();

        if (inGame())
        {
            pauseMenu = GameObject.Find("PlayerUI").GetComponentInChildren<CanvasGroup>();            
            isPaused = false;
			SetPaused (isPaused);
        }        
	}
	
	// Update is called once per frame
	void Update ()
    {        
        if(!inGame())
            Cursor.lockState = CursorLockMode.None;
        if (Input.GetButtonDown("Escape") && inGame())
        {            
			SetPaused (!isPaused);
        }
	}    

    public void Restart()
    {
		SetPaused (false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadSceneWhereLastDied()
    {
        LoadScene(DetectPlayerDeath.lastDiedSceneName);
    }

	public void LoadScene(string sceneName)
	{
		SetPaused (false);
        SceneManager.LoadScene(sceneName);
	}

    public bool inGame()
    {
        int pos = Array.IndexOf(GamestateUtilities.notInGame, SceneManager.GetActiveScene().name);
        if (pos < 0)
            return true;
        return false;
    }

    public static bool inGame(string level)
    {
        int pos = Array.IndexOf(GamestateUtilities.notInGame, level);
        if (pos < 0)
            return true;
        return false;
    }

	public void SetPaused(bool pause)
	{
		if(pauseMenu)
		{
			isPaused = pause;
			pauseMenu.interactable = isPaused;

			if (isPaused)
				Time.timeScale = 0F;
			else
				Time.timeScale = 1F;

			foreach (Transform g in pauseMenu.GetComponentInChildren<Transform>())
			{
				g.gameObject.SetActive(isPaused);
			}            
		}         
	}

	public static bool gamePaused()
	{
		return isPaused;
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
