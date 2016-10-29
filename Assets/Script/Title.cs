using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    private FadeManager fadeManager;
    

	void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
        SelectArrow.StartSelect();
    }
	
	
	void Update () {
        
    }

    public void OnStartButton()
    {
        if (fadeManager.isFadeFinished)
        {
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart(3);
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

    //public void OnQuitButton()
    //{
    //    if (fadeManager.isFadeFinished)
    //    {
    //        fadeManager.fadeMode = FadeManager.FadeMode.close;
    //        fadeManager.FadeStart("Quit()");
    //    }
    //}
}
