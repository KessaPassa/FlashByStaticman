using UnityEngine;
using System.Collections;

public class StaticManager : MonoBehaviour {
	static public int resultScore = 0;  //画面に表示するスコア
    static public int totalScore = 0;   //クリアボーナスなどを加算した最終的なスコア
    public int clearBonus;

    public enum DifficultyMode
    {
        Easy,
        Normal,
        Hard
    }
    public DifficultyMode diffycultyMode;


    void Start()
    {
        int coefficient = 1;
        switch (diffycultyMode)
        {
            case DifficultyMode.Easy:
                coefficient = 1;
                break;

            case DifficultyMode.Normal:
                coefficient = 2;
                break;

            case DifficultyMode.Hard:
                coefficient = 3;
                break;
        }
        clearBonus *= coefficient;
    }

    void Update()
    {
        totalScore = resultScore + clearBonus;
    }
    
    //スコアを加算
    static public void AddScore(int score)
    {
        resultScore += score;
    }

    static public int GetResultSocre()
    {
        return resultScore;
    }

    static public int GetTotalScore()
    {
        return totalScore;
    }
}
