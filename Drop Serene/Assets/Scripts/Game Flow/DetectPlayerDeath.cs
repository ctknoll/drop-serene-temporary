using UnityEngine;

public class DetectPlayerDeath : MonoBehaviour {

    Transform enemy;

    GamestateUtilities gameStateUtils;

    bool dying = false;
    float timeOfDeath = -1;
    public float timeToFade = 2;

    public Texture fadeTexture;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        gameStateUtils = GameObject.Find("__MASTER__").GetComponent<GamestateUtilities>();
    }

    void Update()
    {
        if (dying == false && Vector3.Magnitude(transform.position - enemy.position) < 1.2F)
        {
            Debug.Log("player has died");

            dying = true;
            timeOfDeath = Time.realtimeSinceStartup;
            gameStateUtils.Pause();

        }


        if (Input.anyKeyDown && dying && Time.realtimeSinceStartup - timeOfDeath >= 2)
        {
            Debug.Log("Restarting level after death screen");

            dying = false;
            gameStateUtils.Restart();
            gameStateUtils.Pause();
        }
    }

    void OnGUI()
    {
        if (!dying) return;

        float fadePercent = (Time.realtimeSinceStartup - timeOfDeath) / timeToFade;
        //Debug.Log("Fade percent: " + fadePercent);
        Color fadeColor = Color.white;
        fadeColor.a = fadePercent;
        GUI.color = fadeColor;
        GUIStyle guistyle = new GUIStyle();
        guistyle.fontSize = 64;

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        if (fadePercent > 1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 50, Screen.width / 2 + 300, Screen.height / 2 + 50), "Press any key to continue", guistyle);

        }
    }
}
