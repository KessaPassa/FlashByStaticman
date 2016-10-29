using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    private FadeManager fadeManager;
    private Animator anim;
    private Image image;
    private Sprite baseSprite;
    private bool isOnce = false;
    public Image retryImage;
    private Text retryComment;
    private bool isFadeStart = false;
    private float alpha;
    public float fadeSpeed = 0.5f;
    public Vector3 offset;
    public Button continueButton;
    public Button quitButton;


    void Start() {
        fadeManager = FindObjectOfType<FadeManager>();
        fadeManager.FadeStart(null, fadeSpeed: 1f);

        anim = GetComponent<Animator>();
        image = GetComponent<Image>();
        baseSprite = image.sprite;
        retryComment = retryImage.transform.FindChild("Text").GetComponent<Text>();

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
            SelectArrow.StartSelect();
        }

        if (fadeManager.isFadeFinished && !isOnce)
        {
            isOnce = true;
            anim.SetTrigger("Walking");
        }

        if (!anim.enabled)
        {
            //アニメーションがストップしているときは基本スプライトを表示する
            if (image.sprite != baseSprite)
            {
                image.sprite = baseSprite;
            }
        }
        ////スキップ
        //else if (Input.anyKeyDown)
        //{
        //    transform.position = 
        //        new Vector3(transform.position.x+offset.x, 
        //        transform.position.y + offset.y, 
        //        transform.position.z + offset.z);
        //    EndAnim();
        //}
    }

    public void EndAnim()
    {
        anim.enabled = false;
        isFadeStart = true;
    }

    public void OnContinueButton()
    {
        fadeManager.fadeMode = FadeManager.FadeMode.close;
        fadeManager.FadeStart(3);
    }

    public void OnQuitButton()
    {
        fadeManager.fadeMode = FadeManager.FadeMode.close;
        fadeManager.FadeStart("Title");
    }

}
