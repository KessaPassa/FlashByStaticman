using UnityEngine;
using System.Collections;

public class HiddenStage : MonoBehaviour {
    private FadeManager fadeManager;
    private float timer;

	// Use this for initialization
	void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= 5f)
        {
            if (Input.anyKeyDown)
            {
                fadeManager.fadeMode = FadeManager.FadeMode.close;
                fadeManager.FadeStart("Title");
            }
        }
	}
}
