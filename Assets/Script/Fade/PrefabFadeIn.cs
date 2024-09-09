using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFadeIn : MonoBehaviour
{
    public float fadeDuration = 2.0f; // ���̵��ο� �ɸ��� �ð�(��)
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0;
            spriteRenderer.color = color;
            StartCoroutine(FadeInCoroutine());
        }
    }

    private System.Collections.IEnumerator FadeInCoroutine()
    {
        float startTime = Time.time;
        while (Time.time < startTime + fadeDuration)
        {
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(0, 1, (Time.time - startTime) / fadeDuration);
            spriteRenderer.color = color;
            yield return null; // ���� �����ӱ��� ���
        }
        // ���� ���İ��� ����
        Color finalColor = spriteRenderer.color;
        finalColor.a = 1;
        spriteRenderer.color = finalColor;
    }
}
