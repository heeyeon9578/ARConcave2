using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmokStone : MonoBehaviour //오목돌의 정보
{
    [SerializeField] private RectTransform rectTrans;
    [SerializeField] public decimal x;
    [SerializeField] public decimal y;

    private void Start()          
    {
       rectTrans = GetComponent<RectTransform>();
       x = (decimal)Math.Round(rectTrans.localPosition.x, 2);
       y= (decimal)Math.Round(rectTrans.localPosition.y,2) ;
      

    }
}
