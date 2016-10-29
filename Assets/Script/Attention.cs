using UnityEngine;
using System.Collections;

public class Attention : MonoBehaviour {
    private FadeManager fadeManager;
    public float timer;
    private float waitTime;

	
	void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
        Cursor.visible = false; //カーソル非表示
        waitTime = timer;
    }
	
	
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0 || (Input.anyKeyDown && timer <= waitTime * 0.75f))
        {
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart("Title");
        }
	}
}
