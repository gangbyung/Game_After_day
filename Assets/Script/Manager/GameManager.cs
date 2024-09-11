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

    public static GameManager Instance //싱글톤 패턴
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
            Debug.LogError("TalkManager가 설정되지 않았습니다. TalkManager가 있는지 확인하세요.");
        }
    }
    public void Action(GameObject scanObj) // 오브젝트 스캔
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

    public void Talk(int id, bool isNpc) //대사 내보내기
    {
        if (talkManager != null)
        {
            // talkManager 인스턴스가 null이 아니면, 해당 메서드를 호출
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
            //텍스트 분할
            talkText.text = talkData.Split(':')[0]; 
            NameText.text = NameData.Split('&')[0];
            //이미지 적용
            portraiImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraiImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            //텍스트에 저장해둔 데이터 적용
            talkText.text = talkData;
            NameText.text = NameData;

            portraiImg.color = new Color(1, 1, 1, 0);
        }
        //다음대사 출력
        isAction = true;
        talkIndex++;
        NameIndex++;
    }
    public void Pause() //게임 일시정지 함수
    {
        Time.timeScale = 0f;
    }
    public void Resume() //게임 일시정지 해제 함수
    {
        Time.timeScale = 1f;
    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 만약 현재 씬이 지정된 씬 이름과 같다면
        if (System.Array.Exists(scenesToDestroy, element => element == scene.name))
        {
            // 각 오브젝트가 null인지 확인한 후 파괴합니다.
            if (Player != null)
            {
                Destroy(Player); // 오브젝트를 파괴합니다.
                Player = null; // 참조를 제거하여 가비지 컬렉터가 처리할 수 있도록 합니다.
            }
            if (Hud != null)
            {
                Destroy(Hud); // 오브젝트를 파괴합니다.
                Hud = null;
            }
            if (TalkManager != null)
            {
                Destroy(TalkManager); // 오브젝트를 파괴합니다.
                TalkManager = null;
            }
            if (MainCamera != null)
            {
                Destroy(MainCamera); // 오브젝트를 파괴합니다.
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
        Debug.Log("초기화");
    }
    public void ResetSpecificObjects()
    {
        if (Player != null) Player.SetActive(true);
        if (Hud != null) Hud.SetActive(true);
        if (TalkManager != null) TalkManager.SetActive(true);
        if (MainCamera != null) MainCamera.SetActive(true);
    }
    
}