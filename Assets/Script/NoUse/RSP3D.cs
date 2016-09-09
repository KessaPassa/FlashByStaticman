using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RSP3D : MonoBehaviour
{
    public Sprite rock;                 //グー
    public Sprite scissors;             //チョキ
    public Sprite paper;                //パー
    public GameObject canvas;           //Imageインスタンスを入れるためのcanvas
    public Image[] enemyHand;           //インスタンスの元となるオブジェクト
    private PlayerStatus playerStatus;  //プレーヤーのステータス
    private EnemyStatus enemyStatus;    //敵のステータス
    private AudioSource SEBox;          //PlayOneShot用の空箱
    private bool isHiddenOn = false;    //手が隠されているか否か
    public Image result;                //じゃんけん判定を表示
    public Sprite imageWin;             //勝利画像
    public Sprite imageDrow;            //引き分け画像
    public Sprite imageLose;            //負け画像


    void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        enemyStatus = FindObjectOfType<EnemyStatus>();
        SEBox = GameObject.Find("SEBox").GetComponent<AudioSource>();

        for (int i = 0; i < enemyHand.Length; i++)
        {
            enemyHand[i].GetComponent<Image>().sprite = GetNextHand(); // = GetNextHand(); //手の絵を入れる
            enemyHand[i].transform.SetParent(canvas.transform);        //canvasと親子にする。UIなのでcanvasがないと生きられない
            enemyHand[i].tag = SetTag(enemyHand[i].GetComponent<Image>().sprite);
        }
    }


    void Update()
    {
        HiddenOn(); //手を隠す
        
        //自分の手を取得
        int playerRSP = -1; 
        //グー
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerRSP = 0;
        }
        //チョキ
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerRSP = 1;
        }
        //パー
        else if (Input.GetKeyDown(KeyCode.D))
        {
            playerRSP = 2;
        }

        CheckRSP(playerRSP);
    }

    //敵の手を隠す
    void HiddenOn()
    {
        //スペースを押したらHiddenを実行する(プロトタイプ)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isHiddenOn = !isHiddenOn; //ボタン1つでオン・オフ
        }
        //trueなら1つ目以外を隠す
        if (isHiddenOn)
        {
            enemyHand[0].GetComponent<Image>().enabled = true;
            enemyHand[1].GetComponent<Image>().enabled = false;
            enemyHand[2].GetComponent<Image>().enabled = false;
        }
        //falseなら通常通り、3つ表示する
        else
        {
            enemyHand[1].GetComponent<Image>().enabled = true;
            enemyHand[2].GetComponent<Image>().enabled = true;
        }
    }

    //自分の手と敵の手を判決
    void CheckRSP(int playerRSP)
    {
        //敵の手を判定
        int enemyRSP = -1;
        if (this.enemyHand[0].tag == "Rock")
        {
            enemyRSP = 0;
        }
        else if (this.enemyHand[0].tag == "Scissors")
        {
            enemyRSP = 1;
        }
        else if (this.enemyHand[0].tag == "Paper")
        {
            enemyRSP = 2;
        }

        //手が設定されていないなら回さない
        if (playerRSP == -1 || enemyRSP == -1)
        {
            return;
        }

        //勝敗の計算
        int bout = (enemyRSP - playerRSP + 3) % 3;
        //引き分け
        if (bout == 0)
        {
            Drow();
        }
        //勝ち
        else if (bout == 1)
        {
            Win();
        }
        //負け
        else if (bout == 2)
        {
            Lose();
        }

        MoveHand();
    }

    //ランダムでじゃんけんの手を決める
    Sprite GetNextHand()
    {
        Sprite hand = null;
        int enemyHand = Random.Range(0, 3);
        switch (enemyHand)
        {
            case 0:
                hand = rock;
                break;

            case 1:
                hand = scissors;
                break;

            case 2:
                hand = paper;
                break;
        }

        return hand;
    }

    //手前を消し、1つずつずらし、最後尾に手を追加する
    void MoveHand()
    {
        //画像を1つずつ前にずらす
        enemyHand[0].sprite = enemyHand[1].sprite;
        enemyHand[1].sprite = enemyHand[2].sprite;

        //tagを1つずつ前にずらす
        enemyHand[0].tag = enemyHand[1].tag;
        enemyHand[1].tag = enemyHand[2].tag;

        enemyHand[2].GetComponent<Image>().sprite = GetNextHand();              //ずらして空になったところに絵を入れる
        enemyHand[2].transform.SetParent(canvas.transform);                     //canvasと親子にする。UIなのでcanvasがないと生きられない
        enemyHand[2].tag = SetTag(enemyHand[2].GetComponent<Image>().sprite);   //tagを設定
    }

    string SetTag(Sprite obj)
    {
        string tagName = null;
        if (obj == rock)
        {
            tagName = "Rock";
        }
        else if (obj == scissors)
        {
            tagName = "Scissors";
        }
        else if (obj == paper)
        {
            tagName = "Paper";
        }

        return tagName;
    }

    void Win()
    {
        enemyStatus.HP -= 3;
        AudioClip SE = Resources.Load("strong") as AudioClip; //強攻撃の効果音を取得
        SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
        result.GetComponent<Image>().sprite = imageWin;
    }

    void Drow()
    {
        playerStatus.HP -= 1;
        enemyStatus.HP -= 1;
        AudioClip SE = Resources.Load("normal") as AudioClip; //強攻撃の効果音を取得
        SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
        result.GetComponent<Image>().sprite = imageDrow;
    }

    void Lose()
    {
        playerStatus.HP -= 3;
        AudioClip SE = Resources.Load("damaged") as AudioClip; //強攻撃の効果音を取得
        SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
        result.GetComponent<Image>().sprite = imageLose;
    }
}