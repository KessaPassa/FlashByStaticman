using UnityEngine;
using System.Collections;

public class Attention : MonoBehaviour {
    private FadeManager fadeManager;
    public float timer;

	
	void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
	}
	
	
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0 || (Input.anyKeyDown && timer <= 5f))
        {
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart("Title");
        }
	}
}
