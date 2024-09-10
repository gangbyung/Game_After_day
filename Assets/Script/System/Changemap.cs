using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changemap : MonoBehaviour
{
    private static Vector3 playerTargetPosition; // 플레이어의 목표 위치

    private void Awake()
    {
        // 씬이 로드된 후 호출되는 이벤트에 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // 씬 로드 이벤트에서 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "4.part3")
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = playerTargetPosition;
            }
        }
    }

    public static void Go_0_Start() => LoadingSceneManager.LoadScene("0.Start");
    public static void Go_1_part0() => LoadingSceneManager.LoadScene("1.part0");
    public static void Go_2_part1() => LoadingSceneManager.LoadScene("2.part1");
    public static void Go_3_Endpart0() => LoadingSceneManager.LoadScene("3.Endpart0");

    public static void Go_4_part3()
    {
        playerTargetPosition = new Vector3(-100, -2, 0); // 이동할 위치 설정
        LoadingSceneManager.LoadScene("4.part3");
    }
    public static void Go_5_part4()
    {
        playerTargetPosition = new Vector3(-100, -2, 0); // 이동할 위치 설정
        LoadingSceneManager.LoadScene("5.part4");

    }
    public static void Go_99_EndGame() => LoadingSceneManager.LoadScene("99.EndGame");
    public void GameQuit() => Application.Quit();
}
