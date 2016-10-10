using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public int maxHP = 50;          //最大体力
    public int HP;                  //体力
    public Slider HPber;            //体力ゲージ
    public int strong = 5;          //強攻撃
    public int normal = 2;          //中攻撃
    public int weak = 1;            //弱攻撃
    public float lerpTime = 1;      //値が大きいほど早くなる
    private ResultCtrl resultCtrl;
    private bool isDied = false;    //死んだかどうか

    public enum LerpMode
    {
        normal,
        sin
    }
    public LerpMode lerpMode = LerpMode.normal;


    void Start()
    {
        HP = maxHP;                     //体力を初期化
        HPber.maxValue = maxHP;         //スライダーの最大値を最大体力に合わせる
        HPber.value = HPber.maxValue;   //最大値を変化させた分、初期valueも合わせる
        resultCtrl = FindObjectOfType<ResultCtrl>();
    }


    void Update()
    {
        HPber.value = HP; //受けたダメージをスライダーに反映させる
        if(HP <= 0 && !isDied)
        {
            resultCtrl.EnemyDead();
            isDied = true;
        }
    }
}
