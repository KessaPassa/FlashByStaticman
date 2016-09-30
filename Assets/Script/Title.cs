using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
    private FadeManager fadeManager;
	
	void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
	}
	
	
	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            fadeManager.FadeStart(sceneIndex: 1, waitForSeconds: 0f);
        }
	}

    public void OnStaartButton()
    {

    }
}
