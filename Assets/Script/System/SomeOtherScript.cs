using UnityEngine;

public class SomeOtherScript : MonoBehaviour
{
    public LoadingUIManager loadingUIManager;

    private void Start()
    {
        // 씬 이름과 그에 대한 맵 이름 설정
        loadingUIManager.SetMapNameForScene("1.part0", "발전소 인근숲");
        loadingUIManager.SetMapNameForScene("2.part1", "도심");
    }
}