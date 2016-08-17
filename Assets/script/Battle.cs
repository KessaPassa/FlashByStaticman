using UnityEngine;
using System.Collections;

public class Battle : MonoBehaviour
{
    public PlayerStatus playerStatus;   //プレーヤーのステータス
    public EnemyStatus enemyStatus;     //敵のステータス
    public AudioSource SEBox;           //PlayOneShot用の空箱
    private float waitTime;             //ノックバック時間


    void Start()
    {
        waitTime = 0f; //一番最初は待機時間なし
    }


    void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime; //タイマー
        }
        else
        {
            //強攻撃
            if (Input.GetKeyDown(KeyCode.A))
            {
                waitTime = 1f;  //ノックバック時間
                enemyStatus.HP -= playerStatus.strong; //強ダメージ
                AudioClip SE = Resources.Load("strong") as AudioClip; //強攻撃の効果音を取得
                SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
            }

            //中攻撃
            if (Input.GetKeyDown(KeyCode.S))
            {
                waitTime = 0.4f; //ノックバック時間
                enemyStatus.HP -= playerStatus.normal; //中ダメージ
                AudioClip SE = Resources.Load("normal") as AudioClip; //中攻撃の効果音を取得
                SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
            }

            //弱攻撃
            if (Input.GetKeyDown(KeyCode.D))
            {
                waitTime = 0.2f; //ノックバック時間
                enemyStatus.HP -= playerStatus.weak; //弱ダメージ
                AudioClip SE = Resources.Load("weak") as AudioClip; //弱攻撃の効果音を取得
                SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
            }
        }
    }
}
