using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    
    public string transferMapName;

    private Player thePlayer;

    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }
    void OnTriggerEnter2D(Collider2D collision)//충돌되었을 때
    {
        if (collision.gameObject.name == "Player") //오브젝트의 이름이 플레이어라면
        {
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName); //파트 0으로 이동하기
        }

    }
    public void Go_0_Start()
    {
        SceneManager.LoadScene("0.Start");
    }
    public void Go_1_part0()
    {
        SceneManager.LoadScene("1.part0");
    }
    public void Go_2_part1()
    {
        SceneManager.LoadScene("2.part1");
    }
    public void Go_3_Endpart0()
    {
        SceneManager.LoadScene("3.Endpart0");
    }
    public void Go_4_part3()
    {
        SceneManager.LoadScene("4.part3");
    }
    public void Go_99_EndGame()
    {
        SceneManager.LoadScene("99.EndGame");
    }
}
