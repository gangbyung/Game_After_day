using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutButton : MonoBehaviour
{
    public float fadeDuration = 1f; // 페이드 아웃이 완료되는 시간

    private CanvasGroup canvasGroup;
    private Button button;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        // 페이드 아웃이 완료된 후, 버튼을 비활성화합니다.
        canvasGroup.alpha = 0f;
        button.interactable = false;
        
        gameObject.SetActive(false);
    }
}
