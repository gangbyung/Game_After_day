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
    public GameObject MainCanera;

    public List<string> scenesToDestroy = new List<string> { "3.Endpart0", "99.EndGame" };

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
                            GameObject singletonObject = new GameObject();
                            _instance = singletonObject.AddComponent<GameManager>();
                            singletonObject.name = nameof(GameManager) + " (Singleton)";
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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void Action(GameObject scanObj) //오브젝트 스캔
    {
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc) //대사 내보내기
    {
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
            talkText.text = talkData.Split(':')[0];
            NameText.text = NameData.Split('&')[0];

            portraiImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraiImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            NameText.text = NameData;

            portraiImg.color = new Color(1, 1, 1, 0);
        }
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



    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 만약 현재 씬이 지정된 씬 이름과 같다면
        if (scenesToDestroy.Contains(scene.name))
        {
            Destroy(Player); // 오브젝트를 파괴합니다.
            Destroy(Hud); // 오브젝트를 파괴합니다.
            Destroy(TalkManager); // 오브젝트를 파괴합니다.
            Destroy(MainCanera); // 오브젝트를 파괴합니다.
        }
    }
}