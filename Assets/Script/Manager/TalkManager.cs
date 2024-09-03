using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
        //NPC 1 ��ĸ���ܸ� ���
        talkData.Add(1000, new string[] { "����, ������ ��å���Դ��ϸ� ��� ������ �̸���̳� ...:0", "�����ҿ��� ���� ���� ���ĺ��� �ϴ��� ��г��ڰ� �Ա����� �������� ����:1", "�׷��� ���� �Ѱ�, ������ ���⼭ ���Ѵٰ� �߾��� �� ������, ���� �˾�?:2" , "�Ƹ� ������ �����̶� ������ ���� �ſ���.:3", "�׷�? �׷� �� �׷��� ū �ϵ� �ƴϱ���.:4" , "��, ���������� �帶ö�̱⵵ �߱���,:5", "'...��Ȳ�� ��� �ǰ� �ִ����� ���� ȣ��� ���� ������ �ٹ濡 ����Ǹ� ū���� ���ٵ�':6"});
        NameData.Add(1000, new string[] { "��ĸ���ܸ�&0", "��ĸ���ܸ�&1", "��ĸ���ܸ�&2", "���ΰ�&3", "��ĸ���ܸ�&4", "��ĸ���ܸ�&5", "���ΰ�&6"});

        //NPC 2 û�� ���
        talkData.Add(2000, new string[] { "1��...1���̶��..:0", "��, ���������� ���� ���ϵ����.:1", "�� ����..:2","�������� 3�￡ �ŷ��Ǵ� ������...����!!!!:3" ,"'...�����ϰ� ��������':4"});
        NameData.Add(2000, new string[] { "û��&0", "���ΰ�&1", "û��&2", "û��&3", "���ΰ�&4" });

        //NPC 3 ���ϰ��� ���ܸ�
        talkData.Add(3000, new string[] { "���� ��� �������� ���� ������ �ϵ鸸 ���´� ������.:0", "�ٷ� ���������� �� ���� ����뿡�� �������� �Ͼ�ٰ� �ϴ���, �ٷ� ������ ���� ���� ������ �����ߴ� ���� �ʳ�:1", "�Ͽ����� ���� �� �����ϴ� ������...:2", "�׷��� ������ �� ���?:3", "�� �׷�...:4","��� �ϳ��� �ּ���.:5" });
        NameData.Add(3000, new string[] { "���ϰ��� ���ܸ�&0", "���ϰ��� ���ܸ�&1", "���ϰ��� ���ܸ�&2", "���ϰ��� ���ܸ�&3", "���ΰ�&4","���ΰ�&5" });

        //NPC 4 ����
        talkData.Add(4000, new string[] { "�����! ��ø���:0", "��?:1", "���� �ܺ��� ���� �����ε� ���..?:2", "Ȥ�� �̹��� ���ñ���� �� �������̽Ű���?:3", "�װ� �ƴѵ���..:4", "����..�̰����� �ܺ��� ���� ������ �� �̻� ������ ���ø� �ȵ˴ϴ�.:5" ,"'�׷� ���ư����Ϸ���...':6","�ǰ�.�ǰ�. (������ �����ִ�):7" });
        NameData.Add(4000, new string[] { "???&0", "���ΰ�&1", "����&2", "����&3", "���ΰ�&4", "����&5","���ΰ�&6","&7" });

        //NPC 5 �Ĺ�
        talkData.Add(5000, new string[] { "��? ����� ��������? �������̳׿�! ���� ���������� ������ ����� ���� ������� ���� �� ������ ���ϱ�...:0", "1���� ������ �� ������! �� Ȥ�� ����� ����ǽŰǰ���?:1", "��? �װǾƴϰ� �ȴٺ��� ���ԵƳ�:2", "�� �׷����� ��� �߱� �ϼ��� �ϳ� �����ϰŵ��.. �����Ҹ� �������̸� ���ۿ� �������� �о�־ ������ �ذ�Ǿ����ٵ�..:3", "�����̸� �� ������ 1�������� ���￡ �������� �Ͼ�� �ٶ���...:4", "..ŭ �⼳�� ������� ����:5", "�ƹ�ư �̰͵� �ο��ε�, �� �� �����̶� �ϽǷ���??:6" });
        NameData.Add(5000, new string[] { "�Ĺ�&0", "�Ĺ�&1", "���ΰ�&2", "�Ĺ�&3", "�Ĺ�&4", "�Ĺ�&5","�Ĺ�&6" });
        //---------------------------------------------------------------------------------------------------------------------------------

        //NPC 1 ��ĸ���ܸ�
        portraitData.Add(1000 + 0, portraitArr[1]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[1]);
        portraitData.Add(1000 + 3, player_portraitArr[0]);
        portraitData.Add(1000 + 4, portraitArr[1]);
        portraitData.Add(1000 + 5, portraitArr[1]);
        portraitData.Add(1000 + 6, player_portraitArr[0]);

        //NPC 2 û��
        portraitData.Add(2000 + 0, portraitArr[2]);
        portraitData.Add(2000 + 1, player_portraitArr[0]);
        portraitData.Add(2000 + 2, portraitArr[2]);
        portraitData.Add(2000 + 3, portraitArr[2]);
        portraitData.Add(2000 + 4, player_portraitArr[0]);

        //NPC 3 ���ϰ��� ���ܸ�
        portraitData.Add(3000 + 0, portraitArr[3]);
        portraitData.Add(3000 + 1, portraitArr[3]);
        portraitData.Add(3000 + 2, portraitArr[3]);
        portraitData.Add(3000 + 3, portraitArr[3]);
        portraitData.Add(3000 + 4, player_portraitArr[0]);
        portraitData.Add(3000 + 5, player_portraitArr[0]);

        //NPC 4 ������ �Ĺ�
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
        // Ű�� �����ϴ��� Ȯ��
        if (talkData.ContainsKey(id))
        {
            if (talkIndex < talkData[id].Length)
            {
                return talkData[id][talkIndex];
            }
            else
            {
                Debug.LogWarning($"Ű '{id}'�� ���� ��ȿ���� ���� ��ȭ �ε���: {talkIndex}");
                return null;
            }
        }
        else
        {
            Debug.LogWarning($"Ű '{id}'�� talkData�� �������� �ʽ��ϴ�.");
            return null;
        }
    }

    public string GetName(int id, int NameIndex)
    {
        // Ű�� �����ϴ��� Ȯ��
        if (NameData.ContainsKey(id))
        {
            if (NameIndex < NameData[id].Length)
            {
                return NameData[id][NameIndex];
            }
            else
            {
                Debug.LogWarning($"Ű '{id}'�� ���� ��ȿ���� ���� �̸� �ε���: {NameIndex}");
                return null;
            }
        }
        else
        {
            Debug.LogWarning($"Ű '{id}'�� NameData�� �������� �ʽ��ϴ�.");
            return null;
        }
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        int key = id + portraitIndex;

        // Ű�� �����ϴ��� Ȯ��
        if (portraitData.ContainsKey(key))
        {
            Sprite portrait = portraitData[key];

            // ���ǿ� ���� UI�� Ȱ��ȭ
            if (id == 4000 && portraitIndex == 7)
            {
                NpcAction.Instance.NpcUnLock0();
                //npc Ȱ��ȭ �Լ�
            }
            else if (id == 5000 && portraitIndex == 6)
            {
                ShowChoiceUI("���󰣴�", "������ ���ư���", (choice) =>
                {
                    if (choice == 1)
                    {
                        //���󰣴�
                        Buttons[0].SetActive(true);
                        // ���⿡ ù ��° ���ÿ� ���� ���� �߰�
                    }
                    else if (choice == 2)
                    {
                        //�ȵ���
                        Changemap.Go_3_Endpart0();
                        // ���⿡ �� ��° ���ÿ� ���� ���� �߰�
                    }
                });
            }

            return portrait;
        }
        else
        {
            Debug.LogWarning($"Ű '{key}'�� portraitData�� �������� �ʽ��ϴ�.");
            return null;
        }
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
