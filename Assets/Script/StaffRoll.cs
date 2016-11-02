using UnityEngine;
using System.Collections;

public class StaffRoll : MonoBehaviour {
    FadeManager fadeManager;
	
	void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
	}
	
	
	void Update () {
        if (fadeManager.isFadeFinished && Input.anyKeyDown)
        {
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart("Title");
        }
	}
}
