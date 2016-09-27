using UnityEngine;
using System.Collections;

public class LerpMode : MonoBehaviour {
    BattleRSP battleRSP;

	// Use this for initialization
	void Start () {
        battleRSP = FindObjectOfType<BattleRSP>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void NormalLerp(float rate)
    {
        //線形補間させ、滑らかに動かす
        for (int i = 0; i < battleRSP.appearHand.Length - 1; i++)
        {
            //x軸とy軸どちらにも対応
            if (battleRSP.originHand[i].transform.position.x < battleRSP.appearHand[i].transform.position.x ||
                battleRSP.originHand[i].transform.position.y < battleRSP.appearHand[i].transform.position.y)
            {
                //線形補間
                battleRSP.appearHand[i].transform.position = Vector3.Lerp(
                    battleRSP.originHand[i + 1].transform.position,
                    battleRSP.originHand[i].transform.position,
                    rate
                    );
            }
        }
    }

    public void SinLerp(float rate)
    {
        //x軸とy軸どちらにも対応
        if (battleRSP.originHand[0].transform.position.x < battleRSP.appearHand[0].transform.position.x)
        {
            float width = 150f;
            float pingpong = Mathf.PingPong(Time.time * 500f, width) - width / 2f;

            //線形補間
            battleRSP.appearHand[0].transform.position = Vector2.Lerp(
                battleRSP.originHand[1].transform.position,
                new Vector2(battleRSP.originHand[0].transform.position.x, battleRSP.originHand[0].transform.position.y + pingpong),
                rate
                );
        }
    }

    public void SwayRot(float rate)
    {
        float wave_angel = 2 * Mathf.PI * Mathf.Repeat(rate, 1);    //ラジアン0～6.18がぐるぐる回る
        float wave_offset = Mathf.Sin(wave_angel);                    //結果的に-1～0～1とサイン波でぐるぐる行来きする
        Vector3 newPos = battleRSP.appearHand[0].transform.position;
        newPos.z = battleRSP.appearHand[0].transform.position.z + wave_offset;
        battleRSP.appearHand[0].transform.position = newPos;
        battleRSP.appearHand[0].transform.rotation = Quaternion.AngleAxis(30.0f * Mathf.Sin(wave_angel), Vector3.up);
    }
}
