﻿using UnityEngine;
using System.Collections;

public class RoopGenerator : MonoBehaviour
{
    public GameObject originBg;
    public int bgLength;

    public GameObject originNextPos;
    static public GameObject[] nextPos;
    public int nextPosLength;

    public float defaultOffset = 3f;

    void Start()
    {
        for(int i=0; i < bgLength; i++)
        {
            var obj = Instantiate(
                originBg, 
                new Vector2(originBg.transform.position.x + i * 32f, originBg.transform.position.y), 
                Quaternion.identity) as GameObject;

                obj.transform.parent = this.transform;
        }

        nextPos = new GameObject[nextPosLength];
        for(int i=0; i < nextPosLength; i++)
        {
            nextPos[i] = Instantiate(
                originNextPos, 
                new Vector2(originNextPos.transform.position.x + i * 25 + defaultOffset, originNextPos.transform.position.y),
                Quaternion.identity) as GameObject;

            //nextPos[i].transform.parent.SetParent(originNextPos.transform);
            nextPos[i].name = "nextPos" + i;
            nextPos[i].transform.parent = this.transform;
        }
    }

    static public Vector2 InitPos(int startIndex)
    {
        //エラーまたは設定していないなら
        if (startIndex == -1)
        {
            return new Vector2(0f, -15f);
        }
        else
        {
            return nextPos[startIndex].transform.position;
        }
    }
}
