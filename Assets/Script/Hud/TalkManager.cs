using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TalkManager : MonoBehaviour
{
    public GameObject choiceUI;  // ���� UI �г�
    public Button choiceButton1; // ù ��° ��ư
    public Button choiceButton2; // �� ��° ��ư
    public TextMeshProUGUI choiceButton1Text; // ù ��° ��ư�� �ؽ�Ʈ
    public TextMeshProUGUI choiceButton2Text; // �� ��° ��ư�� �ؽ�Ʈ

    private Action<int> onChoiceMade;  // ��ư Ŭ�� �� ����� �ݹ� �Լ�

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
        talkData.Add(1000, new string[] { "�ȳ��ϽŰ�...:0", "�� �̻��� ������ �� ����:1" });
        NameData.Add(1000, new string[] { "����&0", "����&1" });

        talkData.Add(2000, new string[] { "�� ����?!:0", "���� �Ժη� ������ ���� �ƴ϶��!!:1", "����!!:2" });
        NameData.Add(2000, new string[] { "����&0", "����&1", "����&2" });

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

        // Ư�� ������ �����ϸ� UI�� Ȱ��ȭ
        if (id == 2000 && portraitIndex == 2)
        {
            ShowChoiceUI("������ 1", "������ 2", (choice) =>
            {
                if (choice == 1)
                {
                    Debug.Log("ù ��° ������ ���õ�");
                }
                else if (choice == 2)
                {
                    Debug.Log("�� ��° ������ ���õ�");
                }
            });
        }
        // id�� 1000�� ���� ���� �߰�
        else if (id == 1000 && portraitIndex == 1)
        {
            ShowChoiceUI("�ٸ� ������ 1", "�ٸ� ������ 2", (choice) =>
            {
                if (choice == 1)
                {
                    Debug.Log("id�� 1000�� �� ù ��° ������ ���õ�");
                    // ���⿡ ù ��° ���ÿ� ���� ���� �߰�
                }
                else if (choice == 2)
                {
                    Debug.Log("id�� 1000�� �� �� ��° ������ ���õ�");
                    // ���⿡ �� ��° ���ÿ� ���� ���� �߰�
                }
            });
        }

        return portrait;
    }

    // ���� UI�� ���̰� �ϰ�, ��ư�� �ؽ�Ʈ�� �ݹ��� ����
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
