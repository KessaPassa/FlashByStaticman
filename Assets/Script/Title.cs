using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Title : MonoBehaviour {
    private FadeManager fadeManager;
    private StaticManager staticManager;
    private TitleSelect titleSelect;
    private AudioSource soundBox;
    public AudioClip decitionSE;
    public AudioClip cancelSE;
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    private int isOnce = 1;

    void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
        staticManager = FindObjectOfType<StaticManager>();
        titleSelect = FindObjectOfType<TitleSelect>();
        soundBox = GameObject.Find("SoundBox").GetComponent<AudioSource>();
    }
	
	
	void Update () {
        if (fadeManager.isFadeFinished && isOnce == 1)
        {
            isOnce++;
            titleSelect.StartSelect();
        }
        
        if (Input.GetKeyDown(KeyCode.Backspace) && titleSelect.isStartSelect)
        {
            easyButton.enabled = false;
            normalButton.enabled = false;
            hardButton.enabled = false;

            soundBox.PlayOneShot(cancelSE, 1f);
            titleSelect.isOnChild = false;
            titleSelect.selectButton[0].Select();
            titleSelect.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void OnStartButton()
    {
        if (fadeManager.isFadeFinished)
        {
            easyButton.enabled = true;
            normalButton.enabled = true;
            hardButton.enabled = true;

            soundBox.PlayOneShot(decitionSE, 1f);
            titleSelect.isOnChild = true;
            titleSelect.childButton[0].Select();
            titleSelect.gameObject.transform.rotation = new Quaternion(0, 0, 180, 0);
        }
    }

    public void OnTutorialButton()
    {
        if (fadeManager.isFadeFinished)
        {
            titleSelect.isStartSelect = false;
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

    public void OnEasy()
    {
        titleSelect.isStartSelect = false;
        soundBox.PlayOneShot(decitionSE, 1f);
        staticManager.ChangeMode(StaticManager.DifficultyMode.Easy);
        fadeManager.fadeMode = FadeManager.FadeMode.close;
        fadeManager.FadeStart(3);
    }

    public void OnNormal()
    {
        titleSelect.isStartSelect = false;
        soundBox.PlayOneShot(decitionSE, 1f);
        staticManager.ChangeMode(StaticManager.DifficultyMode.Normal);
        fadeManager.fadeMode = FadeManager.FadeMode.close;
        fadeManager.FadeStart(4);
    }

    public void OnHard()
    {
        titleSelect.isStartSelect = false;
        soundBox.PlayOneShot(decitionSE, 1f);
        staticManager.ChangeMode(StaticManager.DifficultyMode.Hard);
        fadeManager.fadeMode = FadeManager.FadeMode.close;
        fadeManager.FadeStart(5);
    }
}
