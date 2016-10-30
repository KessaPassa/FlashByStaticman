using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    private FadeManager fadeManager;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public Sprite baseSprite;
    private bool isOnce = false;
    public Image retryImage;
    private Text retryComment;
    private bool isFadeStart = false;
    private float alpha;
    public float fadeSpeed = 0.5f;
    public Vector3 offset;
    public Button continueButton;
    public Button quitButton;
    public AudioSource soundBox;
    public AudioClip decitionSE;
    private StaticManager staticManager;
    private SelectArrow selectArrow;


    void Start() {
        fadeManager = FindObjectOfType<FadeManager>();
        fadeManager.FadeStart(null, fadeSpeed: 1f);

        anim = GetComponent<Animator>();
        //image = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //baseSprite = image.sprite;
        retryComment = retryImage.transform.FindChild("Text").GetComponent<Text>();
        staticManager = FindObjectOfType<StaticManager>();
        selectArrow = FindObjectOfType<SelectArrow>();

        //見えないようにしておく
        retryImage.color = new Color(255, 255, 255, 0);
        retryComment.color = new Color(50, 50, 50, 0);


        continueButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }


    void Update()
    {
        if (isFadeStart)
        {
            alpha += Time.deltaTime * fadeSpeed;
            retryImage.color = new Color(255, 255, 255, alpha);
            retryComment.color = new Color(0.2f, 0.2f, 0.2f, alpha);
        }

        if(alpha >= 1)
        {
            //selectArrow.enabled = true;
            continueButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            selectArrow.StartSelect();
        }

        if (fadeManager.isFadeFinished && !isOnce)
        {
            isOnce = true;
            anim.SetTrigger("Walking");
        }

        if (!anim.enabled)
        {
            //アニメーションがストップしているときは基本スプライトを表示する
            if (spriteRenderer.sprite != baseSprite)
            {
                spriteRenderer.sprite = baseSprite;
            }
        }
        //スキップ
        else if (Input.anyKeyDown)
        {
            transform.position = new Vector2(-1.22f, transform.position.y);
            EndAnim();
        }
    }

    public void EndAnim()
    {
        anim.enabled = false;
        isFadeStart = true;
    }

    public void OnContinueButton()
    {
        if (!fadeManager.isFading)
        {
            int index = -1;
            switch (staticManager.difficultyMode)
            {
                case StaticManager.DifficultyMode.Easy:
                    index = 3;
                    break;

                case StaticManager.DifficultyMode.Normal:
                    index = 4;
                    break;

                case StaticManager.DifficultyMode.Hard:
                    index = 5;
                    break;
            }

            soundBox.PlayOneShot(decitionSE, 1f);
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart(index);
        }
    }

    public void OnQuitButton()
    {
        if (!fadeManager.isFading)
        {
            soundBox.PlayOneShot(decitionSE, 1f);
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart("Title");
        }
    }
}
