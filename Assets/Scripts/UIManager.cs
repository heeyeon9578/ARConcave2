using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private static UIManager instance;       
    [SerializeField] private Sprite white;
    [SerializeField] private Sprite black;
    [SerializeField] private Image image;
    [SerializeField] private GameObject gameEndPanel; //게임 엔딩에 쓰이는 패널

    private int blackColor = 1;
    private int whiteColor = 2;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }
    public static UIManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void setColor(int color)
    {
        if(color == blackColor)
        {
            image.sprite = black;

        }
        else
        {
            image.sprite = white;
        }

    }

    public void gameEnd(string str)//게임 오버 (누군가가 승리하였을 경우)
    {

        gameEndPanel.GetComponentInChildren<Text>().text = str;
        gameEndPanel.SetActive(true);
    }
}
