using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial2 : MonoBehaviour {
    private FadeManager fadeManager;
    public Sprite[] slide;
    private int leftCnt = 0;
    private int rightCnt = 0;
    private AudioSource soundBox;
    public AudioClip selectSE;
    public AudioClip cancelSE;
    private int totalCnt = 0;

    void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>();
        soundBox = GameObject.Find("SoundBox").GetComponent<AudioSource>();
    }


    void Update()
    {
        if (!fadeManager.isFadeFinished)
        {
            return;
        }

        if (leftCnt <= 0)
        {
            leftCnt = 0;
        }
        if (rightCnt <= 0)
        {
            rightCnt = 0;
        }

        //totalCnt = rightCnt - leftCnt;
        if(totalCnt <= 0)
        {
            totalCnt = 0;
        }
        else if(2<= totalCnt)
        {
            totalCnt = 2;
        }

        GetComponent<Image>().sprite = slide[totalCnt];

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRightButton();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnLeftButton();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            OnReturnButton();
        }
    }

    public void OnRightButton()
    {
        if (0 <= totalCnt && totalCnt <= 1)
        {
            soundBox.PlayOneShot(selectSE, 1f);
            totalCnt++;
        }
    }

    public void OnLeftButton()
    {
        if (1 <= totalCnt && totalCnt <= 2)
        {
            soundBox.PlayOneShot(selectSE, 1f);
            totalCnt--;
        }
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
