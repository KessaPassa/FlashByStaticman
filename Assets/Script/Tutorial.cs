using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
    private FadeManager fadeManager;
    public GameObject slide;
    private int leftCnt = 0;
    private int rightCnt = 0;
    private bool wait = false;
    private AudioSource soundBox;
    public AudioClip selectSE;
    public AudioClip cancelSE;
	
	void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
        soundBox = GameObject.Find("SoundBox").GetComponent<AudioSource>();
	}
	
	
	void Update () {
        if(leftCnt <= 0)
        {
            leftCnt = 0;
        }
        if(rightCnt <= 0)
        {
            rightCnt = 0;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRightButton();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnLeftButton();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)){
            OnReturnButton();
        }
    }

    public void OnRightButton()
    {
        if (!wait && fadeManager.isFadeFinished && 0 <= rightCnt && rightCnt < 2)
        {
            soundBox.PlayOneShot(selectSE, 1f);
            rightCnt++;
            leftCnt--;
            wait = true;
            iTween.MoveTo(slide, iTween.Hash("x", slide.transform.position.x - 18f, "timje", 2f));
            Invoke("Wait", 0.5f);
        }
    }

    public void OnLeftButton()
    {
        if (!wait && fadeManager.isFadeFinished && 0 <= leftCnt && leftCnt < 2)
        {
            soundBox.PlayOneShot(selectSE, 1f);
            rightCnt--;
            leftCnt++;
            wait = true;
            iTween.MoveTo(slide, iTween.Hash("x", slide.transform.position.x + 18f, "timje", 2f));
            Invoke("Wait", 0.5f);
        }
    }

    void Wait()
    {
        wait = false;
    }

    public void OnReturnButton()
    {
        if (!fadeManager.isFading)
        {
            soundBox.PlayOneShot(cancelSE, 1f);
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart("Title");
        }
    }
}
