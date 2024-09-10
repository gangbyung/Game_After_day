using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Danger : MonoBehaviour
{
    public CanvasGroup canvasGroup; // 페이드 대상
    public Text messageText;        // 텍스트 컴포넌트
    public string[] messages;       // 표시할 텍스트 배열
    public float fadeDuration = 1f; // 페이드 시간
    public float displayDuration = 1f; // 텍스트가 완전히 보이는 상태로 유지되는 시간

    public void StartFade()
    {
        StartCoroutine(FadeInOutRoutine());
    }
     public IEnumerator FadeInOutRoutine()
    {  
        canvasGroup.gameObject.SetActive(true);
        // 메시지를 두 번 표시
        for (int i = 0; i < 2; i++)
        {
            if (i < messages.Length)
            {
                messageText.text = messages[i]; // 각 반복에서 텍스트 설정
            }

            // 페이드인
            yield return StartCoroutine(Fade(0f, 1f));
            // 텍스트가 완전히 보이는 상태 유지
            yield return new WaitForSeconds(displayDuration);
            // 페이드아웃
            yield return StartCoroutine(Fade(1f, 0f));
        }

        // 모든 페이드가 끝난 후 오브젝트 삭제
        Destroy(gameObject);
        canvasGroup.gameObject.SetActive(false);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeDuration);
            yield return null; // 프레임마다 업데이트
        }

        canvasGroup.alpha = endAlpha; // 정확하게 끝 값으로 맞춤
    }
}
