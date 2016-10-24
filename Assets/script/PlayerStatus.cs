using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public int maxHP = 50;              //最大体力
    [HideInInspector]
    public int HP;                      //体力
    public Slider HPber;                //体力ゲージ
    public int strong = 5;              //強攻撃
    public int normal = 2;              //中攻撃
    public int weak = 1;                //弱攻撃
    private FadeManager fadeManager;    //シーン遷移の時に使う
    private BattleRSP battleRSP;        //じゃんけん系
    private bool isDied = false;        //死んだかどうか
    private Vector2 startPos;           //pingpong関数フワフワするのに使う


    void Start()
    {
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
