using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class ResultCtrl : MonoBehaviour
{
    private GameObject[] enemys;        //Enemyを全て取得
    private PlayerStatus playerStatus;  //プレーヤーのステータス
    private PlayerAnim playerAnim;
    [HideInInspector]
    public EnemyStatus enemyStatus;     //敵のステータス
    private BattleRSP battleRSP;        //Input系の制御script
    private MoveStage moveStage;        //敵を撃破時に移動制御するscript
    private FadeManager fadeManager;    //フェードを管理するスクリプト
    private AudioSource SoundBox;       //PlayOneShot用の空箱
    public Animator anim;               //Animation用の空箱
    public Image result;                //じゃんけん判定を表示
    public Sprite imageCongra;          //終了画像
    public Sprite imageinvisible;       //透明の画像
    public bool isGameStop = true;      //ゲームが動いているかどうか
    private bool isGameEnd = false;     //クリア or ゲームオーバーしてゲームが終わったか
    public Image startAtClick;          //クリックしてスタートの画像
    public Image deadLine;              //この線にじゃんけんの手が触れたらアウト
    public Image predictBg;             //予測手の背景
    public Image predictHand;           //次の手の予測
    private bool isOnHidden = false;    //手が隠されているか否か


    void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        playerAnim = FindObjectOfType<PlayerAnim>();
        battleRSP = FindObjectOfType<BattleRSP>();
        moveStage = FindObjectOfType<MoveStage>();
        //initGenerator = FindObjectOfType<InitGenerator>();
        fadeManager = FindObjectOfType<FadeManager>();
        SoundBox = GameObject.Find("SoundBox").GetComponent<AudioSource>();
        anim = GameObject.FindWithTag("AnimCtrl").GetComponent<Animator>();
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");//.ToArray();
        enemys = enemy.OrderBy(e => Vector2.Distance(e.transform.position, transform.position)).ToArray(); //距離順でソートする
        enemyStatus = enemys[moveStage.winCounter].GetComponent<EnemyStatus>();

        anim.SetTrigger("Stamp"); //スタンプを表示
        fadeManager.FadeStart(null,waitForSeconds: 0f);  //指定秒待ってからスタートする
    }

    
    void Update()
    {
        //ゲームが終わっていれば何もしない
        if (isGameEnd)
        {
            battleRSP.enabled = false;
            return;
        }
        IsPlayGame();   //ゲームが動いているか
        OnHidden();

        float pingpong = Mathf.PingPong(Time.time * 1.2f, 1f); //pingpong関数で0と1を行ったり来たり
        startAtClick.color = new Color(startAtClick.color.r, startAtClick.color.g, startAtClick.color.b, pingpong); //点めつするようにする
    }

    void IsPlayGame()
    {
        //ゲームが止まっているなら
        if (isGameStop)
        {
            //なにか押すと、スタンプが消えゲームスタート
            if (fadeManager.isFadeFinished && Input.anyKeyDown)
            //if(fadeManager.isFadeFinished && Input.anyKeyDown && playerStatus.gameObject.transform.position.x >= InitGenerator.nextPos[moveStage.winCounter].transform.position.x - 3f)
            {
                isGameStop = false;
                deadLine.enabled = true;
                predictBg.enabled = true;
                startAtClick.enabled = false;   //点滅を消す
                EndAnim();
                battleRSP.StartGame();
            }
            else
            {
                predictHand.sprite = imageinvisible;
                deadLine.enabled = false;
                predictBg.enabled = false;
                battleRSP.enabled = false;
            }
        }
        //止まっていないならscriptをオンにして正常に動かす
        else
        {
            battleRSP.enabled = true;
            predictHand.sprite = battleRSP.appearHand[1].sprite;
        }
    }

    public void EnemyDead()
    { 
        //敵がまだ居るのならステージ移動する
        if (moveStage.winCounter < InitGenerator.nextPos.Length - 1)
        {
            //result.GetComponent<Image>().sprite = imageinvisible; //ステージ移動時は画像が無いようにする
            moveStage.NextStage(GetComponent<ResultCtrl>(), battleRSP); //ステージ移動
            enemyStatus = enemys[moveStage.winCounter].GetComponent<EnemyStatus>(); //新しい敵のステータスを取得
        }
        //もう居ないのなら終了
        else
        {
            isGameStop = true;
            isGameEnd = true;
            result.GetComponent<Image>().sprite = imageCongra;
            fadeManager.isFadeFinished = false;
            fadeManager.fadeMode = FadeManager.FadeMode.close;
            fadeManager.FadeStart("Title", waitForSeconds: 3f);
        }
    }

    //アニメーションを終了させる
    public void EndAnim()
    {
        anim.SetTrigger("Wait");
    }

    //勝ったときの手でアニメーションを変更
    public void StartAnim(int playerRSP)
    {
        string trigger = null; ;
        if (playerRSP == 0)
        {
            trigger = "Rock";
        }
        else if (playerRSP == 1)
        {
            trigger = "Scissors";
        }
        else if (playerRSP == 2)
        {
            trigger = "Paper";
        }
        anim.SetTrigger(trigger);
        playerAnim.AttackAnim(trigger);
    }

    //敵の次の手を隠す
    void OnHidden()
    {
        //スペースを押したらHiddenを実行する(プロトタイプ)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOnHidden = !isOnHidden; //ボタン1つでオン・オフ
        }
        //trueなら1つ目以外を隠す
        if (isOnHidden)
        {
            predictHand.enabled = false;
        }
        //falseなら通常通り、全部表示する
        else
        {
            predictHand.enabled = true;
        }
    }

    public void Win()
    {
        enemyStatus.HP -= 3;
        AudioClip SE = Resources.Load("strong") as AudioClip; //強攻撃の効果音を取得
        SoundBox.PlayOneShot(SE, 3f); //効果音を鳴らす
    }

    public void Drow()
    {
        playerStatus.HP -= 1;
        enemyStatus.HP -= 1;
        AudioClip SE = Resources.Load("normal") as AudioClip; //強攻撃の効果音を取得
        SoundBox.PlayOneShot(SE, 3f); //効果音を鳴らす
    }

    public void Lose()
    {
        playerStatus.HP -= 3;
        AudioClip SE = Resources.Load("damaged") as AudioClip; //強攻撃の効果音を取得
        SoundBox.PlayOneShot(SE, 3f); //効果音を鳴らす
    }
}
