using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmoksContainer : MonoBehaviour // 오목판의 돌 위치 좌표계 보유
{

    public static decimal[,] OmokBoardXs = new decimal[19, 19];
    public static decimal[,] OmokBoardYs = new decimal[19, 19];

    decimal firstX = (decimal)-4.68;
    decimal firstY = (decimal)4.68;

    decimal nextX = (decimal)0.52;
    decimal nextY = (decimal)-0.52;
    private void Awake()
    {

        for (int i = 0; i < 19; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                OmokBoardXs[i, j] = firstX + (nextX * j);
                OmokBoardYs[i, j] = firstY;
            }
            firstY += nextY;
        }

    }
}
