using UnityEngine;
using System.Collections;

public class MoveStage : MonoBehaviour
{
    public GameObject player;
    private Animator anim;
    public int winCounter;
    private ResultCtrl resultCtrl;
    //public bool isFignhting = true;
    private bool isFrying = false;


    void Awake()
    {
        winCounter = 0;
    }

    void Start()
    {
        anim = player.GetComponent<Animator>();
        resultCtrl = FindObjectOfType<ResultCtrl>();
    }

    void Update()
    {
        //playerは固定のため、背景の方を調整する
        if (isFrying && player.transform.position.x >= InitGenerator.nextPos[winCounter].transform.position.x - 3.5f)
        {
            if(winCounter != 0)
            {
                //isFignhting = false;
                isFrying = false;
                resultCtrl.EndAnim();
                player.GetComponent<PlayerAnim>().EndAnim();
            }
        }
    }

    ////カメラごと動かし、ステージを移動する
    //public void NextStage(ResultCtrl resultCtrl, BattleRSP battleRSP)
    //{
    //    winCounter++;
    //    float next = background.position.x + nextPos[winCounter].position.x;
    //    iTween.MoveTo(gameObject, iTween.Hash("x", next, "time", 3f));
    //    resultCtrl.anim.SetTrigger("MoveScene");
    //    battleRSP.EndGame();
    //}

    //背景と敵を動かす
    //引数でとらないと、何故かResultCtrlとBattleRSPがNullReferenceExceptionになる
    public void NextStage(ResultCtrl resultCtrl, BattleRSP battleRSP)
    {
        anim.enabled = true;
        anim.SetTrigger("Frying");
        winCounter++;
        float next = InitGenerator.nextPos[winCounter].transform.position.x - transform.position.x;
        iTween.MoveTo(gameObject, iTween.Hash("x", next * -1f, "time", 1f));
        resultCtrl.anim.SetTrigger("MoveScene");
        isFrying = true;
        
        battleRSP.EndGame();
    }
}
