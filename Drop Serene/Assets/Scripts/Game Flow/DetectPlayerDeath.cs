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
        if (Vector3.Distance(transform.GetComponent<Collider>().ClosestPointOnBounds(enemy.position), enemy.GetComponent<Collider>().ClosestPointOnBounds(transform.position)) < .1F)
        {

            lastDiedSceneName = SceneManager.GetActiveScene().name;

            Debug.Log("player has died");

            Cursor.lockState = CursorLockMode.None;          
            gameStateUtils.LoadScene("Dead");
        }    
    }
}
