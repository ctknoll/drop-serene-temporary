using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class LoadNextLevelArea : MonoBehaviour {
    public string levelName;
	// Use this for initialization
	void Start () {
		foreach(Collider col in this.gameObject.GetComponents<Collider>())
        {
            col.isTrigger = true;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(levelName);
    }

}
