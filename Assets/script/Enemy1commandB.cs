using UnityEngine;
using System.Collections;

public class Enemy1commandB : MonoBehaviour {
	public AudioSource SEBox;  //PlayOneShot用の空箱
	public PlayerStatus PLst; //オブジェクト側のパブリックでプレイヤーオブジェクトを選択
	EnemyStatus ENst;
	public float PwaitTime = 3.0f; //強攻撃待機時間
	public float NwaitTime = 1.0f; //中攻撃待機時間　反撃、防御に使い回し
	public float WwaitTime = 0.5f; //弱攻撃待機時間
	float wt; //処理に使用する待機時間
	public float GuardTime = 0.2f; //防御の時間
	bool GuardNow = false; //防御いているか

	enum State{
		Wait, //待機状態、何もしない。
		Guard, //防御状態、披ダメージ減少。
		AttackP, //強攻撃状態、パワー、大ダメージスローモーション。
		AttackN, //中攻撃状態、ノーマル、特色無し。
		AttackS, //弱攻撃状態、スピード、小ダメージ確率連続攻撃高速モーション。
		AttackC, //反撃攻撃状態、当身？極小時間に大ダメージ反撃。
	};
	State state = State.Wait;

	// Use this for initialization
	void Start () {
		wt = 0.1f;
		ENst = GetComponent<EnemyStatus> ();
	}

	// Update is called once per frame
	void Update () {
		switch (state) {
		case State.Wait:
			TimeElapse ();
			break;
		case State.Guard:
			Guard ();
			break;
		case State.AttackP:
			AttackP ();
			break;
		}

	}

	void Guard(){
		//防御を行う
		//現在は飾り
		if (wt >= NwaitTime - GuardTime && GuardNow == false) {
			GuardNow = true;
		} else if (wt < NwaitTime - GuardTime && GuardNow == true) {
			GuardNow = false;
		}
		TimeElapse (); //時間経過
	}

	void AttackP(){
		//強攻撃、スキ多し、パワー大、無効までの時間長
		WaitStart(PwaitTime);
		PLst.HP -= ENst.strong;
		AudioClip SE = Resources.Load("strong") as AudioClip; //強攻撃の効果音を取得
		SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
	}

	void AttackN(){
		//通常攻撃、特筆なし
		WaitStart(NwaitTime);
		PLst.HP -= ENst.normal;
		AudioClip SE = Resources.Load("nomal") as AudioClip; //強攻撃の効果音を取得
		SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
	}

	void AttackS(){
		//弱攻撃、高速、パワー小、確率連続攻撃
		WaitStart(PwaitTime);
		PLst.HP -= ENst.weak;
		AudioClip SE = Resources.Load("weak") as AudioClip; //強攻撃の効果音を取得
		SEBox.PlayOneShot(SE, 3f); //効果音を鳴らす
	}

	void AttackC(){
		//いわゆる当身、一定時間攻撃を受けると無効にして反撃

	}

	void TimeElapse(){
		//待機時間を経過させる
		Debug.Log(1);
		wt -= Time.deltaTime;
		if (wt <= 0.0f) {
			Debug.Log (2);
			randomState ();
		}
	}

	void WaitStart(float beginWaitTime){
		//待機状態になる
		state = State.Wait;
		wt = beginWaitTime;
	}

	void randomState(){
		//Stateをランダムに決定する
		state = State.AttackP;
	}

}
