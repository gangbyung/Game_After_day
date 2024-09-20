using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;  // 싱글톤 인스턴스
    public AudioClip[] soundClips;        // 사운드 클립 배열
    private AudioSource[] audioSources;   // 여러 AudioSource 배열

    void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 변경 시 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 중복된 게임 오브젝트 제거
        }
    }

    void Start()
    {
        // AudioSource 배열 초기화
        audioSources = new AudioSource[soundClips.Length];

        // AudioSource 컴포넌트 추가 및 설정
        for (int i = 0; i < soundClips.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>(); // AudioSource 추가
            audioSources[i].clip = soundClips[i];                     // 사운드 클립 할당
        }
    }

    // 특정 인덱스의 사운드를 재생하는 메서드
    public void PlaySound(int index)
    {
        if (index >= 0 && index < soundClips.Length)
        {
            if (!audioSources[index].isPlaying)
            {
                audioSources[index].Play();  // 사운드 재생
            }
        }
        else
        {
            Debug.LogWarning("사운드 인덱스 벗어남.");
        }
    }

    // 특정 인덱스의 사운드를 멈추는 메서드
    public void StopSound(int index)
    {
        if (index >= 0 && index < audioSources.Length)
        {
            if (audioSources[index].isPlaying)
            {
                audioSources[index].Stop(); // 사운드 정지
            }
        }
        else
        {
            Debug.LogWarning("사운드 인덱스 벗어남.");
        }
    }
}
