using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectPlayerDeath : MonoBehaviour {

    Transform enemy;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
    }

    void Update()
    {
        if (Vector3.Magnitude(transform.position - enemy.position) < 1.2F)
        {            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
