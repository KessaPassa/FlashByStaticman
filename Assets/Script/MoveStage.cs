using UnityEngine;
using System.Collections;

public class MoveStage : MonoBehaviour
{
    public GameObject[] nextPos;
    private int winCounter;
    private ResultCtrl resultCtrl;


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
        if(transform.position.x >= nextPos[winCounter].transform.position.x)
        {
            if(winCounter != 0)
            {
                resultCtrl.EndAnim();
            }
        }
    }

    //カメラごと動かし、ステージを移動する
    public void NextStage()
    {
        winCounter++;
        iTween.MoveTo(gameObject, iTween.Hash("x", nextPos[winCounter].transform.position.x, "time", 3f));
        resultCtrl.anim.SetTrigger("MoveScene");
    }

    public int GetWinCount()
    {
        return winCounter;
    }
}
