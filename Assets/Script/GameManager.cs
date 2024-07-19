using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Image portraiImg;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

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

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
        if(isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            portraiImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraiImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;

            portraiImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;
    }
    public void Pause() //게임 일시정지 함수
    {
        Time.timeScale = 0f;
    }
    public void Resume() //게임 일시정지 해제 함수
    {
        Time.timeScale = 1f;
    }
}