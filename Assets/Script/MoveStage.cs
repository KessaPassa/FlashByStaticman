using UnityEngine;
using System.Collections;

public class MoveStage : MonoBehaviour
{
    public GameObject[] nextPos;
    private int winCounter;


    void Start()
    {
        winCounter = 0;
    }


    void Update()
    {

    }

    //カメラごと動かし、ステージを移動する
    public void NextStage()
    {
        winCounter++;
        iTween.MoveTo(gameObject, iTween.Hash("x", nextPos[winCounter].transform.position.x, "time", 3f));
    }

    public int GetWinCount()
    {
        return winCounter;
    }
}
