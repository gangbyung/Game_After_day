using System.Collections;
using UnityEngine;

public class RadiationController : MonoBehaviour
{
    private static RadiationController _instance;
    private static readonly object _lock = new object();

    public static RadiationController Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<RadiationController>();

                        if (_instance == null)
                        {
                            GameObject singletonObject = new GameObject();
                            _instance = singletonObject.AddComponent<RadiationController>();
                            singletonObject.name = nameof(RadiationController) + " (Singleton)";
                            DontDestroyOnLoad(singletonObject);
                        }
                    }
                }
            }
            return _instance;
        }
    }

    private Coroutine radiationCoroutine;

    public float currentRadiationExposure;
    public float maxRadiationExposure = 100f;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Radiazon"))
        {
            StartRadiationExposure();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Radiazon"))
        {
            StartRadiationExposure();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Radiazon"))
        {
            EndRadiationExposure();
        }
    }

    public void StartRadiationExposure()
    {
        if (radiationCoroutine == null)
        {
            radiationCoroutine = StartCoroutine(IncreaseRadiationExposure());
        }
    }

    void EndRadiationExposure()
    {
        if (radiationCoroutine != null)
        {
            StopCoroutine(radiationCoroutine);
            radiationCoroutine = null;
        }
    }

    private IEnumerator IncreaseRadiationExposure()
    {
        while (true)
        {
            // 방사선 노출량 증가
            currentRadiationExposure = Mathf.Clamp(currentRadiationExposure + 10, 0f, maxRadiationExposure);
            Hud.Instance.UpdateUI();

            // 1초 대기
            yield return new WaitForSeconds(1f);
        }
    }
}
