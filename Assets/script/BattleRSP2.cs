using UnityEngine;
using System.Collections;

public class BattleRSP2 : MonoBehaviour
{
    public Sprite rock;                                     //グー
    public Sprite scissors;                                 //チョキ
    public Sprite paper;                                    //パー
    public GameObject[] originHand;                         //インスタンスの元となるオブジェクト
    private GameObject[] appearHand = new GameObject[5];    //画面上に出ている手
    public PlayerStatus playerStatus;                       //プレーヤーのステータス
    public EnemyStatus enemyStatus;                         //敵のステータス
    public AudioSource SEBox;                               //PlayOneShot用の空箱
    private bool isHiddenOn = false;                        //手を隠しているか否か


    void Start()
    {
        for (int i = 0; i < originHand.Length; i++)
        {
            appearHand[i] = Instantiate(originHand[i], originHand[i].transform.position, Quaternion.identity) as GameObject; //インスタンス生成
            appearHand[i].GetComponent<SpriteRenderer>().sprite = GetNextHand(); //手の絵を入れる
            originHand[i].GetComponent<Renderer>().enabled = false; //見えなくする
        }
    }


    void Update()
    {
        string enemyHand = appearHand[0].GetComponent<SpriteRenderer>().sprite.name; //現在の手の名前を取得

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
        else if (Input.GetKeyDown(KeyCode.S))
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
        else if (Input.GetKeyDown(KeyCode.D))
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

        //スペースを押したらHiddenを実行する(プロトタイプ)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isHiddenOn = !isHiddenOn; //ボタン1つでオン・オフ
        }
        //trueなら1つ目以外を隠す
        if (isHiddenOn)
        {
            appearHand[0].GetComponent<Renderer>().enabled = true;
            for (int i = 1; i < appearHand.Length; i++)
            {
                appearHand[i].GetComponent<Renderer>().enabled = false;
            }
        }
        //falseなら通常通り、3つ表示する
        else
        {
            for (int i = 0; i < appearHand.Length; i++)
            {
                appearHand[i].GetComponent<Renderer>().enabled = true;
            }
        }
    }

    //手前の手を消し、1つずつずらし、最後尾に手を追加する
    void MoveHand()
    {
        Destroy(appearHand[0]); //手前の手を削除

        //手前の手には配列の1か3を入れる
        while (true)
        {
            int random = Random.Range(1, 4);
            if (random == 1 || random == 3)
            {
                appearHand[0] = appearHand[random];
                if(random == 1)
                {
                    Destroy(appearHand[3]);
                }
                else
                {
                    Destroy(appearHand[1]);
                }
                break;
            }
        }

        //1つずつ前にずらす
        for (int i = 1; i < appearHand.Length; i++)
        {
            if (i == 2 || i == 4)
            {
                continue;
            }
            appearHand[i] = appearHand[i + 1];
        }

        //ずらして空になった[2]と[4]にインスタンスを生成
        appearHand[2] = Instantiate(originHand[2], originHand[2].transform.position, Quaternion.identity) as GameObject;
        appearHand[2].GetComponent<SpriteRenderer>().sprite = GetNextHand();
        appearHand[4] = Instantiate(originHand[4], originHand[2].transform.position, Quaternion.identity) as GameObject;
        appearHand[4].GetComponent<SpriteRenderer>().sprite = GetNextHand();

        //位置を1つずつずら
        for (int i = 0; i < appearHand.Length; i++)
        {
            appearHand[i].transform.position = originHand[i].transform.position;
        }
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
