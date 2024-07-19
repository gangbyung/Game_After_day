using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.WSA;

public class Hud : MonoBehaviour
{
    private static readonly object _lock = new object();
    [Header("�����̴�")]//�����̴� ����
    public Slider staminaSlider;
    public Slider HpSlider;
    public Slider Radiation_exposure_Slider;
    [Header("���¹̳� ���")]//�ִ� ���¹̳�, ���� ���¹̳�
    public float maxStamina = 100f;
    public float currentStamina = 100f;
    //�޷����� ���¹̳� �Ҹ�, �����Ͽ����� ���¹̳� �Ҹ�
    public float runStamina = 10f; //�ʴ� 5
    public float jumpStamina = 10f; //Ƚ���� 10
    //������ ������ ȸ���Ǵ� ���¹̳� ��
    public float recoveryStamina = 2f; //�ʴ� 2
    public float downRecoveryStamina = 10;
    [Header("HP ���")]//HP ��� �߰�
    public float currentHp = 100f;
    public float maxHp = 100f;
    [Header("ESC ���")]
    public bool GameEscape = false; //���� esc�� �������� ������ �ʾҴ��� �˷��ִ� ����
    public GameObject pauseMainCanvas; //�Ͻ������� ���� �� ������ ui
    public Button ResumeButton; //�̾��ϱ� ��ư ����
    public Button PauseButton; //�Ͻ����� ��ư ����
    public Button EscResumeButton; //esc�� ������ ������ ���� ��ư

    private static Hud _instance;
    public static Hud Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<Hud>();

                        if (_instance == null)
                        {
                            GameObject singletonObject = new GameObject();
                            _instance = singletonObject.AddComponent<Hud>();
                            singletonObject.name = nameof(Hud) + " (Singleton)";
                            DontDestroyOnLoad(singletonObject);
                        }
                    }
                }
            }
            return _instance;
        }
    }

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
    void Start()
    {
        //���� �� ���¹̳��� Hp�� �ִ����� ����
        currentStamina = maxStamina; 
        currentHp = maxHp;
        //��ư Ŭ��
        PauseButton.onClick.AddListener(OnPauseButtonClicked);
        ResumeButton.onClick.AddListener(OnResumeButtonCliked);
        EscResumeButton.onClick.AddListener(OnResumeButtonCliked);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameEscape)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void UpdateUI() //ui������Ʈ �Լ�
    {
        staminaSlider.value = currentStamina;
        HpSlider.value = currentHp;
        Radiation_exposure_Slider.value = RadiationController.Instance.currentRadiationExposure;
    }
    void OnPauseButtonClicked() //���� ��ư Ŭ��
    {
        if (!GameEscape)
            PauseGame();
    }
    void OnResumeButtonCliked() //���� ��ưŬ��
    {
        if (GameEscape)
            ResumeGame();
    }
    public void PauseGame() //����� �Ͼ�� ȿ��
    {
        GameManager.Instance.Pause(); //�ð�����
        GameEscape = true;
        pauseMainCanvas.SetActive(true); //��ư ����

        PauseButton.gameObject.SetActive(false);
        ResumeButton.gameObject.SetActive(true);
        EscResumeButton.gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        GameManager.Instance.Resume();
        GameEscape = false;
        pauseMainCanvas.SetActive(false);

        ResumeButton.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(true);
        EscResumeButton.gameObject.SetActive(false);
    }
}
