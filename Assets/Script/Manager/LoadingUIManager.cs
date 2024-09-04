using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro; // TextMeshPro를 사용하기 위한 네임스페이스 추가

public class LoadingUIManager : MonoBehaviour
{
    public TextMeshProUGUI mapNameText;  // 맵 이름을 표시할 TextMeshPro 텍스트
    public Slider progressBar;           // 프로그레스바 UI
    public Image fadePanel;              // 페이드 아웃 효과를 위한 패널
    public float fadeDuration = 1f;      // 페이드 아웃 지속 시간
    public float delayBeforeSceneLoad = 0.1f;  // 씬 전환 전 대기 시간

    private Dictionary<string, string> sceneToMapName = new Dictionary<string, string>();

    private void Start()
    {
        // Null 검사를 추가하여 오류 확인
        if (mapNameText == null)
            Debug.LogError("mapNameText가 할당되지 않았습니다. 인스펙터에서 설정해 주세요.");

        if (progressBar == null)
            Debug.LogError("progressBar가 할당되지 않았습니다. 인스펙터에서 설정해 주세요.");

        if (fadePanel == null)
            Debug.LogError("fadePanel이 할당되지 않았습니다. 인스펙터에서 설정해 주세요.");

        // 씬 이동 이벤트에 대한 리스너 추가
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 초기화 상태로 패널 비활성화
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
            Debug.LogWarning($"씬 이름 '{sceneName}'에 대한 맵 이름이 정의되지 않았습니다.");
        }
    }

    // 씬과 맵 이름 간의 매핑을 설정하는 메서드
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
            Debug.LogError("UI 컴포넌트가 null 상태입니다. 인스펙터에서 설정을 확인하세요.");
            return; // UI 컴포넌트가 null이면 함수 종료
        }

        // 맵 이름 텍스트를 업데이트하고 보이도록 설정
        mapNameText.text = mapName;
        mapNameText.CrossFadeAlpha(1, 0.5f, false); // 텍스트 페이드 인

        // 패널을 활성화하고 프로그레스바 초기화
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 0); // 패널을 투명하게 설정

        // 프로그레스바 초기화 및 애니메이션 시작
        StartCoroutine(ProgressBarAnimation());
    }

    private IEnumerator ProgressBarAnimation()
    {
        progressBar.value = 0;
        float elapsedTime = 0f;
        float duration = 1f; // 프로그레스바가 1초 동안 애니메이션

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        // 애니메이션이 완료된 후 페이드 아웃 시작
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

        // 페이드 아웃이 끝난 후 지연 시간 추가
        yield return new WaitForSeconds(delayBeforeSceneLoad);

        // 페이드 아웃이 완료된 후 패널 비활성화
        fadePanel.gameObject.SetActive(false);

        
    }
    private void OnDestroy()
    {
        // 씬이 파괴될 때 리스너 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
