using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutButton : MonoBehaviour
{
    public float fadeDuration = 1f; // ���̵� �ƿ��� �Ϸ�Ǵ� �ð�

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

        // ���̵� �ƿ��� �Ϸ�� ��, ��ư�� ��Ȱ��ȭ�մϴ�.
        canvasGroup.alpha = 0f;
        button.interactable = false;
        
        gameObject.SetActive(false);
    }
}
