using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    public void OnRestartGame()
    {
        // 스타트 씬으로 전환
        SceneManager.LoadScene("0.Start"); // "StartScene"은 실제 스타트 씬의 이름으로 교체
    }
}