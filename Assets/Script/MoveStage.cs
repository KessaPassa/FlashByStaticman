using UnityEngine;
using System.Collections;

public class MoveStage : MonoBehaviour
{
    public GameObject[] nextPos;
    public int winCounter;
    private ResultCtrl resultCtrl;
    private BattleRSP battleRSP;
    public bool isFignhting = true;

    void Awake()
    {
        winCounter = 0;
    }

    void Start()
    {
        resultCtrl = FindObjectOfType<ResultCtrl>();
        battleRSP = FindObjectOfType<BattleRSP>();
    }

    void Update()
    {
        EnemyStatus enemyStatus = resultCtrl.enemyStatus;

        if(transform.position.x >= nextPos[winCounter].transform.position.x)
        {
            if(winCounter != 0)
            {
                isFignhting = false;
                resultCtrl.EndAnim();
            }
        }

        //if(!isFignhting && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)))
        //{
        //    isFignhting = true;  
        //    //battleRSP.StartGame();
        //}
    }

    //カメラごと動かし、ステージを移動する
    public void NextStage()
    {
        winCounter++;
        iTween.MoveTo(gameObject, iTween.Hash("x", nextPos[winCounter].transform.position.x, "time", 3f));
        resultCtrl.anim.SetTrigger("MoveScene");
        battleRSP.EndGame();
    }
}
