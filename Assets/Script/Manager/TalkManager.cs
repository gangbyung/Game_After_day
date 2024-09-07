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

    public GameObject MiniGamePanel;

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
        //NpcAction a = FindObjectOfType<NpcAction>();
    }
    void GenerateData()
    {
        //NPC 1 ��ĸ���ܸ� ���
        talkData.Add(1000, new string[] {
            "����, ������ ��å���Դ��ϸ� ��� ������ �̸���̳� ...:0",
            "�����ҿ��� ���� ���� ���ĺ��� �ϴ��� ��г��ڰ� �Ա����� �������� ����:1",
            "�׷��� ���� �Ѱ�, ������ ���⼭ ���Ѵٰ� �߾��� �� ������, ���� �˾�?:2" ,
            "�Ƹ� ������ �����̶� ������ ���� �ſ���.:3",
            "�׷�? �׷� �� �׷��� ū �ϵ� �ƴϱ���.:4" ,
            "��, ���������� �帶ö�̱⵵ �߱���,:5",
            "'...��Ȳ�� ��� �ǰ� �ִ����� ���� ȣ��� ���� ������ �ٹ濡 ����Ǹ� ū���� ���ٵ�':6"});
        NameData.Add(1000, new string[] { "��ĸ���ܸ�&0", "��ĸ���ܸ�&1", "��ĸ���ܸ�&2", "���ΰ�&3", "��ĸ���ܸ�&4", "��ĸ���ܸ�&5", "���ΰ�&6" });

        //NPC 2 û�� ���
        talkData.Add(2000, new string[] {
            "1��...1���̶��..:0",
            "��, ���������� ���� ���ϵ����.:1",
            "�� ����..:2",
            "�������� 3�￡ �ŷ��Ǵ� ������...����!!!!:3" ,
            "'...�����ϰ� ��������':4"});
        NameData.Add(2000, new string[] { "û��&0", "���ΰ�&1", "û��&2", "û��&3", "���ΰ�&4" });

        //NPC 3 ���ϰ��� ���ܸ�
        talkData.Add(3000, new string[] {
            "���� ��� �������� ���� ������ �ϵ鸸 ���´� ������.:0",
            "�ٷ� ���������� �� ���� ����뿡�� �������� �Ͼ�ٰ� �ϴ���, �ٷ� ������ ���� ���� ������ �����ߴ� ���� �ʳ�:1",
            "�Ͽ����� ���� �� �����ϴ� ������...:2",
            "�׷��� ������ �� ���?:3",
            "�� �׷�...:4",
            "��� �ϳ��� �ּ���.:5" });
        NameData.Add(3000, new string[] { "���ϰ��� ���ܸ�&0", "���ϰ��� ���ܸ�&1", "���ϰ��� ���ܸ�&2", "���ϰ��� ���ܸ�&3", "���ΰ�&4", "���ΰ�&5" });

        //NPC 4 ����
        talkData.Add(4000, new string[] {
            "�����! ��ø���:0",
            "��?:1",
            "���� �ܺ��� ���� �����ε� ���..?:2",
            "Ȥ�� �̹��� ���ñ���� �� �������̽Ű���?:3",
            "�װ� �ƴѵ���..:4",
            "����..�̰����� �ܺ��� ���� ������ �� �̻� ������ ���ø� �ȵ˴ϴ�.:5",
            "'�׷� ���ư����Ϸ���...':6",
            "�ǰ�.�ǰ�. (������ �����ִ�):7" });
        NameData.Add(4000, new string[] { "???&0", "���ΰ�&1", "����&2", "����&3", "���ΰ�&4", "����&5", "���ΰ�&6", "&7" });

        //NPC 5 �Ĺ�
        talkData.Add(5000, new string[] {
            "��? ����� ��������? �������̳׿�! ���� ���������� ������ ����� ���� ������� ���� �� ������ ���ϱ�...:0",
            "1���� ������ �� ������! �� Ȥ�� ����� ����ǽŰǰ���?:1",
            "��? �װǾƴϰ� �ȴٺ��� ���ԵƳ�:2",
            "�� �׷����� ��� �߱� �ϼ��� �ϳ� �����ϰŵ��.. �����Ҹ� �������̸� ���ۿ� �������� �о�־ ������ �ذ�Ǿ����ٵ�..:3",
            "�����̸� �� ������ 1�������� ���￡ �������� �Ͼ�� �ٶ���...:4",
            "..ŭ �⼳�� ������� ����:5",
            "�ƹ�ư �̰͵� �ο��ε�, �� �� �����̶� �ϽǷ���??:6" });
        NameData.Add(5000, new string[] { "�Ĺ�&0", "�Ĺ�&1", "���ΰ�&2", "�Ĺ�&3", "�Ĺ�&4", "�Ĺ�&5", "�Ĺ�&6" });
        //---------------------------------------------------------------------------------------------------------------------------------
        //NPC 6 �����1
        talkData.Add(6000, new string[]{
            "�����̿���. �� �ѷ����ôٰ� ������:0"
        });
        NameData.Add(6000, new string[] { "�����1&0" });

        //NPC 4 �Ĺ� (������/ �밭 �� �ѷ��� �� ����)
        talkData.Add(7000, new string[] {
            "�� �ѷ����̳���?:0"
        });
        NameData.Add(7000, new string[] { "�Ĺ�&0" });

        //NPC 6 �����1
        talkData.Add(8000, new string[] {
            "������ ���ε�...:0",
            "�ƴѰ�? �ƴϸ� �̾��ϰ�, ���� ���� 3�ϰ� ���� ���ڼ� ���� ������ �� ���������ϰŵ�? ���� �� ����.:1",
            "����...:2",
            "�ƹ�ư, �� �� ������ ���� �ִ� ���� �� �� �������� �ǳ��ٷ�? ����, �Ӹ��� ���� �����ִ� ��� ���̾�.:3",
            "��?:4",
            " �� ���⼭ ���ϴ� ��� �ƴѵ�...:4",
            "(����� ���� ���ϰ� å�� ���� ��������)..zzZ:5"
        });
        NameData.Add(8000, new string[] { "�����1&0", "�����1&1", "���ΰ�&2", "�����1&3", "���ΰ�&4", "���ΰ�&5", "�����&6" });

        // NPC 7 �����2
        talkData.Add(9000, new string[] {
            "�� ����� �����Ű���?:0",
            "��? ��, ��. �����̴ϴ�. ����������?:1",
            "���� ��ô� ���� �̰� �� ������ �帮�� �ϼż���:2",
            "����... �װ� å�� ������ ���� ���ֽø� �˴ϴ�.:3",
            "(���̴��̸� ���̺� ���� ��):4"
        });
        NameData.Add(9000, new string[] { "���ΰ�&0", "�����2&1", "���ΰ�&2", "�����2&3", "���ΰ�&4" });

        // NPC 7 �����2
        talkData.Add(10000, new string[] {
            "...:0",
            "... Ȥ�� �ٸ� ����� �����Ű���?:1",
            "��Ȳ�� ��� �Ǿ�� �ִ��� �ñ��ؼ���.:2",
            "��... ��Ȳ, ���� ���� �ʾƿ�. �ƴ�, ���� �־ǿ� ������.:3",
            "���������� �ٽ��� ���� ������ �����ϴٰ� �ϴ���:4",
            "�۽��, ������ �е��� �ϴ� ���� ��������?:5",
            "���ʿ�, �̰� �ܼ� �ҷ� ������ �Ͼ ���� �ƴ϶���,:6",
            "����ȭ �� ���� �� ��κ��� �׷��� Ĩ�ô�. ��ǰ�� �����ϴ� ��ǰ��ü�� ������ �涥�ļ� �ҷ�ǰ���� ��ǰ�ϰ� ���δ� ����ؼ� �� ���� ������ �����ϱ� ���߰�, �� ��� ���������� ���� ��踦 ����� ������.:7",
            "�ƽôٽ��� ���ڷ� ������ �����ϰ�, ��� ������ ���� ��� �Ǵ����� �ٵ� �� �˱� ������ ������ Ȥ�� �� ��Ȳ�� ����� ��ġ�� �۵����� ���� ��츦 ����� �͵� �ȵ� ���� �����... �°� ��ȣ��ġ�� �ɾ����.:8",
            "�׷� ��°�� ���� ���ϴ°ž�?!:9",
            "��ġ�� ������ ���մϱ�! ��κ��� �νĵǰ� ���峪�� �۵��غ��� ����� ������ ���ؿ�!:10",
            "�ƹ�ư �׳��� ������ ��ġ�� ã�Ƴ��� �� ���ظ� �ּ�ȭ ��Ű�� ���� ������ ��ǥ�Դϴ�.:11"
        });
        NameData.Add(10000, new string[] { "���ΰ�&0", "�����2&1", "���ΰ�&2", "�����2&3", "���ΰ�&4", "�����2&5", "�����2&6", "�����2&7", "�����2&8", "���ΰ�&9", "�����2&10", "�����11&11" });

        //NPC 8 �����3
        talkData.Add(11000, new string[] {
            "�Ͽ�����... ���� ���� �αͿ�ȭ�� �������� ���ڿ��� ���� �������...:0",
            "... ��, ���� ���̽���?:1",
            "(������ �帵ũ �ǳ��ֱ�):2",
            "�̰�... ����, �����մϴ�.:3",
            "(�ܲ�.�ܲ�.) ũ��, ������ �� �� ����~:4",
            "���� �����Ű����׿�.:5",
            "����, �� ���� �󸶳� ��������!:6",
            "��..�̰� �� �� ������ ��������.:7",
            "�ٸ� �ڿ����� Ȱ���� �����Ҵ� �� �������� ���ڷ� �����Һ��� ���� ȿ�����̰� ���� ������� ������� ��ư��µ� �ʿ�� �ϴ� �ּ����� �������� ������ �� �� �ִ� ȹ������ ��ȵ� ����:8",
            "���ڷ� �����Ҵ� �����ϰ� ���ߵ� ����������, ��ȣ���� �������� ���ɵ��� �������̷� �����״ϱ��.:9",
            "�׷� ��Ȳ������ ��κ��� ���ڷ� �����Ҵ� �̹� ������ ������ ���� �Ѱܼ� �������� �����ϴ� ���� ����, ��ǰ�� ���� �̹� �νĵǰ� ����ȭ�Ǿ� ��ü�� �ʿ��� �͵��� �״�� �������α⸸ ����.:10",
            "�׸��� ���ݱ��� �ƹ��� �ϵ� ������ �ʾ����� ���ݱ��� �ϴ� ��� �ϴ��� ū ���� �������� ���� ���̴�... ��� ���ϸ鼭��.:11",
            "..�� ��� ���� �� ���� ���׿�. �и� �� ���� ��ݿ��� ��ƿ �� �ְ� ������ ��� �������� ���۽ǵ� ���� �ҿ� �����.:12",
            "���ʿ�, ���� �����ϴٸ� ���� ��� �����ϰ� ���￡ ��ʰ��� ���� �ʰھ��? �׷��� ��ȣ�ϴ� ��� ġ�� �� ���� ������ ���� �ƴϸ� �ڱ� ��� ������ ���ڰ� ����ϰ� ���� �� �ִ� ���� �� ��������.:13",
            "�ڱ�鵵 �밭 �˰� �����鼭 �𸣼� �ϴ°���. �׸��� �� ����� ���� ��� �ϰ� �ֳ׿�...:14",
            "����� �����ó׿�..:15",
        });
        NameData.Add(11000, new string[] { "�����3&0", "���ΰ�&1", "���ΰ�&2", "�����&3", "�����&4", "���ΰ�&5", "�����3&6", "�����3&7", "�����3&8", "�����3&9", "�����3&10", "�����3&11", "�����3&12", "�����3&13", "�����3&14", "���ΰ�&15" });

        //NPC 4 �Ĺ� �ٽ� �� �� ��, �� �������� ��� �ؾ��� �� ���� �׳� �̷��� �߾� �ʰ� �˾Ƽ� ��������.
        talkData.Add(12000, new string[]
        {
            "�� �ѷ����̳���?:0",
            "������:1",
            "�밭 �� �ѷ��� �� ����:2"
        });
        NameData.Add(12000, new string[] { "�Ĺ�&0", "���ΰ�&1","���ΰ�&2"});

        // NPC 4 �Ĺ�(2�� ��Ʈ)
        talkData.Add(13000, new string[] {
            "... �׷�����.:0",
            "�� ��¿ �� ����. �̷� ���� ���ڴ�� ������ ���� ���� �븩�̱⵵ �ϰ��.:1",
            "�� ���� ���񳢸��� ��Ե� �غ�����, ��...:2",
            "(����� ħ��):3",
            "ũ��... �ȳ��� ���ʼ�:4"
        });
        NameData.Add(13000, new string[] { "�Ĺ�&0", "�Ĺ�&1", "�Ĺ�&2", "�Ĺ�&3", "�Ĺ�&4" });

        // NPC 4 �Ĺ�(1��)
        talkData.Add(14000, new string[]{
            "�����Դϱ�...?! �������� �亯�� ���� ���̶� �������� ���߽��ϴ�.:0",
            "�׷� ���¿� ���� ���� ���� ��� ���صΰڽ��ϴ�.:1",
            "... �����մϴ�. ���� ���� ������ �����ּż�:2",
            "�����ϸ� ������ ���̳� �ѹ� ��:3",
            "����, ���� ������ �Ǹ� �׷��� ����:4",
            "��ħ ���� ��⸦ �Ⱑ������ �����ִ� ������ ã�Ƴ� ���̰ŵ��.:5",
        });
        NameData.Add(14000, new string[] { "�Ĺ�&0", "�Ĺ�&1", "�Ĺ�&2", "���ΰ�&3", "�Ĺ�&4", "�Ĺ�&5" });

        //NPC 4 �Ĺ�
        talkData.Add(15000, new string[]
        {
            "���̽��ϱ� �����!:0",
            "�켱 ���� �� �ϸ� �������� ���� ��������.:1",
            "�� �װſ�? �ϴ� ���� ��Ȳ�� ���� ��� ������...:2",
            "(���̺� ���� �������̷� �÷��� �������� ���� �ٶ󺻴�)��...:3",
            "������... ����� ���׿�, ������ �ð��� ���:4",
            "ã�� �迡 �����ϴ� �� ���ڳ�.:5"
        });
        NameData.Add(15000, new string[] { "�Ĺ�&0", "���ΰ�&1", "�Ĺ�&2", "���ΰ�&3", "�Ĺ�&4", "���ΰ�&5" });

        //NPC 4�Ĺ�
        talkData.Add(16000, new string[] {
            "�����ּż� �����մϴ�. �׷� �����...:0",
            "��, ã�Ҵ�! ���� �־��.:1",
            "... �̰� ������ �� ������ �� ������:2",
            "��... ������� �̹��ֿ� ��������� ��Ž���� ��ġ�� �׿� ���� ������ �� �����̾��µ� ������,:3",
            "... �̹��� �ü��� Ȯ���Ϸ� �� ��������� ���⵵ ���� ���� ���� ��� ���޽ü��� �Ƿ����ŵ��.���� �λ����� ������� ��������� �Ͽ� ���ؼ� ���Ҽ��� ���� �븩������... ��, �׷��� �Ǿ��׿�.:4",
            "����:5",
            "�׷� ��� Ȯ���� �ؾ� �ϴ°���?:6",
            "�� �� �ֳ���. �����ϰ� ���ο��� �� �η��� �İ��� �ְų� �� �е��� �λ��� �� ���� ������ ��ٸ� ���� ���� �븩�̰�, ����� ���� �� �ۿ���.:7",
            "���� Ȯ���� ���ϸ� �� ��� �� �� �𸣴� ��Ȳ�̴ϱ��.:8",
            "�׷� ���� �ѹ� ������.:9",
            "�������� �����ðھ��?:10",
            "... ���⿡�� ������ Ž����� �� ��ŭ�� �ð� ������ �ִ� ����� ���� ������.:11",
            "����... ����� ������ ���� �� ������ �ܺ� ��ȣ�� ������ ������ �ٱ����θ� ������� �ʾ��� ��, ������ ��� ���� �Ǹ����� �� �ʸ� �ӹ����ٸ� �� �е� ä ����Ƽ�� �幰�幰 �����ſ���.:12",
            "�Դٰ� ���� �����Ǵ� �ü� ���� ���� ��ġ�� ���ڸ� ����� �� ��� �����ϰ� ���� �ϴ��� 100% �Ϻ��ϰ� ��ȣ�� �Ұ����ؿ�.:13",
            "���� �ӹ��ٸ�... ��, ������ ������ �� ���� �ް� �ɰſ���. �Ǻο��� ü������ ������ ������ �� �ִ°� ���̰��.:14",
            "�׷����� ������ ���� �� ��������?:15",
            "�غ��� ��.:16",
            "... ����Ͻó׿�.:17",
            "�׷� Ž���� �е��� ����Ͻô� ��� â����� �ȳ��� �帱�Կ�.:18",
            "���� �㰡�� ���ۿ� �޾Ƴ��ŵ��.:19",
        });
        NameData.Add(16000, new string[] { "�Ĺ�&0", "�Ĺ�&1", "���ΰ�&2", "�Ĺ�&3", "�ĺ�&4", "���ΰ�&5", "���ΰ�&6", "�Ĺ�&7", "�Ĺ�&8", "���ΰ�&9", "�Ĺ�&10", "���ΰ�&11", "�Ĺ�&12", "�Ĺ�&13", "�Ĺ�&14", "�Ĺ�&15", "�Ĺ�&16", "�Ĺ�&17", "�Ĺ�&18", "�Ĺ�&19" });

        //NPC 4 �Ĺ�
        talkData.Add(17000, new string[] {
            "���Ⱑ â����.:0",
            "������ ���� ������ ì�ܵаǵ�... ������� ���Ŵ� �Ͻ� ���� �����׿�.:1",
            "���������� ������ ���� ü���� ����Ͻô� �Ƹ� �����ǰſ���.:2",
            //�ٱ��� ��ȣ�ۿ� �� ������ ȹ��
        });
        NameData.Add(17000, new string[] { "�Ĺ�&0", "�Ĺ�&1", "�Ĺ�&2" });

        //NPC 4 �Ĺ�
        talkData.Add(18000, new string[] {
            "������ �ٳ���ʼ�:0"
        });
        NameData.Add(18000, new string[] { "�Ĺ�&0" });

        //NPC 6 �����1
        talkData.Add(19000, new string[] {
            "��ĥ�� �ȵż� �̷� ���� �ñ�� �Ǿ� �̾��ϱ���.:0"
        });
        NameData.Add(19000, new string[] { "�����1&0" });

        //NPC 7 �����2
        talkData.Add(20000, new string[] { "���� �����ʴϴ�. �� �ٳ���ʼ�.:0" });
        NameData.Add(20000, new string[] { "�����2:0" });

        //NPC 8 �����3
        talkData.Add(21000, new string[] { "Ž�簡 �� �����̱� ������ ���� �߿��� �� �ǰ��Դϴ�.:0" });
        NameData.Add(21000, new string[] { "�����3&0" });
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
        portraitData.Add(4000 + 6, player_portraitArr[0]);
        portraitData.Add(4000 + 7, portraitArr[0]);

        //NPC 5 �Ĺ�
        portraitData.Add(5000 + 0, portraitArr[5]);
        portraitData.Add(5000 + 1, portraitArr[5]);
        portraitData.Add(5000 + 2, player_portraitArr[0]);
        portraitData.Add(5000 + 3, portraitArr[5]);
        portraitData.Add(5000 + 4, portraitArr[5]);
        portraitData.Add(5000 + 5, portraitArr[5]);
        portraitData.Add(5000 + 6, portraitArr[5]);

        //NPC 6 ����� 1
        portraitData.Add(6000 + 0, portraitArr[6]);

        //NPC 4 �Ĺ�
        portraitData.Add(7000 + 0, portraitArr[4]);

        //NPC 6 ����� 1
        portraitData.Add(8000 + 0, portraitArr[6]);
        portraitData.Add(8000 + 1, portraitArr[6]);
        portraitData.Add(8000 + 2, portraitArr[6]);
        portraitData.Add(8000 + 3, portraitArr[6]);
        portraitData.Add(8000 + 4, portraitArr[6]);
        portraitData.Add(8000 + 5, portraitArr[6]);
        portraitData.Add(8000 + 6, portraitArr[6]);

        //NPC 7 ����� 2
        portraitData.Add(9000 + 0, portraitArr[7]);
        portraitData.Add(9000 + 1, portraitArr[7]);
        portraitData.Add(9000 + 2, portraitArr[7]);
        portraitData.Add(9000 + 3, portraitArr[7]);
        portraitData.Add(9000 + 4, portraitArr[7]);

        //NPC 7 ����� 2
        portraitData.Add(10000 + 0, portraitArr[7]);
        portraitData.Add(10000 + 1, portraitArr[7]);
        portraitData.Add(10000 + 2, portraitArr[7]);
        portraitData.Add(10000 + 3, portraitArr[7]);
        portraitData.Add(10000 + 4, portraitArr[7]);
        portraitData.Add(10000 + 5, portraitArr[7]);
        portraitData.Add(10000 + 6, portraitArr[7]);
        portraitData.Add(10000 + 7, portraitArr[7]);
        portraitData.Add(10000 + 8, portraitArr[7]);
        portraitData.Add(10000 + 9, portraitArr[7]);
        portraitData.Add(10000 + 10, portraitArr[7]);
        portraitData.Add(10000 + 11, portraitArr[7]);

        //NPC 8 ����� 3
        portraitData.Add(11000 + 0, portraitArr[8]);
        portraitData.Add(11000 + 1, portraitArr[8]);
        portraitData.Add(11000 + 2, portraitArr[8]);
        portraitData.Add(11000 + 3, portraitArr[8]);
        portraitData.Add(11000 + 4, portraitArr[8]);
        portraitData.Add(11000 + 5, portraitArr[8]);
        portraitData.Add(11000 + 6, portraitArr[8]);
        portraitData.Add(11000 + 7, portraitArr[8]);
        portraitData.Add(11000 + 8, portraitArr[8]);
        portraitData.Add(11000 + 9, portraitArr[8]);
        portraitData.Add(11000 + 10, portraitArr[8]);
        portraitData.Add(11000 + 11, portraitArr[8]);
        portraitData.Add(11000 + 12, portraitArr[8]);
        portraitData.Add(11000 + 13, portraitArr[8]);
        portraitData.Add(11000 + 14, portraitArr[8]);
        portraitData.Add(11000 + 15, portraitArr[8]);

        //NPC 4 �Ĺ�
        portraitData.Add(12000 + 0, portraitArr[4]);
        portraitData.Add(12000 + 1, portraitArr[4]);
        portraitData.Add(12000 + 2, portraitArr[4]);


        //NPC 4 �Ĺ� 
        portraitData.Add(13000 + 0, portraitArr[4]);
        portraitData.Add(13000 + 1, portraitArr[4]);
        portraitData.Add(13000 + 2, portraitArr[4]);
        portraitData.Add(13000 + 3, portraitArr[4]);
        portraitData.Add(13000 + 4, portraitArr[4]);

        //NPC 4 �Ĺ� (1��)
        portraitData.Add(14000 + 0, portraitArr[4]);
        portraitData.Add(14000 + 1, portraitArr[4]);
        portraitData.Add(14000 + 2, portraitArr[4]);
        portraitData.Add(14000 + 3, portraitArr[4]);
        portraitData.Add(14000 + 4, portraitArr[4]);
        portraitData.Add(14000 + 5, portraitArr[4]);

        //NPC 4 �Ĺ�
        portraitData.Add(15000 + 0, portraitArr[4]);
        portraitData.Add(15000 + 1, portraitArr[4]);
        portraitData.Add(15000 + 2, portraitArr[4]);
        portraitData.Add(15000 + 3, portraitArr[4]);
        portraitData.Add(15000 + 4, portraitArr[4]);
        portraitData.Add(15000 + 5, portraitArr[4]);

        //NPC 4 �Ĺ�
        portraitData.Add(16000 + 0, portraitArr[4]);
        portraitData.Add(16000 + 1, portraitArr[4]);
        portraitData.Add(16000 + 2, portraitArr[4]);
        portraitData.Add(16000 + 3, portraitArr[4]);
        portraitData.Add(16000 + 4, portraitArr[4]);
        portraitData.Add(16000 + 5, portraitArr[4]);
        portraitData.Add(16000 + 6, portraitArr[4]);
        portraitData.Add(16000 + 7, portraitArr[4]);
        portraitData.Add(16000 + 8, portraitArr[4]);
        portraitData.Add(16000 + 9, portraitArr[4]);
        portraitData.Add(16000 + 10, portraitArr[4]);
        portraitData.Add(16000 + 11, portraitArr[4]);
        portraitData.Add(16000 + 12, portraitArr[4]);
        portraitData.Add(16000 + 13, portraitArr[4]);
        portraitData.Add(16000 + 14, portraitArr[4]);
        portraitData.Add(16000 + 15, portraitArr[4]);
        portraitData.Add(16000 + 16, portraitArr[4]);
        portraitData.Add(16000 + 17, portraitArr[4]);
        portraitData.Add(16000 + 18, portraitArr[4]);
        portraitData.Add(16000 + 19, portraitArr[4]);

        //NPC 4�Ĺ�
        portraitData.Add(17000 + 0, portraitArr[4]);
        portraitData.Add(17000 + 1, portraitArr[4]);
        portraitData.Add(17000 + 2, portraitArr[4]);

        //NPC 4�Ĺ�
        portraitData.Add(18000 + 0, portraitArr[4]);

        //NPC 6 ����� 1
        portraitData.Add(19000 + 0, portraitArr[6]);

        //NPC 7 ����� 2
        portraitData.Add(20000 + 0, portraitArr[7]);

        //NPC 8 ����� 3
        portraitData.Add(21000 + 0, portraitArr[8]);

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
                        //MiniGamePanel.SetActive(true);
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
