using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    public void OnRestartGame()
    {
        // ��ŸƮ ������ ��ȯ
        SceneManager.LoadScene("0.Start"); // "StartScene"�� ���� ��ŸƮ ���� �̸����� ��ü
    }
}