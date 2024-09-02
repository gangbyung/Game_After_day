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

    public static TalkManager instance { get; private set; }

    Changemap Chmap;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
    }
    void GenerateData()
    {
        //NPC 1 ��ĸ���ܸ� ���
        talkData.Add(1000, new string[] { "����, ������ ��å���Դ��ϸ� ��� ������ �̸���̳� ...:0", "�����ҿ��� ���� ���� ���ĺ��� �ϴ��� ��г��ڰ� �Ա����� �������� ����:1", "�׷��� ���� �Ѱ�, ������ ���⼭ ���Ѵٰ� �߾��� �� ������, ���� �˾�?:2" , "�Ƹ� ������ �����̶� ������ ���� �ſ���.:3", "�׷�? �׷� �� �׷��� ū �ϵ� �ƴϱ���.:4" , "��, ���������� �帶ö�̱⵵ �߱���,:5", "'...��Ȳ�� ��� �ǰ� �ִ����� ���� ȣ��� ���� ������ �ٹ濡 ����Ǹ� ū���� ���ٵ�':6" });
        NameData.Add(1000, new string[] { "��ĸ���ܸ�&0", "��ĸ���ܸ�&1", "��ĸ���ܸ�&2", "���ΰ�&3", "��ĸ���ܸ�&4", "��ĸ���ܸ�&5", "���ΰ�&6" });

        //NPC 2 û�� ���
        talkData.Add(2000, new string[] { "1��...1���̶��..:0", "��, ���������� ���� ���ϵ����.:1", "�� ����..:2","�������� 3�￡ �ŷ��Ǵ� ������...����!!!!:3" ,"'...�����ϰ� ��������':4"});
        NameData.Add(2000, new string[] { "û��&0", "���ΰ�&1", "û��&2", "û��&3", "���ΰ�&4" });

        //NPC 3 ���ϰ��� ���ܸ�
        talkData.Add(3000, new string[] { "���� ��� �������� ���� ������ �ϵ鸸 ���´� ������.:0", "�ٷ� ���������� �� ���� ����뿡�� �������� �Ͼ�ٰ� �ϴ���, �ٷ� ������ ���� ���� ������ �����ߴ� ���� �ʳ�:1", "�Ͽ����� ���� �� �����ϴ� ������...:2", "�׷��� ������ �� ���?:3", "�� �׷�...:4","��� �ϳ��� �ּ���.:5" });
        NameData.Add(3000, new string[] { "���ϰ��� ���ܸ�&0", "���ϰ��� ���ܸ�&1", "���ϰ��� ���ܸ�&2", "���ϰ��� ���ܸ�&3", "���ΰ�&4","���ΰ�&5" });

        //NPC 4 ����
        talkData.Add(4000, new string[] { "�����! ��ø���:0", "��?:1", "���� �ܺ��� ���� �����ε� ���..?:2", "Ȥ�� �̹��� ���ñ���� �� �������̽Ű���?:3", "�װ� �ƴѵ���.:4", "����..�̰����� �ܺ��� ���� ������ �� �̻� ������ ���ø� �ȵ˴ϴ�.:5" });
        NameData.Add(4000, new string[] { "???&0", "���ΰ�&1", "����&2", "����&3", "���ΰ�&4", "����&5" });
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

        //NPC 4 ����
        portraitData.Add(4000 + 0, portraitArr[4]);
        portraitData.Add(4000 + 1, player_portraitArr[0]);
        portraitData.Add(4000 + 2, portraitArr[4]);
        portraitData.Add(4000 + 3, portraitArr[4]);
        portraitData.Add(4000 + 4, player_portraitArr[0]);
        portraitData.Add(4000 + 5, portraitArr[4]);

        //NPC 5 �Ĺ�

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
        if (id == 4000 && portraitIndex == 5)
        {
            Debug.Log("�� �㲲�ĤĤĤĤĤĤĤĤĤĤĤĤĤĤĤĤĤĤĤ�");
            ShowChoiceUI("������ 1", "������ 2", (choice) =>
            {
                if (choice == 1)
                {
                    Debug.Log("ù ��° ������ ���õ�");
                    SceneManager.LoadScene("3.Endpart0");
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
