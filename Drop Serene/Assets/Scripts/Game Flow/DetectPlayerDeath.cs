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
        if (Vector3.Magnitude(new Vector3(transform.position.x - enemy.position.x, (transform.position.y - enemy.position.y)/2, transform.position.z - enemy.position.z)) < 1.1F)
        {

            lastDiedSceneName = SceneManager.GetActiveScene().name;

            Debug.Log("player has died");

            Cursor.lockState = CursorLockMode.None;          
            gameStateUtils.LoadScene("Dead");
        }    
    }
}
