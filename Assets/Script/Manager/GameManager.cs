using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Image portraiImg;
    public Text talkText;
    public Text NameText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;
    public int NameIndex;

    public GameObject Player;
    public GameObject Hud;
    public GameObject TalkManager;
    public GameObject MainCamera;
    public GameObject GameObj;


    public string[] scenesToDestroy;

    private static GameManager _instance;
    private static readonly object _lock = new object();

    public static GameManager Instance //�̱��� ����
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<GameManager>();

                        if (_instance == null)
                        {
                            //GameObject singletonObject = new GameObject();
                            //_instance = singletonObject.AddComponent<GameManager>();
                            //singletonObject.name = nameof(GameManager) + " (Singleton)";
                            //DontDestroyOnLoad(singletonObject);
                            
                        }
                        else
                        {
                            DontDestroyOnLoad(_instance.gameObject);
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
            
            DontDestroyOnLoad(Player);
            DontDestroyOnLoad(Hud);
            DontDestroyOnLoad(TalkManager);
            DontDestroyOnLoad(MainCamera);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if(talkManager == null)
        {
            talkManager = FindObjectOfType<TalkManager>();
        }
        if (talkManager == null)
        {
            Debug.LogError("TalkManager�� �������� �ʾҽ��ϴ�. TalkManager�� �ִ��� Ȯ���ϼ���.");
        }
    }
    public void Action(GameObject scanObj) // ������Ʈ ��ĵ
    {
        if (scanObj == null)
        {
            Debug.LogError("null!");
            return;
        }

        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();

        if (objData == null)
        {
            Debug.LogError("missing");
            return;
        }

        Talk(objData.id, objData.isNpc);
        talkPanel.SetActive(isAction);
    }
    public void NextTalk()
    {
        ObjectData objData = scanObject.GetComponent<ObjectData>();

        Talk(objData.id, objData.isNpc);
        talkPanel.SetActive(isAction);
    }
    public void TalkBugpix()
    {
        talkPanel.SetActive(false);
        isAction = false;
        talkIndex = 0;
        NameIndex = 0;
    }

    public void Talk(int id, bool isNpc) //��� ��������
    {
        if (talkManager != null)
        {
            // talkManager �ν��Ͻ��� null�� �ƴϸ�, �ش� �޼��带 ȣ��
            string talkText = talkManager.GetTalk(id, 0);
            
        }
        string talkData = talkManager.GetTalk(id, talkIndex);
        
        string NameData = talkManager.GetName(id, NameIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            NameIndex = 0;
            return;
        }
        if(isNpc)
        {
            //�ؽ�Ʈ ����
            talkText.text = talkData.Split(':')[0]; 
            NameText.text = NameData.Split('&')[0];
            //�̹��� ����
            portraiImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraiImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            //�ؽ�Ʈ�� �����ص� ������ ����
            talkText.text = talkData;
            NameText.text = NameData;

            portraiImg.color = new Color(1, 1, 1, 0);
        }
        //������� ���
        isAction = true;
        talkIndex++;
        NameIndex++;
    }
    public void Pause() //���� �Ͻ����� �Լ�
    {
        Time.timeScale = 0f;
    }
    public void Resume() //���� �Ͻ����� ���� �Լ�
    {
        Time.timeScale = 1f;
    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� ���� ���� ������ �� �̸��� ���ٸ�
        if (System.Array.Exists(scenesToDestroy, element => element == scene.name))
        {
            // �� ������Ʈ�� null���� Ȯ���� �� �ı��մϴ�.
            if (Player != null)
            {
                Destroy(Player); // ������Ʈ�� �ı��մϴ�.
                Player = null; // ������ �����Ͽ� ������ �÷��Ͱ� ó���� �� �ֵ��� �մϴ�.
            }
            if (Hud != null)
            {
                Destroy(Hud); // ������Ʈ�� �ı��մϴ�.
                Hud = null;
            }
            if (TalkManager != null)
            {
                Destroy(TalkManager); // ������Ʈ�� �ı��մϴ�.
                TalkManager = null;
            }
            if (MainCamera != null)
            {
                Destroy(MainCamera); // ������Ʈ�� �ı��մϴ�.
                MainCamera = null;
            }
            if(GameObj != null)
            {
                Destroy(GameObj);
                GameObj = null;
            }
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void ResetGameData()
    {
        Debug.Log("�ʱ�ȭ");
    }
    public void ResetSpecificObjects()
    {
        if (Player != null) Player.SetActive(true);
        if (Hud != null) Hud.SetActive(true);
        if (TalkManager != null) TalkManager.SetActive(true);
        if (MainCamera != null) MainCamera.SetActive(true);
    }
    
}