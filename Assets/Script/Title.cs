using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    private FadeManager fadeManager;
    private AudioSource soundBox;
    public AudioClip decitionSE;

	void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
        soundBox = GameObject.Find("SoundBox").GetComponent<AudioSource>();
        SelectArrow.StartSelect();
    }
	
	
	void Update () {
        
    }

    public void OnStartButton()
    {
        if (fadeManager.isFadeFinished)
        {
            SelectArrow.isStartSelect = false;
            soundBox.PlayOneShot(decitionSE, 1f);
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart(3);
        }
    }

    public void OnTutorialButton()
    {
        if (fadeManager.isFadeFinished)
        {
            SelectArrow.isStartSelect = false;
            soundBox.PlayOneShot(decitionSE, 1f);
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
