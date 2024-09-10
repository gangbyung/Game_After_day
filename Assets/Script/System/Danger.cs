using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Danger : MonoBehaviour
{
    public CanvasGroup canvasGroup; // ���̵� ���
    public Text messageText;        // �ؽ�Ʈ ������Ʈ
    public string[] messages;       // ǥ���� �ؽ�Ʈ �迭
    public float fadeDuration = 1f; // ���̵� �ð�
    public float displayDuration = 1f; // �ؽ�Ʈ�� ������ ���̴� ���·� �����Ǵ� �ð�

    public void StartFade()
    {
        StartCoroutine(FadeInOutRoutine());
    }
     public IEnumerator FadeInOutRoutine()
    {  
        canvasGroup.gameObject.SetActive(true);
        // �޽����� �� �� ǥ��
        for (int i = 0; i < 2; i++)
        {
            if (i < messages.Length)
            {
                messageText.text = messages[i]; // �� �ݺ����� �ؽ�Ʈ ����
            }

            // ���̵���
            yield return StartCoroutine(Fade(0f, 1f));
            // �ؽ�Ʈ�� ������ ���̴� ���� ����
            yield return new WaitForSeconds(displayDuration);
            // ���̵�ƿ�
            yield return StartCoroutine(Fade(1f, 0f));
        }

        // ��� ���̵尡 ���� �� ������Ʈ ����
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
            yield return null; // �����Ӹ��� ������Ʈ
        }

        canvasGroup.alpha = endAlpha; // ��Ȯ�ϰ� �� ������ ����
    }
}
