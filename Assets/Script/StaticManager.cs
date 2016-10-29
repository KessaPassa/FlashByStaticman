using UnityEngine;
using System.Collections;

public class StaticManager : MonoBehaviour {
	static public int resultScore = 0;  //画面に表示するスコア
    static public int totalScore = 0;   //クリアボーナスなどを加算した最終的なスコア

    public enum DifficultyMode
    {
        Easy,
        Normal,
        Hard
    }
    public DifficultyMode diffycultyMode;

    
    //スコアを加算
    static public void AddScore(int score)
    {
        resultScore += score;
    }
}
