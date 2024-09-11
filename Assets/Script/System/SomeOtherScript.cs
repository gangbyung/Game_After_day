using UnityEngine;

public class SomeOtherScript : MonoBehaviour
{
    public LoadingUIManager loadingUIManager;

    private void Start()
    {
        // 씬 이름과 그에 대한 맵 이름 설정
        loadingUIManager.SetMapNameForScene("1.part0", "발전소 인근숲");
        loadingUIManager.SetMapNameForScene("2.part1", "도심");
        loadingUIManager.SetMapNameForScene("4.part3", "기술자 임시캠프");
        loadingUIManager.SetMapNameForScene("5.part4", "기술자 임시캠프");
        loadingUIManager.SetMapNameForScene("6.part5", "원자력 발전소 앞");
        loadingUIManager.SetMapNameForScene("7.part6", "상황실");
    }
}