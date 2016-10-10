using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    private FadeManager fadeManager;
	

	void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
        fadeManager.FadeStart(null, waitForSeconds: 0f);  //指定秒待ってからスタートする
    }
	
	
	void Update () {
        
	}

    public void OnStaartButton()
    {
        if (fadeManager.isFadeFinished)
        {
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart(1);
        }
    }

    public void OnTutorialButton()
    {
        if (fadeManager.isFadeFinished)
        {
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart("Tutorial");
        }
    }
}
