using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    private FadeManager fadeManager;


    void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
        fadeManager.FadeStart(null, waitForSeconds: 0f);  //指定秒待ってからスタートする
    }
	
	
	void Update () {
        if (fadeManager.isFadeFinished && Input.anyKeyDown)
        {
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart(sceneIndex: 0, waitForSeconds: 0f);
        }
    }
}
