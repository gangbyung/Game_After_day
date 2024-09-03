using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public static TransferMap Instance { get; private set; } // �̱��� �ν��Ͻ�

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
            LoadingSceneManager.LoadScene(transferMapName); //��Ʈ 0���� �̵��ϱ�
        }

    }

}
