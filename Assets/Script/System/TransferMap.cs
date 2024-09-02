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
    void OnTriggerEnter2D(Collider2D collision)//�浹�Ǿ��� ��
    {
        if (collision.gameObject.name == "Player") //������Ʈ�� �̸��� �÷��̾���
        {
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName); //��Ʈ 0���� �̵��ϱ�
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
