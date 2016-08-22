using UnityEngine;
using System.Collections;

public class BattleRSP : MonoBehaviour {
    public Sprite rock;                 //グー
    public Sprite scissors;             //チョキ
    public Sprite paper;                //パー
    public GameObject originHand1;      //位置情報獲得のためのインスタンス元その1
    public GameObject originHand2;      //位置情報獲得のためのインスタンス元その2
    public GameObject originHand3;      //位置情報獲得のためのインスタンス元その3
    private GameObject firstHand;       //手前の手
    private GameObject secondHand;      //真ん中の手
    private GameObject thirdHand;       //最後尾の手
    public PlayerStatus playerStatus;   //プレーヤーのステータス
    public EnemyStatus enemyStatus;     //敵のステータス
    public AudioSource SEBox;           //PlayOneShot用の空箱


    enum RSP
    {
        None = -1,
        Rock,
        Scissors,
        Paper,
    };


    void Start () {
        firstHand = Instantiate(originHand1, originHand1.transform.position, Quaternion.identity) as GameObject; //インスタンス生成
        firstHand.GetComponent<SpriteRenderer>().sprite = GetNextHand(); //手の絵を入れる
        originHand1.GetComponent<Renderer>().enabled = false; //見えなくする    

        secondHand = Instantiate(originHand2, originHand2.transform.position, Quaternion.identity) as GameObject;
        secondHand.GetComponent<SpriteRenderer>().sprite = GetNextHand();
        originHand2.GetComponent<Renderer>().enabled = false;

        thirdHand = Instantiate(originHand3, originHand3.transform.position, Quaternion.identity) as GameObject;
        thirdHand.GetComponent<SpriteRenderer>().sprite = GetNextHand();
        originHand3.GetComponent<Renderer>().enabled = false;
    }


    void Update()
    {
        string enemyHand = firstHand.GetComponent<SpriteRenderer>().sprite.name; //firstHandの現在の手の名前を取得

        //グー
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (enemyHand == "Rock")
            {
                Drow();
            }
            else if (enemyHand == "Scissors")
            {
                Win();
            }
            else if (enemyHand == "Paper")
            {
                Lose();
            }

            MoveHand();
        }

        //チョキ
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (enemyHand == "Rock")
            {
                Lose();
            }
            else if (enemyHand == "Scissors")
            {
                Drow();
            }
            else if (enemyHand == "Paper")
            {
                Win();
            }

            MoveHand();
        }

        //パー
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (enemyHand == "Rock")
            {
                Win();
            }
            else if (enemyHand == "Scissors")
            {
                Lose();
            }
            else if (enemyHand == "Paper")
            {
                Drow();
            }

            MoveHand();
        }
    }

    //firstHandを消し、1つずつずらし、thirdHandに手を追加する
    void MoveHand()
    {
        //手前の手を削除し、1つずつ前にずらす
        Destroy(firstHand);
        firstHand = secondHand;
        secondHand = thirdHand;

        //位置を1つずつずらす
        firstHand.transform.position = originHand1.transform.position;
        secondHand.transform.position = originHand2.transform.position;

        //ずらして空になったthirdHandにインスタンスを生成
        thirdHand = Instantiate(originHand3, originHand3.transform.position, Quaternion.identity) as GameObject;
        thirdHand.GetComponent<SpriteRenderer>().sprite = GetNextHand();
        thirdHand.GetComponent<Renderer>().enabled = true;
    }

    //ランダムでじゃんけんの手を決める
    Sprite GetNextHand()
    {
        Sprite hand = null;
        int random = Random.Range(0, 3);
        switch (random)
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

    void Win()
    {
        enemyStatus.HP -= 3;
        AudioClip SE = Resources.Load("strong") as AudioClip; //強攻撃の効果音を取得
        SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
    }

    void Drow()
    {
        playerStatus.HP -= 1;
        enemyStatus.HP -= 1;
        AudioClip SE = Resources.Load("normal") as AudioClip; //強攻撃の効果音を取得
        SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
    }

    void Lose()
    {
        playerStatus.HP -= 3;
        AudioClip SE = Resources.Load("damaged") as AudioClip; //強攻撃の効果音を取得
        SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
    }
}
