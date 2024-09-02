using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TalkManager : MonoBehaviour
{
    public GameObject choiceUI;  // 識澱 UI 鳶確
    public Button choiceButton1; // 湛 腰属 獄動
    public Button choiceButton2; // 砧 腰属 獄動
    public TextMeshProUGUI choiceButton1Text; // 湛 腰属 獄動税 努什闘
    public TextMeshProUGUI choiceButton2Text; // 砧 腰属 獄動税 努什闘

    private Action<int> onChoiceMade;  // 獄動 適遣 獣 叔楳吃 紬拷 敗呪

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
        //NPC 1 識銚焼捜原 企紫
        talkData.Add(1000, new string[] { "嬢政, 娃幻拭 至奪蟹尽希艦幻 域紗 劾松亜 戚乞丞戚革 ...:0", "降穿社拭辞 杭亜 持延 戚板採斗 馬潅戚 奄歳蟹孜惟 股姥硯幻 亜究馬走 更醤:1", "益君壱 左艦 恥唖, 疹劾拭 煽奄辞 析廃陥壱 梅醸揮 依 旭精汽, 杭走 硝嬢?:2" , "焼原 降穿社 賑降戚櫛 尻淫精 蒸聖 暗拭推.:3", "益掘? 益軍 更 益係惟 笛 析亀 焼艦姥幻.:4" , "焼, 繕榎赤生檎 舌原旦戚奄亀 梅姥幻,:5", "'...雌伐戚 嬢胸惟 鞠壱 赤澗走澗 侯虞亀 硲酔稽 号紫管 弘霜戚 悦号拭 政窒鞠檎 笛析戚 劾度汽':6" });
        NameData.Add(1000, new string[] { "識銚焼捜原&0", "識銚焼捜原&1", "識銚焼捜原&2", "爽昔因&3", "識銚焼捜原&4", "識銚焼捜原&5", "爽昔因&6" });

        //NPC 2 短鰍 企紫
        talkData.Add(2000, new string[] { "1常...1常戚虞艦..:0", "人, 巷充析昔遭 侯虞亀 逐馬球形推.:1", "鎧 増葵..:2","嬢薦猿遭 3常拭 暗掘鞠揮 鎧増戚...生焦!!!!:3" ,"'...巷獣馬壱 走蟹亜切':4"});
        NameData.Add(2000, new string[] { "短鰍&0", "爽昔因&1", "短鰍&2", "短鰍&3", "爽昔因&4" });

        //NPC 3 引析亜惟 焼捜原
        talkData.Add(3000, new string[] { "推葬 級嬢 敢什拭辞 亀搭 照疏精 析級幻 蟹紳舘 源戚走.:0", "郊稽 推穿腰拭澗 益 辞随 嬢巨鷹拭辞 企走遭戚 析嬢概陥壱 馬希幻, 郊稽 神潅精 煽奄 因舌 旭精惟 賑降梅陥 馬走 省蟹:1", "馬食娃拭 室雌 凧 披披馬舘 源戚走...:2", "益掘辞 引析精 照 詞移?:3", "緋 益軍...:4","紫引 馬蟹幻 爽室推.:5" });
        NameData.Add(3000, new string[] { "引析亜惟 焼捜原&0", "引析亜惟 焼捜原&1", "引析亜惟 焼捜原&2", "引析亜惟 焼捜原&3", "爽昔因&4","爽昔因&5" });

        //NPC 4 井搾据
        talkData.Add(4000, new string[] { "煽奄推! 節獣幻推:0", "革?:1", "食延 須採昔 窒脊 榎走昔汽 嬢胸惟..?:2", "箸獣 戚腰拭 神獣奄稽廃 置 嘘呪還戚重亜推?:3", "益闇 焼観汽推.:4", "焼馬..戚員採渡 須採昔 窒脊 榎走虞辞 戚 戚雌 蒋生稽 亜獣檎 照桔艦陥.:5" });
        NameData.Add(4000, new string[] { "???&0", "爽昔因&1", "井搾据&2", "井搾据&3", "爽昔因&4", "井搾据&5" });
        //---------------------------------------------------------------------------------------------------------------------------------

        //NPC 1 識銚焼捜原
        portraitData.Add(1000 + 0, portraitArr[1]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[1]);
        portraitData.Add(1000 + 3, player_portraitArr[0]);
        portraitData.Add(1000 + 4, portraitArr[1]);
        portraitData.Add(1000 + 5, portraitArr[1]);
        portraitData.Add(1000 + 6, player_portraitArr[0]);

        //NPC 2 短鰍
        portraitData.Add(2000 + 0, portraitArr[2]);
        portraitData.Add(2000 + 1, player_portraitArr[0]);
        portraitData.Add(2000 + 2, portraitArr[2]);
        portraitData.Add(2000 + 3, portraitArr[2]);
        portraitData.Add(2000 + 4, player_portraitArr[0]);

        //NPC 3 引析亜惟 焼捜原
        portraitData.Add(3000 + 0, portraitArr[3]);
        portraitData.Add(3000 + 1, portraitArr[3]);
        portraitData.Add(3000 + 2, portraitArr[3]);
        portraitData.Add(3000 + 3, portraitArr[3]);
        portraitData.Add(3000 + 4, player_portraitArr[0]);
        portraitData.Add(3000 + 5, player_portraitArr[0]);

        //NPC 4 井搾据
        portraitData.Add(4000 + 0, portraitArr[4]);
        portraitData.Add(4000 + 1, player_portraitArr[0]);
        portraitData.Add(4000 + 2, portraitArr[4]);
        portraitData.Add(4000 + 3, portraitArr[4]);
        portraitData.Add(4000 + 4, player_portraitArr[0]);
        portraitData.Add(4000 + 5, portraitArr[4]);

        //NPC 5 板壕

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

        // 働舛 繕闇聖 幻膳馬檎 UI研 醗失鉢
        if (id == 4000 && portraitIndex == 5)
        {
            Debug.Log("蟹 吟臆つつつつつつつつつつつつつつつつつつつつ");
            ShowChoiceUI("識澱走 1", "識澱走 2", (choice) =>
            {
                if (choice == 1)
                {
                    Debug.Log("湛 腰属 識澱走 識澱喫");
                    SceneManager.LoadScene("3.Endpart0");
                }
                else if (choice == 2)
                {
                    Debug.Log("砧 腰属 識澱走 識澱喫");
                    
                }
            });
        }
        // id亜 1000析 凶税 繕闇 蓄亜
        else if (id == 1000 && portraitIndex == 1)
        {
            ShowChoiceUI("陥献 識澱走 1", "陥献 識澱走 2", (choice) =>
            {
                if (choice == 1)
                {
                    Debug.Log("id亜 1000析 凶 湛 腰属 識澱走 識澱喫");
                    // 食奄拭 湛 腰属 識澱拭 魚献 稽送 蓄亜
                }
                else if (choice == 2)
                {
                    Debug.Log("id亜 1000析 凶 砧 腰属 識澱走 識澱喫");
                    // 食奄拭 砧 腰属 識澱拭 魚献 稽送 蓄亜
                }
            });
        }

        return portrait;
    }

    // 識澱 UI研 左戚惟 馬壱, 獄動拭 努什闘人 紬拷聖 竺舛
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
