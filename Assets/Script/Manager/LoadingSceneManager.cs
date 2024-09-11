using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;

    [SerializeField]
    private Slider progressBar;  // Slider로 변경

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        if (sceneName == "1.part0")
            DataManager.Instance.map = 1;
        if(sceneName == "2.part1")
            DataManager.Instance.map = 2;
        if (sceneName == "4.part3")
            DataManager.Instance.map = 3;
        if(sceneName == "5.part4")
            DataManager.Instance.map = 4;
        if(sceneName == "6.part5")
            DataManager.Instance.map = 5;
        if(sceneName == "7.part6")
            DataManager.Instance.map = 6;
        SceneManager.LoadScene("98.LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            // op.progress는 0.9까지 증가하며, 완료되면 1로 설정
            if (op.progress < 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                if (progressBar.value >= 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
