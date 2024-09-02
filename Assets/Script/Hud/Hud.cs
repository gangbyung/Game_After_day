using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    Inventory inven;

    private static readonly object _lock = new object();
    [Header("슬라이더")]//슬라이더 연결
    public Slider staminaSlider;
    public Slider HpSlider;
    public Slider Radiation_exposure_Slider;
    [Header("%연결")]
    public TextMeshProUGUI StaminaPer;
    public TextMeshProUGUI HpPer;
    public TextMeshProUGUI Radiation_exposurePer;

    [Header("인벤토리")]//인벤토리연결
    public GameObject InventoryPanel; //인벤토리 판넬
    bool activeInventory = false; //인벤토리가 켜져있는지 꺼져있는지 알려주는 변수

    public Slot[] slots;
    public Transform slotHolder;



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
    [Header("ESC 기능")]
    public bool GameEscape = false; //현재 esc를 눌렀는지 누르지 않았는지 알려주는 변수
    public GameObject pauseMainCanvas; //일시정지를 누를 시 나오는 ui
    public Button ResumeButton; //이어하기 버튼 변수
    public Button PauseButton; //일시정지 버튼 변수
    public Button EscResumeButton; //esc를 누를때 나오는 퍼즈 버튼

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
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        //시작 시 스태미나와 Hp를 최댓값으로 설정
        currentStamina = maxStamina; 
        currentHp = maxHp;
        //버튼 클릭
        PauseButton.onClick.AddListener(OnPauseButtonClicked);
        ResumeButton.onClick.AddListener(OnResumeButtonCliked);
        EscResumeButton.onClick.AddListener(OnResumeButtonCliked);
        //인벤토리 끄기
        InventoryPanel.SetActive(activeInventory);
    }
    
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && activeInventory == false) //게임 일시정지 호출
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
        if (Input.GetKeyDown(KeyCode.I) && GameEscape == false) //인벤토리 호출
        {
            activeInventory = !activeInventory;
            InventoryPanel.SetActive(activeInventory);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(activeInventory)
            {
                activeInventory = false;
                InventoryPanel.SetActive(activeInventory);
            }
        }
        StaminaPer.text = currentStamina.ToString("F0") + "%";
        HpPer.text = currentHp.ToString("F0") + "%";
        Radiation_exposurePer.text = RadiationController.Instance.currentRadiationExposure.ToString("F0") + "%";
    }
    public void UpdateUI() //ui업데이트 함수
    {
        staminaSlider.value = currentStamina;
        HpSlider.value = currentHp;
        Radiation_exposure_Slider.value = RadiationController.Instance.currentRadiationExposure;
    }
    void OnPauseButtonClicked() //퍼즈 버튼 클릭
    {
        if (!GameEscape)
            PauseGame();
    }
    void OnResumeButtonCliked() //리점 버튼클릭
    {
        if (GameEscape)
            ResumeGame();
    }
    public void PauseGame() //퍼즈시 일어나는 효과
    {
        GameManager.Instance.Pause(); //시간정지
        GameEscape = true;
        pauseMainCanvas.SetActive(true); //버튼 끄기

        PauseButton.gameObject.SetActive(false);
        ResumeButton.gameObject.SetActive(true);
        EscResumeButton.gameObject.SetActive(true);
    }
    public void ResumeGame() //이어하기
    {
        GameManager.Instance.Resume();
        GameEscape = false;
        pauseMainCanvas.SetActive(false);

        ResumeButton.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(true);
        EscResumeButton.gameObject.SetActive(false);
    }

    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;

            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    public void AddSlot()
    {
        inven.SlotCnt++;
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i = 0;i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
