using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public float maxHP = 30;                            //最大体力
    [HideInInspector]
    public float HP;                                    //体力
    private GameObject HPbarPrefab;                     //読み込み用
    private Slider HPbar;                               //体力ゲージ
    public float win = 5;                               //勝った時与えるダメージ
    public float drow = 2;                              //あいこの時与えるダメージ
    //public float lose = 1;                            //負けた時与えるダメージ
    public float lerpTime;                              //値が大きいほど早くなる
    [SerializeField, Range(-1, 4)]
    public int startIndex = -1;                         //初期位置, InitGeneratorのnextPos[]へプリセット
    public Vector2 enemyOffset = new Vector2(0f, 0f);   //エネミーの初期位置の調整
    public Vector2 barOffset = new Vector2(0f, -0f);    //バーの初期位置の調整
    private Canvas enemyCanvas;                         //HPバー用のCanvas
    private ResultCtrl resultCtrl;                      //UI系
    private StaticManager staticManager;
    private bool isDied = false;                        //死んだかどうか
    private float fadeSpeed = 1f;                       //死んだときにフェードするスピード
    public int score;

    public enum LerpMode
    {
        Normal,
        Sin
    }
    public LerpMode lerpMode;

    void Start()
    {
        transform.position = InitGenerator.InitPos(startIndex);
        //初期位置をoffsetを加味した位置にする
        transform.position = new Vector2(
            transform.position.x + enemyOffset.x + 3f,
            transform.position.y + enemyOffset.y
            );

        staticManager = FindObjectOfType<StaticManager>();
        //難易度によってステータスに係数をかける
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
                coefficient = 3;
                break;
        }
        maxHP *= coefficient;
        win *= coefficient;
        drow *= coefficient;
        //lose *= coefficient;
        score *= (int)(coefficient * 2f);

        enemyCanvas = GameObject.FindWithTag("EnemyCanvas").GetComponent<Canvas>();
        HPbarPrefab = Instantiate(Resources.Load("EnemyHPbar")) as GameObject; //HPバー生成
        HPbar = this.HPbarPrefab.GetComponent<Slider>();
        HPbar.name += startIndex;                           //名前セット
        HPbar.transform.SetParent(enemyCanvas.transform);   //親子関連付け
        HP = maxHP;                                         //体力を初期化
        HPbar.maxValue = maxHP;                             //スライダーの最大値を最大体力に合わせる
        HPbar.value = HPbar.maxValue;                       //最大値を変化させた分、初期valueも合わせる
        //位置も合わせる
        HPbar.transform.position = new Vector2(transform.position.x + barOffset.x, transform.position.y - 1.5f + barOffset.y);

        resultCtrl = FindObjectOfType<ResultCtrl>();
    }


    void Update()
    {
        HPbar.value = HP; //受けたダメージをスライダーに反映させる

        if (HP <= 0)
        {
            resultCtrl.isGameStop = true; //ゲームを止める
            HPbar.gameObject.SetActive(false);

            Color alpha = GetComponent<SpriteRenderer>().color;
            alpha.a -= Time.deltaTime * fadeSpeed;
            GetComponent<SpriteRenderer>().color = alpha;

            if(!isDied && GetComponent<SpriteRenderer>().color.a <= 0)
            {
                //死んだらスコアを加算
                if (resultCtrl.scoreTimer > 10f)
                {
                    score *= (int)(resultCtrl.scoreTimer / 10f);
                }
                StaticManager.AddScore(score);
                resultCtrl.EnemyDead();
                isDied = true;
                GetComponent<EnemyStatus>().enabled = false;
            }
        }
    }
}
