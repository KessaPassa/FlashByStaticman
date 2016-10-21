using UnityEngine;
using System.Collections;

public class MoveStage : MonoBehaviour
{
    public Transform player;
    public int winCounter;
    private ResultCtrl resultCtrl;
    public bool isFignhting = true;


    void Awake()
    {
        winCounter = 0;
    }

    void Start()
    {
        resultCtrl = FindObjectOfType<ResultCtrl>();
    }

    void Update()
    {
        //playerは固定のため、背景の方を調整する
        if (player.position.x >= InitGenerator.nextPos[winCounter].transform.position.x - 3.5f)
        {
            if(winCounter != 0)
            {
                isFignhting = false;
                resultCtrl.EndAnim();
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
        winCounter++;
        float next = InitGenerator.nextPos[winCounter].transform.position.x - transform.position.x;
        iTween.MoveTo(gameObject, iTween.Hash("x", next * -1f, "time", 1f));
        resultCtrl.anim.SetTrigger("MoveScene");
        battleRSP.EndGame();
    }
}
