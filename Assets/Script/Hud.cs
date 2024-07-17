using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.WSA;

public class Hud : MonoBehaviour
{
    private static Hud _instance;
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
        currentStamina = maxStamina;
        currentHp = maxHp;

    }
    public void UpdateUI() //ui������Ʈ
    {
        staminaSlider.value = currentStamina;
        HpSlider.value = currentHp;
        Radiation_exposure_Slider.value = RadiationController.Instance.currentRadiationExposure;
    }

    
}
