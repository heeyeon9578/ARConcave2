
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ConcaveGame : MonoBehaviour //������ �ְ��ϴ� ��ũ��Ʈ
{
    [SerializeField] private List<Button> images; //�ٵ��ǵ�
   

    private int black = 1;
    private int white = 2;

    //���� üũ�ϱ� ���� ���� 
    private int blackCount = 0;
    private int whiteCount = 0;


    private decimal[,] OmokBoard= new decimal[19, 19]; //���� ������

    private bool whatColor = false; //�Ͼ� ��/ ���� �� �������� ����

    private void Start()
    {
        for (int i = 0; i < 361; i++) //InitGame �޼ҵ忡�� ���� �ʴ�, ó������ ������ for��
        {
            int temp = i;
            images[temp].onClick.AddListener(() => clickBtn(images[temp].gameObject));
        }
            InitGame(); //���� ȯ�� �ʱ�ȭ

    }
    /// <summary>
    /// clickBtn�� ��ũ��Ʈ�� ������ ��ư�� ���� ������ �Ķ���Ͱ� Ŭ���� ������Ʈ�� ������ �޾ƿ;� �ϱ� ������
    /// </summary>
    /// <param name="go"></param>
    public void clickBtn(GameObject go)//������� �α�
    {
        Color newColor = go.GetComponent<Image>().color;
        if (newColor.a ==0)
        {
            //���� ���� �̹��� ������Ʈ�� �޾ƿ�
            Image thisImage = go.GetComponent<Image>();
            Color imageColor = thisImage.color;
            //���� ���� x,y ��ǥ �޾ƿ�
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

            imageColor.a = 1f; // ���� -> ������
            thisImage.color = imageColor;
            check();
            PrintOmok();
        }

    }
    public void check() //����, ����, �밢���� ��/ȭ��Ʈ�� 5���� �Ǿ����� üũ
    {
        checkRow();
        checkCol();
        checkDia();
        checkDia2();
    }
    public void checkRow() //���ο� ���̳� ȭ��Ʈ�� 5�� �Ǿ����� üũ
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
                if (OmokBoard[i, j] == black) //��
                {
                    blackCount++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountRow:  " + blackCount);
                    if (blackCount == 5)
                    {
                        UIManager.Instance.gameEnd("=��=�� ���η� 5���� �Ǿ �̰���ϴ�~!! ^3^");
                    }
                    if (j < 18)
                    {
                        if (OmokBoard[i, j + 1] != black)
                        {
                            blackCount = 0;

                        }
                    }

                }
                else if (OmokBoard[i, j] == white) //ȭ��Ʈ
                {

                    whiteCount++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountRow:  " + whiteCount);

                    if (whiteCount == 5)
                    {
                        UIManager.Instance.gameEnd("=ȭ��Ʈ=�� ���η� 5���� �Ǿ �̰���ϴ�~!! ^3^");
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
    public void checkCol()//���ο� ���̳� ȭ��Ʈ�� 5�� �Ǿ����� üũ
    {
        blackCount = 0;
        whiteCount = 0;
        Debug.Log("checkCol");

        for (int j = 0; j < 19; j++)
        {

            for (int i = 0; i < 19; i++)
            {

                if (OmokBoard[i, j] == black)//��
                {
                    blackCount++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountCol:  " + blackCount);
                    if (blackCount == 5)
                    {
                        UIManager.Instance.gameEnd("=��=�� ���η� 5���� �Ǿ �̰���ϴ�~!! >3<");
                    }
                    if (i < 18)
                    {
                        if (OmokBoard[i + 1, j] != black)
                        {
                            blackCount = 0;

                        }
                    }

                }
                else if (OmokBoard[i, j] == white)//ȭ��Ʈ
                {
                    whiteCount++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountCol:   " + whiteCount);


                    if (whiteCount == 5)
                    {
                        UIManager.Instance.gameEnd("=ȭ��Ʈ=�� ���η� 5���� �Ǿ �̰���ϴ�~!! >3<");
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
    public void checkDia()//�밢��(\)�� ���̳� ȭ��Ʈ�� 5�� �Ǿ����� üũ
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
                    if (OmokBoard[r, d] == black)//��
                    {
                        blackCount++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountDia:  " + blackCount);

                    }
                    else if (OmokBoard[r, d] == white)//ȭ��Ʈ
                    {
                        whiteCount++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountDia:  " + whiteCount);

                    }
                    d++;
                }
                if (blackCount == 5)
                {
                    UIManager.Instance.gameEnd("=��=�� �밢��(\\)���� 5���� �Ǿ �̰���ϴ�~!! >0<");
                }
                else if (whiteCount == 5)
                {
                    UIManager.Instance.gameEnd("=ȭ��Ʈ=�� �밢��(\\)���� 5���� �Ǿ �̰���ϴ�~!! >0<");
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
    public void checkDia2()//�밢��(/)�� ���̳� ȭ��Ʈ�� 5�� �Ǿ����� üũ
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
                    if (OmokBoard[r, d] == black)//��
                    {
                        blackCount++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountDia2:  " + blackCount);

                    }
                    else if (OmokBoard[r, d] == white)//ȭ��Ʈ
                    {
                        whiteCount++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountDia2:  " + whiteCount);

                    }
                    d--;
                }
                if (blackCount == 5)
                {
                    UIManager.Instance.gameEnd("=��=�� �밢��(/)���� 5���� �Ǿ �̰���ϴ�~!! ^0^");
                }
                else if (whiteCount == 5)
                {
                    UIManager.Instance.gameEnd("=ȭ��Ʈ=�� �밢��(/)���� 5���� �Ǿ �̰���ϴ�~!! ^0^");
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
    public void findOmok(decimal x, decimal y, int color) //���̳� ȭ��Ʈ�� ����
    {
        
        Debug.Log("findOmok");
        Debug.Log("x:" + x + "y:" + y);
        Debug.Log("color:" + color);
        //��ǥã��
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

    public void InitGame() //���� �ʱ�ȭ (�����)
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