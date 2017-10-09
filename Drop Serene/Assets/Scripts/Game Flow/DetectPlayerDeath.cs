using UnityEngine;

public class DetectPlayerDeath : MonoBehaviour {

    Transform enemy;
    GamestateUtilities gameStateUtils;    

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        gameStateUtils = GameObject.Find("__MASTER__").GetComponent<GamestateUtilities>();
    }

    void Update()
    {
        if (Vector3.Magnitude(transform.position - enemy.position) < 1.2F)
        {
            Debug.Log("player has died");
            Cursor.lockState = CursorLockMode.None;          
            gameStateUtils.LoadScene("Dead");
        }    
    }
}
