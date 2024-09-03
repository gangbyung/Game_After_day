using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public static TransferMap Instance { get; private set; } // 싱글톤 인스턴스

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
            LoadingSceneManager.LoadScene(transferMapName); //파트 0으로 이동하기
        }

    }

}
