using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    public Image targetImage; // ���̵����� ��� �̹���
    public float fadeDuration = 1.0f; // ���̵��� �ð� (��)
    public float delayBeforeDestroy = 2.0f; // �̹����� �����Ǳ� �� ��� �ð� (��)
    public GameObject talkpanel;
    private void Start()
    {
        // �̹��� �ʱ� ���¸� ��Ȱ��ȭ
        targetImage.gameObject.SetActive(false);
    }

    public void TriggerFadeIn()
    {
        // �̹��� Ȱ��ȭ
        targetImage.gameObject.SetActive(true);
        StartCoroutine(FadeInAndDestroy());
    }

    private IEnumerator FadeInAndDestroy()
    {
        // �̹����� ������ ������ ������Ű�� ���̵��� ȿ�� ����
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

        // ������ �ð� �� �̹��� ����
        yield return new WaitForSeconds(delayBeforeDestroy);
        Destroy(targetImage.gameObject);

        Changemap.Go_4_part3();
        if (talkpanel.activeSelf)
        {
            GameManager.Instance.NextTalk();
        }
    }
}
