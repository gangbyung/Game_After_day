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
    [Header("슬라이더")]//슬라이더 연결
    public Slider staminaSlider;
    public Slider HpSlider;
    public Slider Radiation_exposure_Slider;
    [Header("스태미나 기능")]//최대 스태미나, 현재 스태미나
    public float maxStamina = 100f;
    public float currentStamina = 100f;
    //달렸을때 스태미나 소모량, 점프하였을때 스태미나 소모량
    public float runStamina = 10f; //초당 5
    public float jumpStamina = 10f; //횟수당 10
    //가만히 있을때 회복되는 스태미나 량
    public float recoveryStamina = 2f; //초당 2
    public float downRecoveryStamina = 10;
    [Header("HP 기능")]//HP 기능 추가
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
    public void UpdateUI() //ui업데이트
    {
        staminaSlider.value = currentStamina;
        HpSlider.value = currentHp;
        Radiation_exposure_Slider.value = RadiationController.Instance.currentRadiationExposure;
    }

    
}
