using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro; // TextMeshPro�� ����ϱ� ���� ���ӽ����̽� �߰�

public class LoadingUIManager : MonoBehaviour
{
    public TextMeshProUGUI mapNameText;  // �� �̸��� ǥ���� TextMeshPro �ؽ�Ʈ
    public Slider progressBar;           // ���α׷����� UI
    public Image fadePanel;              // ���̵� �ƿ� ȿ���� ���� �г�
    public float fadeDuration = 1f;      // ���̵� �ƿ� ���� �ð�
    public float delayBeforeSceneLoad = 0.1f;  // �� ��ȯ �� ��� �ð�

    private Dictionary<string, string> sceneToMapName = new Dictionary<string, string>();

    private void Start()
    {
        // Null �˻縦 �߰��Ͽ� ���� Ȯ��
        if (mapNameText == null)
            Debug.LogError("mapNameText�� �Ҵ���� �ʾҽ��ϴ�. �ν����Ϳ��� ������ �ּ���.");

        if (progressBar == null)
            Debug.LogError("progressBar�� �Ҵ���� �ʾҽ��ϴ�. �ν����Ϳ��� ������ �ּ���.");

        if (fadePanel == null)
            Debug.LogError("fadePanel�� �Ҵ���� �ʾҽ��ϴ�. �ν����Ϳ��� ������ �ּ���.");

        // �� �̵� �̺�Ʈ�� ���� ������ �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;

        // �ʱ�ȭ ���·� �г� ��Ȱ��ȭ
        fadePanel.gameObject.SetActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        if (sceneToMapName.TryGetValue(sceneName, out string mapName))
        {
            UpdateUI(mapName);
        }
        else
        {
            Debug.LogWarning($"�� �̸� '{sceneName}'�� ���� �� �̸��� ���ǵ��� �ʾҽ��ϴ�.");
        }
    }

    // ���� �� �̸� ���� ������ �����ϴ� �޼���
    public void SetMapNameForScene(string sceneName, string mapName)
    {
        if (sceneToMapName.ContainsKey(sceneName))
        {
            sceneToMapName[sceneName] = mapName;
        }
        else
        {
            sceneToMapName.Add(sceneName, mapName);
        }
    }

    private void UpdateUI(string mapName)
    {
        if (mapNameText == null || progressBar == null || fadePanel == null)
        {
            Debug.LogError("UI ������Ʈ�� null �����Դϴ�. �ν����Ϳ��� ������ Ȯ���ϼ���.");
            return; // UI ������Ʈ�� null�̸� �Լ� ����
        }

        // �� �̸� �ؽ�Ʈ�� ������Ʈ�ϰ� ���̵��� ����
        mapNameText.text = mapName;
        mapNameText.CrossFadeAlpha(1, 0.5f, false); // �ؽ�Ʈ ���̵� ��

        // �г��� Ȱ��ȭ�ϰ� ���α׷����� �ʱ�ȭ
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 0); // �г��� �����ϰ� ����

        // ���α׷����� �ʱ�ȭ �� �ִϸ��̼� ����
        StartCoroutine(ProgressBarAnimation());
    }

    private IEnumerator ProgressBarAnimation()
    {
        progressBar.value = 0;
        float elapsedTime = 0f;
        float duration = 1f; // ���α׷����ٰ� 1�� ���� �ִϸ��̼�

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        // �ִϸ��̼��� �Ϸ�� �� ���̵� �ƿ� ����
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 1f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }

        // ���̵� �ƿ��� ���� �� ���� �ð� �߰�
        yield return new WaitForSeconds(delayBeforeSceneLoad);

        // ���̵� �ƿ��� �Ϸ�� �� �г� ��Ȱ��ȭ
        fadePanel.gameObject.SetActive(false);

        
    }
    private void OnDestroy()
    {
        // ���� �ı��� �� ������ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
