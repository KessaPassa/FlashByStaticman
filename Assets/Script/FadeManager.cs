using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {
    public Color fadeColor = Color.black;
    private float alpha;
    public float fadeSpeed = 0.5f;
    public bool isStart = false;
    private int index = -1;

    public enum FadeMode
    {
        open, //黒から白へ+遷移しない
        close //白から黒へ+遷移する
    }
    public FadeMode fadeMode = FadeMode.open;


    void Start () {
	    if(fadeMode == FadeMode.open)
        {
            alpha = 1f; //黒から始まる
            isStart = true;
        }
        else if (fadeMode == FadeMode.close)
        {
            alpha = 0f; //透明から始まる
        }
    }
	
	void Update () {
        if (isStart)
        {
            //フェードインして、現在のシーンが始まる
            if (fadeMode == FadeMode.open)
            {
                alpha -= Time.deltaTime * fadeSpeed;
                if (alpha <= 0)
                {
                    isStart = false;
                }
            }
            //フェードアウトして、次のシーンへ遷移する
            else if (fadeMode == FadeMode.close)
            {
                alpha += Time.deltaTime * fadeSpeed;
                if (alpha >= 1)
                {
                    TransitionScene();
                    isStart = false;
                }
            }
        }
	}

    public void FadeStart(int index)
    {
        this.index = index;
        isStart = true;
    }

    void TransitionScene()
    {
        SceneManager.LoadScene(index);
    }

    void OnGUI()
    {
        fadeColor.a = alpha;
        GUI.color = fadeColor;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
    }
}
