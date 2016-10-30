using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
    private FadeManager fadeManager;
    public GameObject slide;
    private int leftCnt = 0;
    private int rightCnt = 0;
    private bool wait = false;
	
	void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
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
    }

    public void OnRightButton()
    {
        if (!wait && fadeManager.isFadeFinished && 0 <= rightCnt && rightCnt < 2)
        {
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
        fadeManager.fadeMode = FadeManager.FadeMode.close;
        fadeManager.FadeStart("Title");
    }
}
