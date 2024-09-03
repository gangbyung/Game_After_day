using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TalkManager : MonoBehaviour
{
    public GameObject choiceUI;  // 선택 UI 패널
    public Button choiceButton1; // 첫 번째 버튼
    public Button choiceButton2; // 두 번째 버튼
    public TextMeshProUGUI choiceButton1Text; // 첫 번째 버튼의 텍스트
    public TextMeshProUGUI choiceButton2Text; // 두 번째 버튼의 텍스트

    private Action<int> onChoiceMade;  // 버튼 클릭 시 실행될 콜백 함수

    Dictionary<int, string[]> talkData;
    Dictionary<int, string[]> NameData;
    Dictionary<int, Sprite> portraitData;
        
    public Sprite[] portraitArr;
    public Sprite[] player_portraitArr;

    public GameObject[] Buttons;

    public static TalkManager Instance { get; private set; }

    Changemap Chmap;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            talkData = new Dictionary<int, string[]>();
            NameData = new Dictionary<int, string[]>();
            portraitData = new Dictionary<int, Sprite>();
            GenerateData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Chmap = GetComponent<Changemap>();
        NpcAction a = FindObjectOfType<NpcAction>();
    }
    void GenerateData()
    {
        //NPC 1 선캡아줌마 대사
        talkData.Add(1000, new string[] { "어유, 간만에 산책나왔더니만 계속 날씨가 이모양이네 ...:0", "발전소에서 뭔가 생긴 이후부터 하늘이 기분나쁘게 먹구름만 가득하지 뭐야:1", "그러고 보니 총각, 옛날에 저기서 일한다고 했었던 것 같은데, 뭔지 알어?:2" , "아마 발전소 폭발이랑 연관은 없을 거에요.:3", "그래? 그럼 뭐 그렇게 큰 일도 아니구만.:4" , "아, 조금있으면 장마철이기도 했구만,:5", "'...상황이 어떻게 되고 있는지는 몰라도 호우로 방사능 물질이 근방에 유출되면 큰일이 날텐데':6"});
        NameData.Add(1000, new string[] { "선캡아줌마&0", "선캡아줌마&1", "선캡아줌마&2", "주인공&3", "선캡아줌마&4", "선캡아줌마&5", "주인공&6"});

        //NPC 2 청년 대사
        talkData.Add(2000, new string[] { "1억...1억이라니..:0", "와, 무슨일인진 몰라도 축하드려요.:1", "내 집값..:2","어제까진 3억에 거래되던 내집이...으악!!!!:3" ,"'...무시하고 지나가자':4"});
        NameData.Add(2000, new string[] { "청년&0", "주인공&1", "청년&2", "청년&3", "주인공&4" });

        //NPC 3 과일가게 아줌마
        talkData.Add(3000, new string[] { "요즘 들어 뉴스에서 도통 안좋은 일들만 나온단 말이지.:0", "바로 요전번에는 그 서울 어디쯤에서 대지진이 일어났다고 하더만, 바로 오늘은 저기 공장 같은게 폭발했다 하지 않나:1", "하여간에 세상 참 흉흉하단 말이지...:2", "그래서 과일은 안 살겨?:3", "흠 그럼...:4","사과 하나만 주세요.:5" });
        NameData.Add(3000, new string[] { "과일가게 아줌마&0", "과일가게 아줌마&1", "과일가게 아줌마&2", "과일가게 아줌마&3", "주인공&4","주인공&5" });

        //NPC 4 경비원
        talkData.Add(4000, new string[] { "저기요! 잠시만요:0", "네?:1", "여긴 외부인 출입 금지인데 어떻게..?:2", "혹시 이번에 오시기로한 최 교수님이신가요?:3", "그건 아닌데요..:4", "아하..이곳부턴 외부인 출입 금지라서 이 이상 앞으로 가시면 안됩니다.:5" ,"'그럼 돌아가야하려나...':6","또각.또각. (누군가 오고있다):7" });
        NameData.Add(4000, new string[] { "???&0", "주인공&1", "경비원&2", "경비원&3", "주인공&4", "경비원&5","주인공&6","&7" });

        //NPC 5 후배
        talkData.Add(5000, new string[] { "어? 선배님 맞으시죠? 오랜만이네요! 저희 마지막으로 만난게 선배님 퇴직 기념으로 같이 술 마셨을 때니까...:0", "1년은 지났던 것 같은데! 아 혹시 여기로 차출되신건가요?:1", "어? 그건아니고 걷다보니 오게됐네:2", "아 그렇군요 사실 야기 일손이 꽤나 부족하거든요.. 발전소만 터진것이면 진작에 전문가들 밀어넣어서 지금쯤 해결되었을텐데..:3", "하필이면 그 터지기 1주일전에 서울에 대지진이 일어나는 바람에...:4", "..큼 잡설은 여기까지 하죠:5", "아무튼 이것도 인연인데, 한 번 구경이라도 하실래요??:6" });
        NameData.Add(5000, new string[] { "후배&0", "후배&1", "주인공&2", "후배&3", "후배&4", "후배&5","후배&6" });
        //---------------------------------------------------------------------------------------------------------------------------------

        //NPC 1 선캡아줌마
        portraitData.Add(1000 + 0, portraitArr[1]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[1]);
        portraitData.Add(1000 + 3, player_portraitArr[0]);
        portraitData.Add(1000 + 4, portraitArr[1]);
        portraitData.Add(1000 + 5, portraitArr[1]);
        portraitData.Add(1000 + 6, player_portraitArr[0]);

        //NPC 2 청년
        portraitData.Add(2000 + 0, portraitArr[2]);
        portraitData.Add(2000 + 1, player_portraitArr[0]);
        portraitData.Add(2000 + 2, portraitArr[2]);
        portraitData.Add(2000 + 3, portraitArr[2]);
        portraitData.Add(2000 + 4, player_portraitArr[0]);

        //NPC 3 과일가게 아줌마
        portraitData.Add(3000 + 0, portraitArr[3]);
        portraitData.Add(3000 + 1, portraitArr[3]);
        portraitData.Add(3000 + 2, portraitArr[3]);
        portraitData.Add(3000 + 3, portraitArr[3]);
        portraitData.Add(3000 + 4, player_portraitArr[0]);
        portraitData.Add(3000 + 5, player_portraitArr[0]);

        //NPC 4 경비원과 후배
        portraitData.Add(4000 + 0, portraitArr[4]);
        portraitData.Add(4000 + 1, player_portraitArr[0]);
        portraitData.Add(4000 + 2, portraitArr[4]);
        portraitData.Add(4000 + 3, portraitArr[4]);
        portraitData.Add(4000 + 4, player_portraitArr[0]);
        portraitData.Add(4000 + 5, portraitArr[4]);
        portraitData.Add(4000 + 6, player_portraitArr[0]);
        portraitData.Add(4000 + 7, portraitArr[0]);

        //NPC 5
        portraitData.Add(5000 + 0, portraitArr[5]);
        portraitData.Add(5000 + 1, portraitArr[5]);
        portraitData.Add(5000 + 2, player_portraitArr[0]);
        portraitData.Add(5000 + 3, portraitArr[5]);
        portraitData.Add(5000 + 4, portraitArr[5]);
        portraitData.Add(5000 + 5, portraitArr[5]);
        portraitData.Add(5000 + 6, portraitArr[5]);

    }

    public string GetTalk(int id, int talkIndex)
    {
        // 키가 존재하는지 확인
        if (talkData.ContainsKey(id))
        {
            if (talkIndex < talkData[id].Length)
            {
                return talkData[id][talkIndex];
            }
            else
            {
                Debug.LogWarning($"키 '{id}'에 대한 유효하지 않은 대화 인덱스: {talkIndex}");
                return null;
            }
        }
        else
        {
            Debug.LogWarning($"키 '{id}'가 talkData에 존재하지 않습니다.");
            return null;
        }
    }

    public string GetName(int id, int NameIndex)
    {
        // 키가 존재하는지 확인
        if (NameData.ContainsKey(id))
        {
            if (NameIndex < NameData[id].Length)
            {
                return NameData[id][NameIndex];
            }
            else
            {
                Debug.LogWarning($"키 '{id}'에 대한 유효하지 않은 이름 인덱스: {NameIndex}");
                return null;
            }
        }
        else
        {
            Debug.LogWarning($"키 '{id}'가 NameData에 존재하지 않습니다.");
            return null;
        }
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        int key = id + portraitIndex;

        // 키가 존재하는지 확인
        if (portraitData.ContainsKey(key))
        {
            Sprite portrait = portraitData[key];

            // 조건에 따라 UI를 활성화
            if (id == 4000 && portraitIndex == 7)
            {
                NpcAction.Instance.NpcUnLock0();
                //npc 활성화 함수
            }
            else if (id == 5000 && portraitIndex == 6)
            {
                ShowChoiceUI("따라간다", "집으로 돌아간다", (choice) =>
                {
                    if (choice == 1)
                    {
                        //따라간다
                        Buttons[0].SetActive(true);
                        // 여기에 첫 번째 선택에 따른 로직 추가
                    }
                    else if (choice == 2)
                    {
                        //안따라감
                        Changemap.Go_3_Endpart0();
                        // 여기에 두 번째 선택에 따른 로직 추가
                    }
                });
            }

            return portrait;
        }
        else
        {
            Debug.LogWarning($"키 '{key}'가 portraitData에 존재하지 않습니다.");
            return null;
        }
    }

    // 선택 UI를 보이게 하고, 버튼에 텍스트와 콜백을 설정
    void ShowChoiceUI(string choice1Text, string choice2Text, Action<int> callback)
    {
        if (choiceUI != null)
        {
            choiceButton1Text.text = choice1Text;
            choiceButton2Text.text = choice2Text;

            onChoiceMade = callback;

            choiceButton1.onClick.RemoveAllListeners();
            choiceButton2.onClick.RemoveAllListeners();

            choiceButton1.onClick.AddListener(() => OnChoiceButtonClicked(1));
            choiceButton2.onClick.AddListener(() => OnChoiceButtonClicked(2));

            choiceUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Choice UI is not assigned!");
        }
    }

    void HideChoiceUI()
    {
        if (choiceUI != null)
        {
            choiceUI.SetActive(false);
        }
    }

    void OnChoiceButtonClicked(int choice)
    {
        HideChoiceUI();

        if (onChoiceMade != null)
        {
            onChoiceMade(choice);
        }
    }
}
