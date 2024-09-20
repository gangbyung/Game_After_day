using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Hud : MonoBehaviour
{
    Inventory inven;
    Changemap chmap;

    private static readonly object _lock = new object();
    [Header("�����̴�")]//�����̴� ����
    public Slider staminaSlider;
    public Slider HpSlider;
    public Slider Radiation_exposure_Slider;
    [Header("%����")]
    public TextMeshProUGUI StaminaPer;
    public TextMeshProUGUI HpPer;
    public TextMeshProUGUI Radiation_exposurePer;

    [Header("�κ��丮")]//�κ��丮����
    public GameObject InventoryPanel; //�κ��丮 �ǳ�
    bool activeInventory = false; //�κ��丮�� �����ִ��� �����ִ��� �˷��ִ� ����

    public Slot[] slots;
    public Transform slotHolder;



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
    public Button SaveButton;

    public bool isDead = false;
    public bool isRadiDmg = false;

    private Coroutine damageCoroutine;

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

        chmap = GetComponent<Changemap>();
        //���� �� ���¹̳��� Hp�� �ִ����� ����
        currentStamina = maxStamina;
        currentHp = maxHp;
        //��ư Ŭ��
        PauseButton.onClick.AddListener(OnPauseButtonClicked);
        ResumeButton.onClick.AddListener(OnResumeButtonCliked);
        EscResumeButton.onClick.AddListener(OnResumeButtonCliked);
        SaveButton.onClick.AddListener(SaveClik);
        //�κ��丮 ����
        InventoryPanel.SetActive(activeInventory);

        damageCoroutine = StartCoroutine(CheckRadiation());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && activeInventory == false) //���� �Ͻ����� ȣ��
        {
            if (GameEscape)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.I) && GameEscape == false) //�κ��丮 ȣ��
        {
            activeInventory = !activeInventory;
            InventoryPanel.SetActive(activeInventory);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activeInventory)
            {
                activeInventory = false;
                InventoryPanel.SetActive(activeInventory);
            }
        }

        StaminaPer.text = currentStamina.ToString("F0") + "%";
        HpPer.text = currentHp.ToString("F0") + "%";
        Radiation_exposurePer.text = RadiationController.Instance.currentRadiationExposure.ToString("F0") + "%";

        GameOver();

        
    }
    void OnDestroy()
    {
        // ������Ʈ�� �ı��� �� �ڷ�ƾ ����
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
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
    public void ResumeGame() //�̾��ϱ�
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
        for (int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    void GameOver()
    {
        if (currentHp < 1f && !isDead)
        {
            Changemap.Go_80_DeadGame();
            isDead = true;
        }

    }
    public void SaveClik()
    {
        DataManager.Instance.GameSave();
    }

    IEnumerator CheckRadiation()
    {
        while (true)
        {
            // 1�� ���
            yield return new WaitForSeconds(1f);

            // ���� ��ġ�� 90 �̻��� �� HP ����
            if (RadiationController.Instance.currentRadiationExposure >= 90f)
            {
                currentHp -= 10f;
                Debug.Log("HP ����: " + currentHp);

                // HP�� 0 �����̸� ��� ó��
                if (currentHp <= 1f)
                {
                    Debug.Log("�÷��̾� ���");
                    // ��� ó�� ������ ���⿡ �߰�
                    yield break;  // �ڷ�ƾ ����
                }
            }
        }
    }
}
