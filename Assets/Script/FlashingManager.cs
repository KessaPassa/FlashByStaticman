using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class FlashingManager : MonoBehaviour
{
    public GameObject targetObject;             //透過したいオブジェクト
    public Image targetImage;                   //透過したいオブジェクト
    public Text targetText;                     //透過したいオブジェクト
    private float alpha;                        //アルファ値
    public float fadeSpeed;                     //フェードする速さ
    [SerializeField, TooltipAttribute("ComponentTypeがGameObject型のときのみ使用する")]
    public float fadeTime;                      //GameObjectのiTween用
    public int isRepeat;                        //何回リピートするのか
    private int repeatCount = 0;                //リピートした回数
    public bool infinityRepeat = false;         //無限ループするかしないか
    public bool isFadeStart = false;            //フェードを始めても良いか
    public bool isFadeFinished = false;         //フェードが終わったか
    public bool isFadeOutFinished = false;      //フェードアウトが終わったか
    public bool isFadeInFinished = false;       //フェードインが終わったか
    public bool isAllRepeatFinished = false;    //リピートも終わり、すべてのフェードが終わったか

    //アルファ値がどこまでの間を通るか
    [SerializeField, Range(0f, 1.0f)]
    public float minAlpha;                      
    [SerializeField, Range(0f, 1.0f)]
    public float maxAlpha;

    //使用するのタイプを決める
    public enum ComponentType
    {
        GameObject,
        Image,
        Text
    }
    public ComponentType componentType;

    //フェードタイプ
    public enum FadeType
    {
        Out,        //元から透明へ
        In          //透明から元へ
    }
    public FadeType fadeType;


    void Start()
    {
        //使用するタイプを初期化
        switch (componentType)
        {
            case ComponentType.GameObject:
                InspectExist_GameObject();
                break;

            case ComponentType.Image:
                InspectExist_Image();
                break;
            case ComponentType.Text:
                InspectExist_Text();
                break;

            default:
                break;
        }
        InitAlpha();
    }

    //アルファ値を初期化
    void InitAlpha()
    {
        //フェードイン, フェードアウトでアルファ値の初期値を変える
        if (fadeType == FadeType.In)
        {
            alpha = minAlpha; //元から始まる
        }
        else if (fadeType == FadeType.Out)
        {
            alpha = maxAlpha; //透明から始まる
        }
        isFadeOutFinished = false;
        isFadeInFinished = false;
    }


    void Update()
    {
        if (isFadeStart)
        {
            //フェードが終わったかどうか
            IsFadeFinished();

            //フェードする
            switch (componentType)
            {
                case ComponentType.GameObject:
                    FadeObject();
                    break;

                case ComponentType.Image:
                    FadeImage();
                    break;
                case ComponentType.Text:
                    FadeText();
                    break;

                default:
                    break;
            }

            if(isFadeOutFinished || isFadeInFinished)
            {
                isFadeFinished = true;
            }
            else
            {
                isFadeFinished = false;
            }

            //フェードをリピートする
            if (isFadeFinished && (repeatCount < isRepeat || infinityRepeat))
            {
                isFadeFinished = false;
                repeatCount++;
                FadeRepeat();
            }
            else if(repeatCount >= isRepeat && !infinityRepeat)
            {
                isAllRepeatFinished = true;
            }
        }
    }

    //フェードが終わったかどうか
    void IsFadeFinished()
    {
        if (fadeType == FadeType.In)
        {
            if (alpha >= maxAlpha)
            {
                //これ以上上げない
                alpha = maxAlpha;
                isFadeInFinished = true;

            }
        }
        else if (fadeType == FadeType.Out)
        {
            if (alpha <= minAlpha)
            {
                //これ以上下げない
                alpha = minAlpha;
                isFadeOutFinished = true;
            }
        }
    }

    //リピートする
    void FadeRepeat()
    {
        if (fadeType == FadeType.In)
        {
            fadeType = FadeType.Out;
        }
        else if (fadeType == FadeType.Out)
        {
            fadeType = FadeType.In;
        }
        InitAlpha();
    }

    void InspectExist_GameObject()
    {
        //存在するなら, nullじゃないなら
        if (!targetObject)
        {
            //Nullにする
            Assert.IsNull(targetObject);
        }  

    }

    void InspectExist_Image()
    {
        //存在しないなら, nullじゃないなら
        if (!targetImage)
        {
            //Nullにする
            Assert.IsNull(targetImage);
        }
    }

    void InspectExist_Text()
    {
        //存在しないなら, nullじゃないなら
        if (!targetText)
        {
            //Nullにする
            Assert.IsNull(targetText);
        }
    }

    //GameObject型をフェードする
    void FadeObject()
    {
        if (fadeType == FadeType.In)
        {
            iTween.FadeTo(targetObject, iTween.Hash("a", maxAlpha, "time", fadeTime));
        }
        else if (fadeType == FadeType.Out)
        {
            iTween.FadeTo(targetObject, iTween.Hash("a", minAlpha, "time", fadeTime));
        }
    }

    //Image型をフェードする
    void FadeImage()
    {
        if (fadeType == FadeType.In)
        {
            alpha += Time.deltaTime * fadeSpeed;
        }
        else if (fadeType == FadeType.Out)
        {
            alpha -= Time.deltaTime * fadeSpeed;
        }
        Color tmp = targetImage.color;
        tmp.a = alpha;
        targetImage.color = tmp;
    }

    //Text型をフェードする
    void FadeText()
    {
        if (fadeType == FadeType.In)
        {
            alpha += Time.deltaTime * fadeSpeed;
        }
        else if (fadeType == FadeType.Out)
        {
            alpha -= Time.deltaTime * fadeSpeed;
        }
        Color tmp = targetText.color;
        tmp.a = alpha;
        targetText.color = tmp;
    }

    //ここにアクセスするとフェードを始める
    public void FadeStart()
    {
        isFadeStart = true;
    }
}
