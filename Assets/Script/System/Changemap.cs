using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changemap : MonoBehaviour
{
    public static void Go_0_Start() => LoadingSceneManager.LoadScene("0.Start"); //SceneManager.LoadScene("0.Start");
    public static void Go_1_part0() => LoadingSceneManager.LoadScene("1.part0");
    public static void Go_2_part1() => LoadingSceneManager.LoadScene("2.part1");
    public static void Go_3_Endpart0() => LoadingSceneManager.LoadScene("3.Endpart0");
    public static void Go_4_part3() => LoadingSceneManager.LoadScene("4.part3");
    public static void Go_99_EndGame() => LoadingSceneManager.LoadScene("99.EndGame");
    public void GameQuit() => Application.Quit();
}
