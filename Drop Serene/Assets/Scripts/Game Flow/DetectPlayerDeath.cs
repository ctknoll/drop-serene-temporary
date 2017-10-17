using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectPlayerDeath : MonoBehaviour {

    Transform enemy;
    GamestateUtilities gameStateUtils;

    public static string lastDiedSceneName = "Level 1";

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        gameStateUtils = GameObject.Find("__MASTER__").GetComponent<GamestateUtilities>();
    }

    void Update()
    {
        if (Vector3.Magnitude(transform.position - enemy.position) < 1.2F)
        {

            lastDiedSceneName = SceneManager.GetActiveScene().name;

            Debug.Log("player has died");

            Cursor.lockState = CursorLockMode.None;          
            gameStateUtils.LoadScene("Dead");
        }    
    }
}
