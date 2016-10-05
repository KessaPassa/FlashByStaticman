using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {
    public Color fadeColor = Color.black;   //どの色からフェードを始めるか
    private float alpha;                    //アルファ値
    public bool isFading = false;           //フェードしているか否か
    public bool isFadeFinished = false;     //フェードが終わったか否か

    private int sceneIndex;                 //遷移するシーンの番号
    private string sceneName;               //遷移するシーンの名前
    private float fadeSpeed = 0.5f;         //値が大きいほど早くフェードする
    private float waitForSeconds;           //コルーチンの待ち時間


    public enum FadeMode
    {
        none = -1,  //すぐに遷移する
        open,       //黒から白へ+遷移しない
        close       //白から黒へ+遷移する
    }
    public FadeMode fadeMode = FadeMode.open;


    void Start () {
	    if(fadeMode == FadeMode.open)
        {
            alpha = 1f; //黒から始まる
            isFading = true;
        }
        else if (fadeMode == FadeMode.close)
        {
            alpha = 0f; //透明から始まる
        }
    }
	
	void Update () {
        if (isFading)
        {
            //フェードインして、現在のシーンが始まる
            if (fadeMode == FadeMode.open)
            {
                alpha -= Time.deltaTime * fadeSpeed;
                if (alpha <= 0)
                {
                    StartCoroutine(FadeFinished(alpha));
                }
            }
            //フェードアウトして、次のシーンへ遷移する
            else if (fadeMode == FadeMode.close)
            {
                alpha += Time.deltaTime * fadeSpeed;
                if (alpha >= 1)
                {
                    StartCoroutine(FadeFinished(alpha));                    
                }
            }
            //noneなら
            else
            {
                TransitionScene();
            }
        }
	}

    //ここにシーン番号を引数にしてアクセスするとフェードが始まる
    public void FadeStart(int sceneIndex, float fadeSpeed = 0.5f, float waitForSeconds = 0f)
    {
        this.sceneIndex = sceneIndex;           //シーン遷移の番号
        this.fadeSpeed = fadeSpeed;             //フェードする速さ
        this.waitForSeconds = waitForSeconds;   //シーン遷移までの時間
        isFading = true;
    }

    //ここにシーン名前を引数にしてアクセスするとフェードが始まる
    public void FadeStart(string sceneName, float fadeSpeed = 0.5f, float waitForSeconds = 0f)
    {
        this.sceneName = sceneName;             //シーン遷移の名前
        this.fadeSpeed = fadeSpeed;             //フェードする速さ
        this.waitForSeconds = waitForSeconds;   //シーン遷移までの時間
        isFading = true;
    }

    //public void ChangeMode(string mode)
    //{

    //}

    IEnumerator FadeFinished(float alpha)
    {
        isFading = false;
        //指定秒数待つ
        yield return new WaitForSeconds(waitForSeconds);

        //画面が黒ならシーン遷移する
        if (alpha >= 1)
        {
            //シーン遷移
            TransitionScene();  
        }
        isFadeFinished = true;
    }

    void TransitionScene()
    {
        if (sceneIndex != -1)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else if(sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    void OnGUI()
    {
        fadeColor.a = alpha;
        GUI.color = fadeColor;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
    }
}
