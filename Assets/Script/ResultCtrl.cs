using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultCtrl : MonoBehaviour
{
    private PlayerStatus playerStatus;  //プレーヤーのステータス
    private EnemyStatus enemyStatus;    //敵のステータス
    BattleRSP battleRSP;
    private AudioSource SEBox;          //PlayOneShot用の空箱
    private Animator anim;              //Animation用の空箱
    public Image result;                //じゃんけん判定を表示
    public Sprite imageWin;             //勝利画像
    public Sprite imageDrow;            //引き分け画像
    public Sprite imageLose;            //負け画像
    public Sprite imageCongra;


    void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        enemyStatus = FindObjectOfType<EnemyStatus>();
        battleRSP = FindObjectOfType<BattleRSP>();
        SEBox = GameObject.Find("SEBox").GetComponent<AudioSource>();
        anim = GameObject.Find("AnimCtrl").GetComponent<Animator>();
    }

    
    void Update()
    {
        //プレイヤー死亡時
        if(playerStatus.HP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        //敵撃破時
        else if(enemyStatus.HP <= 0)
        {
            battleRSP.enabled = false;
            result.GetComponent<Image>().sprite = imageCongra;
        }
    }

    //アニメーションを停止させる
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
    }
}
