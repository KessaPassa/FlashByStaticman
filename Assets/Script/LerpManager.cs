using UnityEngine;
using System.Collections;

public class LerpManager : MonoBehaviour {
    private BattleRSP battleRSP;
    private ResultCtrl resultCtrl;
    private float diff;
    private float lerpTime;
    

	void Start () {
        battleRSP = FindObjectOfType<BattleRSP>();
        resultCtrl = FindObjectOfType<ResultCtrl>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SelectLerp(EnemyStatus enemyStatus, float diff)
    {
        this.diff = diff;
        this.lerpTime = enemyStatus.lerpTime;

        if (enemyStatus.lerpMode == EnemyStatus.LerpMode.Normal)
        {
            NormalLerp();
        }
        else if(enemyStatus.lerpMode == EnemyStatus.LerpMode.Sin)
        {
            SinLerp();
        }
    }

    public void NormalLerp()
    {
        //線形補間
        battleRSP.appearHand[0].transform.position = Vector2.Lerp(
            battleRSP.originHand[0].transform.position,
            resultCtrl.deadLine.transform.position,
            diff * lerpTime
            );
    }

    public void SinLerp()
    {
        float width = 150f;
        float pingpong = Mathf.PingPong(Time.time * 500f, width) - width / 2f;

        //線形補間
        battleRSP.appearHand[0].transform.position = Vector2.Lerp(
            battleRSP.originHand[0].transform.position,
            new Vector2(resultCtrl.deadLine.transform.position.x, resultCtrl.deadLine.transform.position.y + pingpong),
            diff * lerpTime
            );
    }

    public void SwayRot()
    {
        float wave_angel = 2 * Mathf.PI * Mathf.Repeat(diff, 1);    //ラジアン0～6.18がぐるぐる回る
        float wave_offset = Mathf.Sin(wave_angel);                    //結果的に-1～0～1とサイン波でぐるぐる行来きする
        Vector3 newPos = battleRSP.appearHand[0].transform.position;
        newPos.z = battleRSP.appearHand[0].transform.position.z + wave_offset;
        battleRSP.appearHand[0].transform.position = newPos;
        battleRSP.appearHand[0].transform.rotation = Quaternion.AngleAxis(30.0f * Mathf.Sin(wave_angel), Vector3.up);
    }
}
