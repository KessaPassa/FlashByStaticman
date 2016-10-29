using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public float maxHP = 100;            //最大体力
    [HideInInspector]
    public float HP;                     //体力
    public Slider HPber;                 //体力ゲージ
    public float win = 5;                //強攻撃
    public float drow = 2;               //中攻撃
    //public float lose = 1;             //弱攻撃
    private FadeManager fadeManager;     //シーン遷移の時に使う
    private StaticManager staticManager; //staticで保存したい値系
    private BattleRSP battleRSP;         //じゃんけん系
    private bool isDied = false;         //死んだかどうか
    private Vector2 startPos;            //pingpong関数フワフワするのに使う


    void Start()
    {
        staticManager = FindObjectOfType<StaticManager>();
        float coefficient = 1;
        switch (staticManager.diffycultyMode)
        {
            case StaticManager.DifficultyMode.Easy:
                coefficient = 1;
                break;

            case StaticManager.DifficultyMode.Normal:
                coefficient = 1.5f;
                break;

            case StaticManager.DifficultyMode.Hard:
                coefficient = 2;
                break;
        }
        maxHP *= coefficient;
        win *= coefficient;
        drow *= coefficient;
        //lose *= coefficient;

        HP = maxHP;                     //体力を初期化
        HPber.maxValue = maxHP;         //スライダーの最大値を最大体力に合わせる
        HPber.value = HPber.maxValue;   //最大値を変化させた分、初期valueも合わせる
        fadeManager = FindObjectOfType<FadeManager>();
        battleRSP = FindObjectOfType<BattleRSP>();
        startPos = transform.position;
    }


    void Update()
    {
        HPber.value = HP; //受けたダメージをスライダーに反映させる

        //プレイヤー死亡時
        if (HP <= 0 && !isDied)
        {
            isDied = true;
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart(sceneName: "GameOver", fadeSpeed: 1f);
            battleRSP.EndGame();
        }

        //上下にフワフワ揺らす
        float range = 0.2f;
        var pingpong = Mathf.PingPong(Time.time * 0.1f, range / 2f) - range;
        transform.position = new Vector2(startPos.x, startPos.y + pingpong);
    }
}
