using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        NameData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕하신가...:0", "이 이상은 지나갈 수 없네:1" });
        NameData.Add(1000, new string[] { "병구&0", "병구&1" });

        talkData.Add(2000, new string[] { "넌 뭐야?!:0", "여긴 함부로 들어오는 곳이 아니라고!!:1", "꺼져!!:2" });
        NameData.Add(2000, new string[] { "수린&0", "수린&1", "병준&2" });

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public string GetName(int id, int NameIndex)
    {
        if (NameIndex == NameData[id].Length)
            return null;
        else
            return NameData[id][NameIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        Sprite portrait = portraitData[id + portraitIndex];

        // 특정 조건을 만족하면 UI를 활성화
        if (id == 2000 && portraitIndex == 2)
        {
            ShowChoiceUI("선택지 1", "선택지 2", (choice) =>
            {
                if (choice == 1)
                {
                    Debug.Log("첫 번째 선택지 선택됨");
                }
                else if (choice == 2)
                {
                    Debug.Log("두 번째 선택지 선택됨");
                }
            });
        }
        // id가 1000일 때의 조건 추가
        else if (id == 1000 && portraitIndex == 1)
        {
            ShowChoiceUI("다른 선택지 1", "다른 선택지 2", (choice) =>
            {
                if (choice == 1)
                {
                    Debug.Log("id가 1000일 때 첫 번째 선택지 선택됨");
                    // 여기에 첫 번째 선택에 따른 로직 추가
                }
                else if (choice == 2)
                {
                    Debug.Log("id가 1000일 때 두 번째 선택지 선택됨");
                    // 여기에 두 번째 선택에 따른 로직 추가
                }
            });
        }

        return portrait;
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
