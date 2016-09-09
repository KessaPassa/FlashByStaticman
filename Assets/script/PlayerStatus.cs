﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public int HP = 50;     //体力
    public Slider HPber;    //体力ゲージ
    public int strong = 5;  //強攻撃
    public int normal = 2;  //中攻撃
    public int weak = 1;    //弱攻撃


    void Start()
    {
        HPber.maxValue = HP;            //スライダーの最大値を最大体力に合わせる
        HPber.value = HPber.maxValue;   //最大値を変化させた分、初期valueも合わせる
    }


    void Update()
    {
        HPber.value = HP; //受けたダメージをスライダーに反映させる
    }
}
