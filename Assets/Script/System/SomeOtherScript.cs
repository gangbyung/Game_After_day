using UnityEngine;

public class SomeOtherScript : MonoBehaviour
{
    public LoadingUIManager loadingUIManager;

    private void Start()
    {
        // 씬 이름과 그에 대한 맵 이름 설정
        loadingUIManager.SetMapNameForScene("1.part0", "공장부지");
        loadingUIManager.SetMapNameForScene("2.part1", "아파트단지");
    }
}