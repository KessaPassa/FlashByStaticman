using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public int maxHP = 50;                              //最大体力
    [HideInInspector]
    public int HP;                                      //体力
    private GameObject HPbarPrefab;                     //読み込み用
    private Slider HPbar;                               //体力ゲージ
    public int strong = 5;                              //強攻撃
    public int normal = 2;                              //中攻撃
    public int weak = 1;                                //弱攻撃
    public float lerpTime = 1;                          //値が大きいほど早くなる
    [SerializeField, Range(-1, 5)]
    public int startIndex = -1;                         //初期位置, InitGeneratorのnextPos[]へプリセット
    public Vector2 enemyOffset = new Vector2(0f, 0f);   //エネミーの初期位置の調整
    public Vector2 barOffset = new Vector2(0f, -0f);    //バーの初期位置の調整
    private Canvas enemyCanvas;                         //HPバー用のCanvas
    private ResultCtrl resultCtrl;                      //UI系
    private bool isDied = false;                        //死んだかどうか
    private float fadeSpeed = 1f;                       //死んだときにフェードするスピード


    public enum LerpMode
    {
        normal,
        sin
    }
    public LerpMode lerpMode = LerpMode.normal;


    void Start()
    {
        transform.position = InitGenerator.InitPos(startIndex);
        //初期位置をoffsetを加味した位置にする
        transform.position = new Vector2(
            transform.position.x + enemyOffset.x + 3f,
            transform.position.y + enemyOffset.y
            );

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
                resultCtrl.EnemyDead();
                isDied = true;
                GetComponent<EnemyStatus>().enabled = false;
            }
        }
    }
}
