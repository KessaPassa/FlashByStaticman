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
    public GameObject allButton;
    public GameObject allImage;
    private int isOnce = 1;

    //裏コード用
    private bool s = false;
    private bool t = false;
    private bool a = false;
    private bool t2 = false;
    private bool i = false;
    private bool c = false;
    private bool isSwitch = false;
    private float timer = 0f;
    public Text hiddenText;

    void Start () {
        fadeManager = FindObjectOfType<FadeManager>();
        staticManager = FindObjectOfType<StaticManager>();
        titleSelect = FindObjectOfType<TitleSelect>();
        soundBox = GameObject.Find("SoundBox").GetComponent<AudioSource>();
    }
	
	
	void Update () {
        HiddenCmd();

        if (fadeManager.isFadeFinished && isOnce == 1)
        {
            isOnce++;
            titleSelect.StartSelect();
        }
        
        if (Input.GetKeyDown(KeyCode.Backspace) && titleSelect.isStartSelect)
        {
            allButton.gameObject.SetActive(false);
            allImage.gameObject.SetActive(false);

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
            allButton.gameObject.SetActive(true);
            allImage.gameObject.SetActive(true);

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

    void HiddenText()
    {
        hiddenText.enabled = false;
    }

    void HiddenCmd()
    {
        if (StaticManager.isHiddenCmd && !isSwitch)
        {
            isSwitch = true;
            hiddenText.enabled = true;
            Invoke("HiddenText", 4f);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            s = true;
            print("1");
        }
        else if (Input.GetKeyDown(KeyCode.T) && s)
        {
            t = true;
            print("2");
        }
        else if (Input.GetKeyDown(KeyCode.A) && t)
        {
            a = true;
            t = false;
            print("3");
        }
        if (Input.GetKeyDown(KeyCode.T) && a)
        {
            t2 = true;
            print("4");
        }
        else if (Input.GetKeyDown(KeyCode.I) && t2)
        {
            i = true;
            print("5");
        }
        else if (Input.GetKeyDown(KeyCode.C) && i)
        {
            StaticManager.isHiddenCmd = true;
            print("static" + StaticManager.isHiddenCmd);
        }
        else
        {
            if (timer >= 5f)
            {
                timer = 0f;
                TooLate();
            }
        }
        if (Input.anyKeyDown)
        {
            timer = 0f;
        }
        timer += Time.deltaTime;
    }

    void TooLate()
    {
        if (!StaticManager.isHiddenCmd)
        {
            s = false;
            t = false;
            a = false;
            t2 = false;
            i = false;
            c = false;
        }
    }
}
