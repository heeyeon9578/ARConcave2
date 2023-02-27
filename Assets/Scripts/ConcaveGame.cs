
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ConcaveGame : MonoBehaviour //게임을 주관하는 스크립트
{
    [SerializeField] private List<Button> images; //바둑판들
   

    private int black = 1;
    private int white = 2;

    //개수 체크하기 위한 변수 
    private int blackCount = 0;
    private int whiteCount = 0;


    private decimal[,] OmokBoard= new decimal[19, 19]; //실제 오목판

    private bool whatColor = false; //하얀 돌/ 검은 돌 순서인지 결정

    private void Start()
    {
        for (int i = 0; i < 361; i++) //InitGame 메소드에는 들어가지 않는, 처음에만 실행할 for문
        {
            int temp = i;
            images[temp].onClick.AddListener(() => clickBtn(images[temp].gameObject));
        }
            InitGame(); //게임 환경 초기화

    }
    /// <summary>
    /// clickBtn을 스크립트로 각각의 버튼에 붙힌 이유는 파라미터가 클릭한 오브젝트의 정보를 받아와야 하기 때문에
    /// </summary>
    /// <param name="go"></param>
    public void clickBtn(GameObject go)//오목알을 두기
    {
        Color newColor = go.GetComponent<Image>().color;
        if (newColor.a ==0)
        {
            //누른 곳의 이미지 컴포넌트를 받아옴
            Image thisImage = go.GetComponent<Image>();
            Color imageColor = thisImage.color;
            //누른 곳의 x,y 좌표 받아옴
            OmokStone omok = go.GetComponent<OmokStone>();

            decimal x = omok.x;
            decimal y = omok.y;

            Debug.Log(x + "," + y);
            if (whatColor == false)//black
            {
                findOmok(x, y, black); //black
                whatColor = true;
                UIManager.Instance.setColor(white);
                Color blackColor = new Color(0, 0, 0);
                imageColor= blackColor; 
            }
            else// white
            {
                findOmok(x, y, white); // white
                whatColor = false;
                UIManager.Instance.setColor(black);

            }

            imageColor.a = 1f; // 투명 -> 불투명
            thisImage.color = imageColor;
            check();
            PrintOmok();
        }

    }
    public void check() //가로, 세로, 대각선에 블랙/화이트가 5개가 되었는지 체크
    {
        checkRow();
        checkCol();
        checkDia();
        checkDia2();
    }
    public void checkRow() //가로에 블랙이나 화이트가 5개 되었는지 체크
    {
        blackCount = 0;
        whiteCount = 0;
        Debug.Log("checkRow");

        for (int i = 0; i < 19; i++)
        {
            blackCount = 0;
            whiteCount = 0;
            for (int j = 0; j < 19; j++)
            {
                if (OmokBoard[i, j] == black) //블랙
                {
                    blackCount++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountRow:  " + blackCount);
                    if (blackCount == 5)
                    {
                        UIManager.Instance.gameEnd("=블랙=이 가로로 5개가 되어서 이겼습니다~!! ^3^");
                    }
                    if (j < 18)
                    {
                        if (OmokBoard[i, j + 1] != black)
                        {
                            blackCount = 0;

                        }
                    }

                }
                else if (OmokBoard[i, j] == white) //화이트
                {

                    whiteCount++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountRow:  " + whiteCount);

                    if (whiteCount == 5)
                    {
                        UIManager.Instance.gameEnd("=화이트=가 가로로 5개가 되어서 이겼습니다~!! ^3^");
                    }
                    if (j < 18)
                    {
                        if (OmokBoard[i, j + 1] != white)
                        {
                            whiteCount = 0;
                        }
                    }

                }
            }
        }
    }
    public void checkCol()//세로에 블랙이나 화이트가 5개 되었는지 체크
    {
        blackCount = 0;
        whiteCount = 0;
        Debug.Log("checkCol");

        for (int j = 0; j < 19; j++)
        {

            for (int i = 0; i < 19; i++)
            {

                if (OmokBoard[i, j] == black)//블랙
                {
                    blackCount++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountCol:  " + blackCount);
                    if (blackCount == 5)
                    {
                        UIManager.Instance.gameEnd("=블랙=이 세로로 5개가 되어서 이겼습니다~!! >3<");
                    }
                    if (i < 18)
                    {
                        if (OmokBoard[i + 1, j] != black)
                        {
                            blackCount = 0;

                        }
                    }

                }
                else if (OmokBoard[i, j] == white)//화이트
                {
                    whiteCount++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountCol:   " + whiteCount);


                    if (whiteCount == 5)
                    {
                        UIManager.Instance.gameEnd("=화이트=가 세로로 5개가 되어서 이겼습니다~!! >3<");
                    }
                    if (i < 18)
                    {
                        if (OmokBoard[i + 1, j] != white)
                        {
                            whiteCount = 0;

                        }
                    }

                }
            }
            blackCount = 0;
            whiteCount = 0;
        }

    }
    public void checkDia()//대각선(\)에 블랙이나 화이트가 5개 되었는지 체크
    {
        blackCount = 0;
        whiteCount = 0;
        Debug.Log("checkDia");
        int temp = 0;

        for (int i = 0; i < 15; i++)
        {

            for (int j = 0; j < 15; j++)
            {
                int d = j;
                for (int r = temp; r < temp + 5; r++)
                {
                    if (OmokBoard[r, d] == black)//블랙
                    {
                        blackCount++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountDia:  " + blackCount);

                    }
                    else if (OmokBoard[r, d] == white)//화이트
                    {
                        whiteCount++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountDia:  " + whiteCount);

                    }
                    d++;
                }
                if (blackCount == 5)
                {
                    UIManager.Instance.gameEnd("=블랙=이 대각선(\\)으로 5개가 되어서 이겼습니다~!! >0<");
                }
                else if (whiteCount == 5)
                {
                    UIManager.Instance.gameEnd("=화이트=가 대각선(\\)으로 5개가 되어서 이겼습니다~!! >0<");
                }
                else
                {
                    blackCount = 0;
                    whiteCount = 0;
                }

            }
            temp++;
        }
    }
    public void checkDia2()//대각선(/)에 블랙이나 화이트가 5개 되었는지 체크
    {
        blackCount = 0;
        whiteCount = 0;
        Debug.Log("checkDia2");
        int temp = 0;

        for (int i = 0; i < 15; i++)
        {

            for (int j = 4; j < 19; j++)
            {
                int d = j;
                for (int r = temp; r < temp + 5; r++)
                {
                    if (OmokBoard[r, d] == black)//블랙
                    {
                        blackCount++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountDia2:  " + blackCount);

                    }
                    else if (OmokBoard[r, d] == white)//화이트
                    {
                        whiteCount++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountDia2:  " + whiteCount);

                    }
                    d--;
                }
                if (blackCount == 5)
                {
                    UIManager.Instance.gameEnd("=블랙=이 대각선(/)으로 5개가 되어서 이겼습니다~!! ^0^");
                }
                else if (whiteCount == 5)
                {
                    UIManager.Instance.gameEnd("=화이트=가 대각선(/)으로 5개가 되어서 이겼습니다~!! ^0^");
                }
                else
                {
                    blackCount = 0;
                    whiteCount = 0;
                }

            }
            temp++;
        }
    }
    public void findOmok(decimal x, decimal y, int color) //블랙이나 화이트로 설정
    {
        
        Debug.Log("findOmok");
        Debug.Log("x:" + x + "y:" + y);
        Debug.Log("color:" + color);
        //좌표찾기
        for (int i = 0; i < 19; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                Debug.Log("x: " + i + ", y: " + j + "= " + OmoksContainer.OmokBoardXs[i, j] + "," + OmoksContainer.OmokBoardYs[i, j]);

                if (OmoksContainer.OmokBoardXs[i, j].Equals(x) && OmoksContainer.OmokBoardYs[i, j].Equals( y))
                {
                    OmokBoard[i, j] = color;
                    Debug.Log(i + ", " + j);
                    return;
                }
            }
        }
    }

    public void InitGame() //게임 초기화 (재시작)
    {

        for (int i = 0; i < images.Count; i++)
        {
            int temp = i;
            Image thisImage = images[temp].GetComponent<Image>();
            Color imageColor = thisImage.color;
            imageColor = Color.white;
            imageColor.a = 0f;
            thisImage.color = imageColor;

        }

        for (int i = 0; i < 19; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                OmokBoard[i, j] =0;

            }
        }
 
    }
    public void PrintOmok()
    {
        string str="";
        for (int i = 0; i < 19; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                str += OmokBoard[i, j]+ " ";
               
            }
            str+= "\n"; 
           
        }
        Debug.Log(str);
    }
}