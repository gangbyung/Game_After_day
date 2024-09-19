using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class TalkManager : MonoBehaviour
{
    public GameObject choiceUI;  // 선택 UI 패널
    public Button choiceButton1; // 첫 번째 버튼
    public Button choiceButton2; // 두 번째 버튼
    public TextMeshProUGUI choiceButton1Text; // 첫 번째 버튼의 텍스트
    public TextMeshProUGUI choiceButton2Text; // 두 번째 버튼의 텍스트

    public GameObject talkPanel;

    public GameObject MiniGamePanel;

    private Action<int> onChoiceMade;  // 버튼 클릭 시 실행될 콜백 함수

    Dictionary<int, string[]> talkData;
    Dictionary<int, string[]> NameData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;
    public Sprite[] player_portraitArr;

    public GameObject[] Buttons;

    public static TalkManager Instance { get; private set; }

    Changemap Chmap;
    ImageFader imageFader;
    PrefabSpawner prefabSpawner;
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
        imageFader = FindObjectOfType<ImageFader>();
        Chmap = GetComponent<Changemap>();
        prefabSpawner = GetComponent<PrefabSpawner>();
        //NpcAction a = FindObjectOfType<NpcAction>();

    }
    void GenerateData()
    {
        //NPC 1 선캡아줌마 대사
        talkData.Add(1000, new string[] {
            "어유, 간만에 산책나왔더니만 계속 날씨가 이모양이네 ...:0",
            "발전소에서 뭔가 생긴 이후부터 하늘이 기분나쁘게 먹구름만 가득하지 뭐야:1",
            "그러고 보니 총각, 옛날에 저기서 일한다고 했었던 것 같은데, 뭔지 알어?:2" ,
            "아마 발전소 폭발이랑 연관은 없을 거에요.:3",
            "그래? 그럼 뭐 그렇게 큰 일도 아니구만.:4" ,
            "아, 조금있으면 장마철이기도 했구만,:5",
            "'...상황이 어떻게 되고 있는지는 몰라도 호우로 방사능 물질이 근방에 유출되면 큰일이 날텐데':6"});
        NameData.Add(1000, new string[] { "선캡아줌마&0", "선캡아줌마&1", "선캡아줌마&2", "주인공&3", "선캡아줌마&4", "선캡아줌마&5", "주인공&6" });

        //NPC 2 청년 대사
        talkData.Add(2000, new string[] {
            "1억...1억이라니..:0",
            "와, 무슨일인진 몰라도 축하드려요.:1",
            "내 집값..:2",
            "어제까진 3억에 거래되던 내집이...으악!!!!:3" ,
            "'...무시하고 지나가자':4"});
        NameData.Add(2000, new string[] { "청년&0", "주인공&1", "청년&2", "청년&3", "주인공&4" });

        //NPC 3 과일가게 아줌마
        talkData.Add(3000, new string[] {
            "요즘 들어 뉴스에서 도통 안좋은 일들만 나온단 말이지.:0",
            "바로 요전번에는 그 서울 어디쯤에서 대지진이 일어났다고 하더만, 바로 오늘은 저기 공장 같은게 폭발했다 하지 않나:1",
            "하여간에 세상 참 흉흉하단 말이지...:2",
            "그래서 과일은 안 살겨?:3",
            "흠 그럼...:4",
            "사과 하나만 주세요.:5" });
        NameData.Add(3000, new string[] { "과일가게 아줌마&0", "과일가게 아줌마&1", "과일가게 아줌마&2", "과일가게 아줌마&3", "주인공&4", "주인공&5" });

        //NPC 4 경비원
        talkData.Add(4000, new string[] {
            "저기요! 잠시만요:0",
            "네?:1",
            "여긴 외부인 출입 금지인데 어떻게..?:2",
            "혹시 이번에 오시기로한 최 교수님이신가요?:3",
            "그건 아닌데요..:4",
            "아하..이곳부턴 외부인 출입 금지라서 이 이상 앞으로 가시면 안됩니다.:5",
            "'그럼 돌아가야하려나...':6",
            "또각.또각. (누군가 오고있다):7" });
        NameData.Add(4000, new string[] { "???&0", "주인공&1", "경비원&2", "경비원&3", "주인공&4", "경비원&5", "주인공&6", "&7" });

        //NPC 5 후배
        talkData.Add(5000, new string[] {
            "어? 선배님 맞으시죠? 오랜만이네요! 저희 마지막으로 만난게 선배님 퇴직 기념으로 같이 술 마셨을 때니까...:0",
            "1년은 지났던 것 같은데! 아 혹시 여기로 차출되신건가요?:1",
            "어? 그건아니고 걷다보니 오게됐네:2",
            "아 그렇군요 사실 야기 일손이 꽤나 부족하거든요.. 발전소만 터진것이면 진작에 전문가들 밀어넣어서 지금쯤 해결되었을텐데..:3",
            "하필이면 그 터지기 1주일전에 서울에 대지진이 일어나는 바람에...:4",
            "..큼 잡설은 여기까지 하죠:5",
            "아무튼 이것도 인연인데, 한 번 구경이라도 하실래요??:6",
            ":7"});
        NameData.Add(5000, new string[] { "후배&0", "후배&1", "주인공&2", "후배&3", "후배&4", "후배&5", "후배&6", "&7" });

        //NPC 5 후배 선택지 1
        talkData.Add(5100, new string[] {
            "아..그럼 어쩔수 없죠..:0",
            "조심히 들어가십쇼:1",
            ":2"
        });
        NameData.Add(5100, new string[] { "후배&0", "후배&1", "&2" });

        //NPC 5 후배 선택지 2
        talkData.Add(5200, new string[] {
            "아, 이제 권한이 생겨서 들어가도 되실겁니다!:0",
            "자문을 받기 위해 제가 초청한 걸로 말해 뒀거든요:1",
            "좋네, 그럼 이제 앞으로 가면 되는거야?:2",
            "아, 여기가 산속인지라 길이 꽤 복잡합니다:3",
            "임시 캠프까지 길안내를 해드릴테니 저를 따라와 주십쇼:4"
        });
        NameData.Add(5200, new string[] { "후배&0", "후배&1", "주인공&2", "후배&3", "후배&4" });
        //---------------------------------------------------------------------------------------------------------------------------------
        //NPC 6 기술자1
        talkData.Add(6000, new string[]{
            "도착이에요. 좀 둘러보시다가 가세요:0"
        });
        NameData.Add(6000, new string[] { "후배&0" });

        //NPC 4 후배 (아직은)
        talkData.Add(7000, new string[] {
            "다 둘러보셨나요?:0",
        });
        NameData.Add(7000, new string[] { "후배&0" });

        //NPC 6 기술자1
        talkData.Add(8000, new string[] {
            "못보던 얼굴인데...:0",
            "아닌가? 아니면 미안하고, 내가 지금 3일간 잠을 못자서 지금 정신이 좀 오락가락하거든? 이해 좀 해줘.:1",
            "저런...:2",
            "아무튼, 손 좀 남으면 저기 있는 서류 좀 박 팀장한테 건네줄래? 저저, 머리가 반쯤 까져있는 사람 말이야.:3",
            "네?:4",
            " 저 여기서 일하는 사람 아닌데...:4",
            "(대답을 듣지 못하고 책상 위로 쓰러진다)..zzZ:5"
        });
        NameData.Add(8000, new string[] { "기술자1&0", "기술자1&1", "주인공&2", "기술자1&3", "주인공&4", "주인공&5", "기술자1&6" });

        // NPC 7 기술자2
        talkData.Add(9000, new string[] {
            "박 팀장님 맞으신가요?:0",
            "예? 아, 예. 맞을겁니다. 무슨일이죠?:1",
            "저기 계시는 분이 이걸 좀 전달해 드리라 하셔서요:2",
            "아하... 그건 책상 오른쪽 끝에 놔주시면 됩니다.:3",
            "(종이더미를 테이블 위에 둠):4"
        });
        NameData.Add(9000, new string[] { "주인공&0", "기술자2&1", "주인공&2", "기술자2&3", "주인공&4" });

        // NPC 7 기술자2
        talkData.Add(10000, new string[] {
            "...:0",
            "... 혹시 다른 용건이 있으신가요?:1",
            "상황이 어떻게 되어가고 있는지 궁금해서요.:2",
            "아... 상황, 별로 좋지 않아요. 아니, 거의 최악에 가깝죠.:3",
            "뉴스에서는 근시일 내로 수습이 가능하다고 하던데:4",
            "글쎄요, 높으신 분들이 하는 말을 믿으세요?:5",
            "애초에, 이건 단순 불량 때문에 일어난 일이 아니라고요,:6",
            "노후화 된 기계야 뭐 대부분이 그렇다 칩시다. 부품을 생산하는 납품업체가 예산을 삥땅쳐서 불량품들을 납품하고 정부는 계속해서 더 많은 전력을 생산하기 원했고, 그 결과 멀쩡하지도 않은 기계를 과출력 시켰죠.:7",
            "아시다시피 원자력 발전은 위험하고, 사고가 터지는 순간 어떻게 되는지는 다들 잘 알기 때문에 보통은 혹시 모를 상황을 대비한 장치가 작동하지 않을 경우를 대비한 것도 안될 것을 대비한... 온갖 보호장치를 걸어두죠.:8",
            "그럼 어째서 막지 못하는거야?!:9",
            "장치가 있으면 뭐합니까! 대부분은 부식되고 고장나서 작동해봤자 제대로 막지도 못해요!:10",
            "아무튼 그나마 멀쩡한 장치라도 찾아내서 이 피해를 최소화 시키는 것이 저희의 목표입니다.:11"
        });
        NameData.Add(10000, new string[] { "주인공&0", "기술자2&1", "주인공&2", "기술자2&3", "주인공&4", "기술자2&5", "기술자2&6", "기술자2&7", "기술자2&8", "주인공&9", "기술자2&10", "기술자2&11" });

        //NPC 8 기술자3
        talkData.Add(11000, new string[] {
            "하여간에... 내가 무슨 부귀영화를 누리려고 팔자에도 없는 개고생을...:0",
            "... 아, 무슨 일이시죠?:1",
            "(에너지 드링크 건네주기):2",
            "이건... 아하, 감사합니다.:3",
            "(꿀꺽.꿀꺽.) 크으, 이제야 살 것 같네~:4",
            "일이 많으신가보네요.:5",
            "많죠, 할 일이 얼마나 많은데요!:6",
            "하..이게 다 저 발전소 때문이죠.:7",
            "다른 자연물을 활용한 발전소는 현 시점에서 원자력 발전소보다 더욱 효율적이고 나은 방법으로 사람들이 살아가는데 필요로 하는 최소한의 에너지를 생산해 낼 수 있는 획기적인 방안도 없고:8",
            "원자력 발전소는 위험하고 폭발도 폭발이지만, 방호벽이 무너지면 방사능들이 마구잡이로 퍼질테니까요.:9",
            "그런 상황에서도 대부분의 원자력 발전소는 이미 예정된 기한을 한참 넘겨서 아직까지 운행하는 일이 많고, 부품들 또한 이미 부식되고 노후화되어 교체가 필요한 것들을 그대로 내버려두기만 하죠.:10",
            "그리고 지금까지 아무런 일도 생기지 않았으니 지금까지 하던 대로 하더라도 큰 일이 생기지는 않을 것이다... 라고 말하면서요.:11",
            "..그 결과 지금 이 꼴이 났네요. 분명 몇 십톤 충격에도 버틸 수 있게 설계한 비상 프로토콜 조작실도 같이 불에 탔고요.:12",
            "애초에, 정말 안전하다면 송전 비용 절감하게 서울에 몇십개는 짓지 않겠어요? 그런데 옹호하는 사람 치고 그 좋은 원전을 서울 아니면 자기 사는 지역에 짓자고 당당하게 말할 수 있는 놈은 몇 없더군요.:13",
            "자기들도 대강 알고 있으면서 모르쇠 하는거죠. 그리고 그 고생은 저희가 대신 하고 있네요...:14",
            "고생이 많으시네요..:15",
        });
        NameData.Add(11000, new string[] { "기술자3&0", "주인공&1", "주인공&2", "기술자&3", "기술자&4", "주인공&5", "기술자3&6", "기술자3&7", "기술자3&8", "기술자3&9", "기술자3&10", "기술자3&11", "기술자3&12", "기술자3&13", "기술자3&14", "주인공&15" });

        //NPC 4 후배 다둘러봤어
        talkData.Add(12000, new string[]
        {
            "다 둘러보셨나요?:0",
            "대강 다 둘러본것같아:1",
            "그거 좋네요! 어떠셨나요?:2",
            "네가 왜 굳이 나를 데려왔는지에 대한 이유를 대충 알 것 같았어.:3",
            "하하..:4",
            "... 네, 선배님이 생각하시는 그게 맞을겁니다.:5",
            "아까 보셔서 아시겠지만은, 여기 상황은 선배님이 생각하시는 대로 개판입니다.:6",
            "수리에 필요한 최소치에 비하면 턱없이 부족한 인력, 자원, 그러면서도 발전소의 상태는 시간이 지날수록 나빠지기만 하죠.:7",
            "어쩌면 폭발해 버릴지도 몰라요. 방호벽도 제대로 지어지지 않았으니 잘못하면 안에 있는 방사능 물질들까지 도심에 새어나가면... 이 근방은 말 그대로 초토화가 되겠죠.:8",
            "폐허가 된 서울을 재건하는데도 벅찬 정부가 이곳에 사는 10만명의 시민들을 어떻게든 챙길 수 있을 지 조차 의문이죠.:9",
            "그런데 발전소의 폭발은 코 앞까지 다가왔고... 어떻게든 막아내기에는 모든것들이 부족합니다.:10",
            "이런 부탁을 드리게 되어 죄송하지만,:11",
            "선배님의 도움이 필요합니다...:12",
            ":13"
        });
        NameData.Add(12000, new string[] { "후배&0", "주인공&1", "후배&2", "주인공&3", "후배&4", "후배&5", "후배&6", "후배&7", "후배&8", "후배&9", "후배&10", "후배&11", "후배&12","&13" });

        // NPC 4 후배(2부 루트)
        talkData.Add(13000, new string[] {
            "... 그렇군요.:0",
            "뭐 어쩔 수 없죠. 이런 일을 제멋대로 강요할 수도 없는 노릇이기도 하고요.:1",
            "이 일은 저희끼리라도 어떻게든 해봐야죠, 뭐...:2",
            "(어색한 침묵):3",
            "크흠... 안녕히 가십쇼:4",
            ":5"
        });
        NameData.Add(13000, new string[] { "후배&0", "후배&1", "후배&2", "후배&3", "후배&4","&5" });

        // NPC 4 후배(1부)
        talkData.Add(14000, new string[]{
            "정말입니까...?! 긍정적인 답변이 나올 것이라 생각하질 못했습니다.:0",
            "그럼 조력에 관한 것은 제가 대신 말해두겠습니다.:1",
            "... 감사합니다. 쉽지 않은 결정을 내려주셔서:2",
            "감사하면 다음에 밥이나 한번 사:3",
            "하하, 일이 마무리 되면 그렇게 하죠:4",
            "마침 제가 고기를 기가막히게 구워주는 맛집을 찾아낸 참이거든요.:5",
        });
        NameData.Add(14000, new string[] { "후배&0", "후배&1", "후배&2", "주인공&3", "후배&4", "후배&5" });

        //NPC 4 후배
        talkData.Add(15000, new string[]
        {
            "오셨습니까 선배님!:0",
            "우선 내가 뭘 하면 좋을지에 대해 듣고싶은데.:1",
            "아 그거요? 일단 현재 상황에 대한 요약 서류를...:2",
            "(테이블 위에 마구잡이로 올려진 서류들을 힐끗 바라본다)음...:3",
            "아하하... 면목이 없네요, 정리할 시간이 없어서:4",
            "찾는 김에 정리하는 게 좋겠네.:5",
            ":6",
        });
        NameData.Add(15000, new string[] { "후배&0", "주인공&1", "후배&2", "주인공&3", "후배&4", "주인공&5","&6" });

        //NPC 4후배
        talkData.Add(16000, new string[] {
            "도와주셔서 감사합니다. 그럼 어디보자...:0",
            "아, 찾았다! 여기 있어요.:1",
            "... 이거 기한이 좀 오래된 것 같은데:2",
            "음... 원래라면 이번주에 조사원들이 재탐색을 마치고 그에 관해 보고서를 쓸 예정이었는데 말이죠,:3",
            "... 이번에 시설을 확인하러 간 조사원들이 들어가기도 전에 사고로 인해 모두 응급시설로 실려갔거든요.당장 부상으로 끙끙대는 사람들한테 일에 대해서 말할수도 없는 노릇인지라... 뭐, 그렇게 되었네요.:4",
            "저런:5",
            "그럼 어떻게 확인을 해야 하는거지?:6",
            "별 수 있나요. 막연하게 정부에서 새 인력을 파견해 주거나 그 분들의 부상이 다 나을 때까지 기다릴 수도 없는 노릇이고, 저희라도 가는 수 밖에요.:7",
            "당장 확인을 안하면 또 어떻게 될 지 모르는 상황이니까요.:8",
            "그럼 내가 한번 가볼게.:9",
            "위험하지 않으시겠어요?:10",
            "... 여기에서 발전소 탐사까지 할 만큼의 시간 여유가 있는 사람이 없어 보여서.:11",
            "아하... 참고로 방사능은 아직 안 무너진 외부 방호벽 때문에 발전소 바깥으로만 유출되지 않았을 뿐, 안쪽은 장비 없이 맨몸으로 몇 초만 머무른다면 몇 분도 채 못버티고 흐물흐물 해질거에요.:12",
            "게다가 지금 추측되는 시설 내의 방사능 수치로 보자면 제대로 된 장비를 착용하고 들어간다 하더라도 100% 완벽하게 방호가 불가능해요.:13",
            "오래 머문다면... 네, 방사능의 영향을 꽤 많이 받게 될거에요. 피부에서 체렌코프 현상을 관측할 수 있는건 덤이고요.:14",
            "그런데도 정말로 가실 수 있으세요?:15",
            "해보지 뭐.:16",
            "... 대단하시네요.:17",
            "그럼 탐사팀 분들이 사용하시는 장비 창고까지 안내해 드릴게요.:18",
            "반출 허가는 진작에 받아놨거든요.:19",
        });
        NameData.Add(16000, new string[] { "후배&0", "후배&1", "주인공&2", "후배&3", "후배&4", "주인공&5", "주인공&6", "후배&7", "후배&8", "주인공&9", "후배&10", "주인공&11", "후배&12", "후배&13", "후배&14", "후배&15", "주인공&16", "후배&17", "후배&18", "후배&19" });

        //NPC 4 후배
        talkData.Add(17000, new string[] {
            "여기가 창고에요.:0",
            "원래는 제가 가려고 챙겨둔건데... 선배님이 가신다 하실 줄은 몰랐네요.:1",
            "눈대중으로 봤을때 저랑 체격이 비슷하시니 아마 맞으실거에요.:2",
            //바구니 상호작용 후 아이템 획득
        });
        NameData.Add(17000, new string[] { "후배&0", "후배&1", "후배&2" });

        //NPC 4 후배
        talkData.Add(18000, new string[] {
            "조심히 다녀오십쇼:0"
        });
        NameData.Add(18000, new string[] { "후배&0" });

        //NPC 6 기술자1
        talkData.Add(19000, new string[] {
            "며칠도 안돼서 이런 일을 맡기게 되어 미안하구만.:0"
        });
        NameData.Add(19000, new string[] { "기술자1&0" });

        //NPC 7 기술자2
        talkData.Add(20000, new string[] { "수고가 많으십니다. 잘 다녀오십쇼.:0" });
        NameData.Add(20000, new string[] { "기술자2&0" });

        //NPC 8 기술자3
        talkData.Add(21000, new string[] { "탐사가 주 목적이긴 하지만 가장 중요한 건 건강입니다.:0" });
        NameData.Add(21000, new string[] { "기술자3&0" });

        //주인공 독백
        talkData.Add(22000, new string[] {
            "'... 을씨년스러운 분위기네,':0",
            "'무단 침입 방지 시스템은 이전에 미리 꺼둔건가,':1",
            "(부숴진 바닥 목격):2",
            "'이건...':3",
            "'..떨어진다면 꽤 위험하겠군.':4"
        });
        NameData.Add(22000, new string[] { "주인공&0", "주인공&1", "주인공&2", "주인공&3", "주인공&4" });

        //2 스태이지 독백
        talkData.Add(23000, new string[] {
            "'...열쇠는 어디에있지? 열쇠가 있어야 상황실에 갈 수 있는데..':0",
            "'열쇠를 찾으러 올라가야겠어.':1"
        });
        NameData.Add(23000, new string[] { "주인공&0", "주인공&1" });

        //스핃 ㅡ런
        talkData.Add(24000, new string[] {
            "(새어나온 방사능 목격):0",
            "'...!':1",
            "'방벽이, 녹아내린건가?':2"
        });
        NameData.Add(24000, new string[] { "주인공&0", "주인공&1", "주인공&2" });

        talkData.Add(24100, new string[] {
            "열쇠를 찾았다.!:0",
            "이제 상황실로 들어갈 수 있겠어..빨리 올라가자!:1",
            
        });
        NameData.Add(24100, new string[] { "주인공&0", "주인공&1"});

        talkData.Add(25000, new string[] {
            "'젠장, 아무래도 1층은 글렀군...':0",
            "'서둘러서 상황실로 가야겠어...!':1"
        });
        NameData.Add(25000, new string[] { "주인공&0", "주인공&1" });

        //상황실 도착
        talkData.Add(26000, new string[] {
            "(모조리 빨간불이 들어온 버튼들을 바라봄):0",
            "'...예상보다 심각해.':1",
            "'이 정도라면... 아냐, 지금 돌아가서 알린다 하더라도 늦어.':2",
            "'애초에 해결책 조차 찾기 어려워 내가 파견된...':3",
            //여기에 건물이 흔들리는 소리
            "'..!':4"//건물이 흔들림. 밀풍선에 !
        });
        NameData.Add(26000, new string[] { "주인공&0", "주인공&1", "주인공&2", "주인공&3", "주인공&4" });

        talkData.Add(27000, new string[] {
            "(모니터 전원 키고 버튼 누름):0",
            "'이건...':1",
            "'... 젠장, 이것도 고장난건가.':2",
            "'아니, 그냥 단순한 전력 부족이라면...':3"

        });
        NameData.Add(27000, new string[] { "주인공&0", "주인공&1", "주인공&2","주인공&3" });

        talkData.Add(28000, new string[] {
            "'(끊어진 전선을 보면서) 이게 문제였던 건가...':0",
            "'일단 이걸 연결시키면 어떻게든 작동이 될 지도 몰라.':1",
            "'...!':2",
            "'이거다!':3"
        });
        NameData.Add(28000, new string[] { "주인공&0", "주인공&1", "주인공&2", "주인공&3" });

        talkData.Add(29000, new string[] {
            "'차폐막이 내려와야 하는데.':0",
            "'버튼이 작동하지 않네.':1",
            "'... 망가진 건가, 어쩐지... 싸구려 티가 약간 나더라니만...':2",
            "'(아까 연결한 줄을 살펴보며... 전력은 확실하게 연결되어 있어. 신호만 보낸다면 바로 작동될 터,':3",
            "'그렇다면... 통제실이야..!':4"
        });

        NameData.Add(29000, new string[] { "주인공&0", "주인공&1", "주인공&2", "주인공&3", "주인공&4" });

        talkData.Add(30000, new string[] {
            "'작동시키는 순간, 나는 꼼짝없이 이곳에 갇혀 온 몸이 녹아내리겠지. 체렌코프 현상으로 몸이 형광색으로 빛나는 건 덤일테고':0",
            "'... 나는 이곳에서 죽게 될테지.':1"
            });

        NameData.Add(30000, new string[] { "주인공&0", "주인공&1" });

        talkData.Add(31000, new string[] {
            "'내 기억이 확실하다면 신호 전송에 쓰는 버튼은 맨 왼쪽에 있는 작은 레버다.':0",
            "'이걸 당기면 예비의 예비의 예비의... 장치가 작동되면서 이전에 설치했었던 구식 방호벽이 올라와 이 시설 밖에 영향이 가지 않게 하겠지.':1",
            "'... 고민할 필요는 없다':2",
            "철컥..!:3"
        });
        NameData.Add(31000, new string[] { "주인공&0", "주인공&1", "주인공&2" ,"&3"});
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

        //NPC 4 경비원
        portraitData.Add(4000 + 0, portraitArr[5]);
        portraitData.Add(4000 + 1, player_portraitArr[0]);
        portraitData.Add(4000 + 2, portraitArr[5]);
        portraitData.Add(4000 + 3, portraitArr[5]);
        portraitData.Add(4000 + 4, player_portraitArr[0]);
        portraitData.Add(4000 + 5, portraitArr[5]);
        portraitData.Add(4000 + 6, player_portraitArr[0]);
        portraitData.Add(4000 + 7, portraitArr[0]);

        //NPC 5 후배
        portraitData.Add(5000 + 0, portraitArr[4]);
        portraitData.Add(5000 + 1, portraitArr[4]);
        portraitData.Add(5000 + 2, player_portraitArr[0]);
        portraitData.Add(5000 + 3, portraitArr[4]);
        portraitData.Add(5000 + 4, portraitArr[4]);
        portraitData.Add(5000 + 5, portraitArr[4]);
        portraitData.Add(5000 + 6, portraitArr[4]);
        portraitData.Add(5000 + 7, portraitArr[0]);

        //NPC 5 후배 선택지 1
        portraitData.Add(5100 + 0, portraitArr[4]);
        portraitData.Add(5100 + 1, portraitArr[4]);
        portraitData.Add(5100 + 2, portraitArr[4]);

        //NPC 5 후배 선택지 2
        portraitData.Add(5200 + 0, portraitArr[4]);
        portraitData.Add(5200 + 1, portraitArr[4]);
        portraitData.Add(5200 + 2, player_portraitArr[0]);
        portraitData.Add(5200 + 3, portraitArr[4]);
        portraitData.Add(5200 + 4, portraitArr[4]);


        //NPC 6 기술자 1
        portraitData.Add(6000 + 0, portraitArr[4]);

        //NPC 4 후배
        portraitData.Add(7000 + 0, portraitArr[4]);

        //NPC 6 기술자 1
        portraitData.Add(8000 + 0, portraitArr[6]);
        portraitData.Add(8000 + 1, portraitArr[6]);
        portraitData.Add(8000 + 2, player_portraitArr[0]);
        portraitData.Add(8000 + 3, portraitArr[6]);
        portraitData.Add(8000 + 4, player_portraitArr[0]);
        portraitData.Add(8000 + 5, player_portraitArr[0]);
        portraitData.Add(8000 + 6, portraitArr[6]);

        //NPC 7 기술자 2
        portraitData.Add(9000 + 0, player_portraitArr[0]);
        portraitData.Add(9000 + 1, portraitArr[7]);
        portraitData.Add(9000 + 2, player_portraitArr[0]);
        portraitData.Add(9000 + 3, portraitArr[7]);
        portraitData.Add(9000 + 4, player_portraitArr[0]);

        //NPC 7 기술자 2
        portraitData.Add(10000 + 0, player_portraitArr[0]);
        portraitData.Add(10000 + 1, portraitArr[7]);
        portraitData.Add(10000 + 2, player_portraitArr[0]);
        portraitData.Add(10000 + 3, portraitArr[7]);
        portraitData.Add(10000 + 4, player_portraitArr[0]);
        portraitData.Add(10000 + 5, portraitArr[7]);
        portraitData.Add(10000 + 6, portraitArr[7]);
        portraitData.Add(10000 + 7, portraitArr[7]);
        portraitData.Add(10000 + 8, portraitArr[7]);
        portraitData.Add(10000 + 9, player_portraitArr[0]);
        portraitData.Add(10000 + 10, portraitArr[7]);
        portraitData.Add(10000 + 11, portraitArr[7]);

        //NPC 8 기술자 3
        portraitData.Add(11000 + 0, portraitArr[8]);
        portraitData.Add(11000 + 1, player_portraitArr[0]);
        portraitData.Add(11000 + 2, player_portraitArr[0]);
        portraitData.Add(11000 + 3, portraitArr[8]);
        portraitData.Add(11000 + 4, portraitArr[8]);
        portraitData.Add(11000 + 5, player_portraitArr[0]);
        portraitData.Add(11000 + 6, portraitArr[8]);
        portraitData.Add(11000 + 7, portraitArr[8]);
        portraitData.Add(11000 + 8, portraitArr[8]);
        portraitData.Add(11000 + 9, portraitArr[8]);
        portraitData.Add(11000 + 10, portraitArr[8]);
        portraitData.Add(11000 + 11, portraitArr[8]);
        portraitData.Add(11000 + 12, portraitArr[8]);
        portraitData.Add(11000 + 13, portraitArr[8]);
        portraitData.Add(11000 + 14, portraitArr[8]);
        portraitData.Add(11000 + 15, player_portraitArr[0]);

        //NPC 4 후배
        portraitData.Add(12000 + 0, portraitArr[4]);
        portraitData.Add(12000 + 1, player_portraitArr[0]);
        portraitData.Add(12000 + 2, portraitArr[4]);
        portraitData.Add(12000 + 3, player_portraitArr[0]);
        portraitData.Add(12000 + 4, portraitArr[4]);
        portraitData.Add(12000 + 5, portraitArr[4]);
        portraitData.Add(12000 + 6, portraitArr[4]);
        portraitData.Add(12000 + 7, portraitArr[4]);
        portraitData.Add(12000 + 8, portraitArr[4]);
        portraitData.Add(12000 + 9, portraitArr[4]);
        portraitData.Add(12000 + 10, portraitArr[4]);
        portraitData.Add(12000 + 11, portraitArr[4]);
        portraitData.Add(12000 + 12, portraitArr[4]);
        portraitData.Add(12000 + 13, portraitArr[0]);


        //NPC 4 후배 
        portraitData.Add(13000 + 0, portraitArr[4]);
        portraitData.Add(13000 + 1, portraitArr[4]);
        portraitData.Add(13000 + 2, portraitArr[4]);
        portraitData.Add(13000 + 3, portraitArr[4]);
        portraitData.Add(13000 + 4, portraitArr[4]);
        portraitData.Add(13000 + 5, portraitArr[0]);

        //NPC 4 후배 (1부)
        portraitData.Add(14000 + 0, portraitArr[4]);
        portraitData.Add(14000 + 1, portraitArr[4]);
        portraitData.Add(14000 + 2, portraitArr[4]);
        portraitData.Add(14000 + 3, player_portraitArr[0]);
        portraitData.Add(14000 + 4, portraitArr[4]);
        portraitData.Add(14000 + 5, portraitArr[4]);

        //NPC 4 후배
        portraitData.Add(15000 + 0, portraitArr[4]);
        portraitData.Add(15000 + 1, player_portraitArr[0]);
        portraitData.Add(15000 + 2, portraitArr[4]);
        portraitData.Add(15000 + 3, player_portraitArr[0]);
        portraitData.Add(15000 + 4, portraitArr[4]);
        portraitData.Add(15000 + 5, player_portraitArr[0]);
        portraitData.Add(15000 + 6, portraitArr[0]);

        //NPC 4 후배
        portraitData.Add(16000 + 0, portraitArr[4]);
        portraitData.Add(16000 + 1, portraitArr[4]);
        portraitData.Add(16000 + 2, player_portraitArr[0]);
        portraitData.Add(16000 + 3, portraitArr[4]);
        portraitData.Add(16000 + 4, portraitArr[4]);
        portraitData.Add(16000 + 5, player_portraitArr[0]);
        portraitData.Add(16000 + 6, player_portraitArr[0]);
        portraitData.Add(16000 + 7, portraitArr[4]);
        portraitData.Add(16000 + 8, portraitArr[4]);
        portraitData.Add(16000 + 9, player_portraitArr[0]);
        portraitData.Add(16000 + 10, portraitArr[4]);
        portraitData.Add(16000 + 11, player_portraitArr[0]);
        portraitData.Add(16000 + 12, portraitArr[4]);
        portraitData.Add(16000 + 13, portraitArr[4]);
        portraitData.Add(16000 + 14, portraitArr[4]);
        portraitData.Add(16000 + 15, portraitArr[4]);
        portraitData.Add(16000 + 16, player_portraitArr[0]);
        portraitData.Add(16000 + 17, portraitArr[4]);
        portraitData.Add(16000 + 18, portraitArr[4]);
        portraitData.Add(16000 + 19, portraitArr[4]);

        //NPC 4후배
        portraitData.Add(17000 + 0, portraitArr[4]);
        portraitData.Add(17000 + 1, portraitArr[4]);
        portraitData.Add(17000 + 2, portraitArr[4]);

        //NPC 4후배
        portraitData.Add(18000 + 0, portraitArr[4]);

        //NPC 6 기술자 1
        portraitData.Add(19000 + 0, portraitArr[6]);

        //NPC 7 기술자 2
        portraitData.Add(20000 + 0, portraitArr[7]);

        //NPC 8 기술자 3
        portraitData.Add(21000 + 0, portraitArr[8]);

        //주인공 독백
        portraitData.Add(22000 + 0, player_portraitArr[0]);
        portraitData.Add(22000 + 1, player_portraitArr[0]);
        portraitData.Add(22000 + 2, player_portraitArr[0]);
        portraitData.Add(22000 + 3, player_portraitArr[0]);
        portraitData.Add(22000 + 4, player_portraitArr[0]);

        //2스태이지 독백
        portraitData.Add(23000 + 0, player_portraitArr[0]);
        portraitData.Add(23000 + 1, player_portraitArr[0]);

        //스피드런
        portraitData.Add(24000 + 0, player_portraitArr[0]);
        portraitData.Add(24000 + 1, player_portraitArr[0]);
        portraitData.Add(24000 + 2, player_portraitArr[0]);

        portraitData.Add(24100 + 0, player_portraitArr[0]);
        portraitData.Add(24100 + 1, player_portraitArr[0]);
        //스피드런2
        portraitData.Add(25000 + 0, player_portraitArr[0]);
        portraitData.Add(25000 + 1, player_portraitArr[0]);

        //상황실 도착
        portraitData.Add(26000 + 0, player_portraitArr[0]);
        portraitData.Add(26000 + 1, player_portraitArr[0]);
        portraitData.Add(26000 + 2, player_portraitArr[0]);
        portraitData.Add(26000 + 3, player_portraitArr[0]);
        portraitData.Add(26000 + 4, player_portraitArr[0]);

        //상호작용
        portraitData.Add(27000 + 0, player_portraitArr[0]);
        portraitData.Add(27000 + 1, player_portraitArr[0]);
        portraitData.Add(27000 + 2, player_portraitArr[0]);
        portraitData.Add(27000 + 3, player_portraitArr[0]);

        //전선문제독백
        portraitData.Add(28000 + 0, player_portraitArr[0]);
        portraitData.Add(28000 + 1, player_portraitArr[0]);
        portraitData.Add(28000 + 2, player_portraitArr[0]);
        portraitData.Add(28000 + 3, player_portraitArr[0]);

        //차폐막독백
        portraitData.Add(29000 + 0, player_portraitArr[0]);
        portraitData.Add(29000 + 1, player_portraitArr[0]);
        portraitData.Add(29000 + 2, player_portraitArr[0]);
        portraitData.Add(29000 + 3, player_portraitArr[0]);
        portraitData.Add(29000 + 4, player_portraitArr[0]);

        //나주겅
        portraitData.Add(30000 + 0, player_portraitArr[0]);
        portraitData.Add(30000 + 1, player_portraitArr[0]);

        //진짜 죽음 엔딩
        portraitData.Add(31000 + 0, player_portraitArr[0]);
        portraitData.Add(31000 + 1, player_portraitArr[0]);
        portraitData.Add(31000 + 2, player_portraitArr[0]);
        portraitData.Add(31000 + 3, portraitArr[0]);

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
                Debug.LogWarning("{id} {talkIndex}");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("'{id}'talkData에 존재하지 않음");
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
                NpcAction.Instance.NpcUnLock5();
            }
            else if (id == 5000 && portraitIndex == 7)
            {
                ShowChoiceUI("따라간다", "집으로 돌아간다", (choice) =>
                {
                    if (choice == 1)
                    {
                        Buttons[0].SetActive(true);
                        NpcAction.Instance.NpcLock5();
                        NpcAction.Instance.NpcUnLock5_2();
                        if (talkPanel.activeSelf)
                        {
                            GameManager.Instance.NextTalk();
                        }
                    }
                    else if (choice == 2)
                    {
                        NpcAction.Instance.NpcLock5();
                        NpcAction.Instance.NpcUnLock5_1();
                        NpcAction.Instance.NpcDumUnLock();
                    }
                });
            }
            else if (id == 5100 && portraitIndex == 2)
            {
                Changemap.Go_3_Endpart0();
            }
            else if (id == 5200 && portraitIndex == 4)
            {
                imageFader.TriggerFadeIn();
            }
            else if (id == 12000 && portraitIndex == 13)
            {
                ShowChoiceUI("까짓거 해보지", "...미안, 힘들것같다", (choice) =>
                {
                    if (choice == 1)
                    {
                        NpcAction.Instance.NpcUnLock14();
                        if (talkPanel.activeSelf)
                        {
                            GameManager.Instance.NextTalk();
                        }
                    }
                    else if (choice == 2)
                    {
                        NpcAction.Instance.NpcUnLock13();

                        if (talkPanel.activeSelf)
                        {
                            GameManager.Instance.NextTalk();
                        }
                    }
                });
            }
            else if (id == 13000 && portraitIndex == 5)
            {
                Changemap.Go_3_Endpart0();
            }
            else if(id == 15000 && portraitIndex == 6)
            {
                
                MiniGamePanel.SetActive(true);
                Debug.Log("tlqkf");
            }
            else if (id == 24000 && portraitIndex == 2)
            {
                NpcAction.Instance.RadiUnLock();
            }
            else if (id == 31000 && portraitIndex == 3)
            {
                Changemap.Go_99_EndGame();
            }
            else if (id == 9000 && portraitIndex == 5)
            {
                NpcAction.Instance.Npc3Lock();
            }
            
            return portrait; // 값을 반환
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
