using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    public Image targetImage; // 페이드인할 대상 이미지
    public float fadeDuration = 1.0f; // 페이드인 시간 (초)
    public float delayBeforeDestroy = 2.0f; // 이미지가 삭제되기 전 대기 시간 (초)
    public GameObject talkpanel;
    private void Start()
    {
        // 이미지 초기 상태를 비활성화
        targetImage.gameObject.SetActive(false);
    }

    public void TriggerFadeIn()
    {
        // 이미지 활성화
        targetImage.gameObject.SetActive(true);
        StartCoroutine(FadeInAndDestroy());
    }

    private IEnumerator FadeInAndDestroy()
    {
        // 이미지의 투명도를 서서히 증가시키며 페이드인 효과 적용
        float elapsed = 0f;
        Color originalColor = targetImage.color;
        originalColor.a = 0f;
        targetImage.color = originalColor;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // 지정한 시간 후 이미지 삭제
        yield return new WaitForSeconds(delayBeforeDestroy);
        Destroy(targetImage.gameObject);

        Changemap.Go_4_part3();
        if (talkpanel.activeSelf)
        {
            GameManager.Instance.NextTalk();
        }
    }
}
