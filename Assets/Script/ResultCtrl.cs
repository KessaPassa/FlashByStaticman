﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class ResultCtrl : MonoBehaviour
{
    private GameObject player;          //Playerを取得
    private GameObject[] enemys;        //Enemyを全て取得
    private PlayerStatus playerStatus;  //プレーヤーのステータス
    private EnemyStatus enemyStatus;    //敵のステータス
    private BattleRSP battleRSP;        //Input系の制御script
    private MoveStage moveStage;        //敵を撃破時に移動制御するscript
    private AudioSource SEBox;          //PlayOneShot用の空箱
    private Animator anim;              //Animation用の空箱
    public Image result;                //じゃんけん判定を表示
    public Sprite imageWin;             //勝ち画像
    public Sprite imageDrow;            //引き分け画像
    public Sprite imageLose;            //負け画像
    public Sprite imageCongra;          //終了画像
    public Sprite imageinvisible;       //透明の画像
    public bool isGameStop = true;      //ゲームが動いているかどうか
    public Image startAtClick;          //クリックしてスタートの画像
    

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");//.ToArray();
        enemys = enemy.OrderBy(e => Vector3.Distance(e.transform.position, transform.position)).ToArray(); //距離順でソートする

        playerStatus = FindObjectOfType<PlayerStatus>();
        battleRSP = FindObjectOfType<BattleRSP>();
        moveStage = FindObjectOfType<MoveStage>();
        SEBox = GameObject.Find("SEBox").GetComponent<AudioSource>();
        anim = GameObject.Find("AnimCtrl").GetComponent<Animator>();
        enemyStatus = enemys[moveStage.GetWinCount()].GetComponent<EnemyStatus>();

        anim.SetTrigger("Stamp"); //スタンプを表示
    }

    
    void Update()
    {
        IsPlayGame();   //ゲームが動いているか
        AfterDead();    //プレーヤーまたは敵の死亡時なにをするか

        float pingpong = Mathf.PingPong(Time.time * 1.2f, 1f);      //pingpong関数で0と1を行ったり来たり
        startAtClick.color = new Color(startAtClick.color.r, startAtClick.color.g, startAtClick.color.b, pingpong); //点めつするようにする
    }

    void IsPlayGame()
    {
        //ゲームが止まっているなら
        if (isGameStop)
        {
            //クリックかエンターを押すと、スタンプが消えゲームスタート
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                startAtClick.enabled = false;   //点滅を消す
                isGameStop = false;
                EndAnim();
                battleRSP.StartGame();
            }
            else
            {
                battleRSP.enabled = false;
            }
        }
        //止まっていないならscriptをオンにして正常に動かす
        else
        {
            battleRSP.enabled = true;
        }
    }

    void AfterDead()
    {
        //プレイヤー死亡時
        if (playerStatus.HP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        //敵撃破時
        else if (enemyStatus.HP <= 0)
        {
            //敵がまだ居るのならステージ移動する
            if (moveStage.GetWinCount() < moveStage.nextPos.Length - 1)
            {
                result.GetComponent<Image>().sprite = imageinvisible; //ステージ移動時は画像が無いようにする
                moveStage.NextStage(); //ステージ移動
                enemyStatus = enemys[moveStage.GetWinCount()].GetComponent<EnemyStatus>(); //新しい敵のステータスを取得
            }
            //もう居ないのなら終了
            else
            {
                battleRSP.enabled = false;
                result.GetComponent<Image>().sprite = imageCongra;
            }
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
        if (playerRSP == 0)
        {
            anim.SetTrigger("Rock");
        }
        else if (playerRSP == 1)
        {
            anim.SetTrigger("Scissors");
        }
        else if (playerRSP == 2)
        {
            anim.SetTrigger("Paper");
        }
    }

    public void Win()
    {
        enemyStatus.HP -= 3;
        AudioClip SE = Resources.Load("strong") as AudioClip; //強攻撃の効果音を取得
        SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
        result.GetComponent<Image>().sprite = imageWin;
        iTween.MoveFrom(player, iTween.Hash("x", player.transform.position.x + 0.4f, "time", 0.01f)); //攻撃時にブレさせる
    }

    public void Drow()
    {
        playerStatus.HP -= 1;
        enemyStatus.HP -= 1;
        AudioClip SE = Resources.Load("normal") as AudioClip; //強攻撃の効果音を取得
        SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
        result.GetComponent<Image>().sprite = imageDrow;
    }

    public void Lose()
    {
        playerStatus.HP -= 3;
        AudioClip SE = Resources.Load("damaged") as AudioClip; //強攻撃の効果音を取得
        SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
        result.GetComponent<Image>().sprite = imageLose;
        foreach (GameObject e in enemys) {
            iTween.MoveFrom(e, iTween.Hash("x", e.transform.position.x - 0.4f, "time", 0.01f)); //攻撃時にブレさせる
        }
    }
}