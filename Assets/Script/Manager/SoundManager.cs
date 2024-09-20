using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;  // �̱��� �ν��Ͻ�
    public AudioClip[] soundClips;        // ���� Ŭ�� �迭
    private AudioSource[] audioSources;   // ���� AudioSource �迭

    void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ���� �� �ı����� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� ���� ������Ʈ ����
        }
    }

    void Start()
    {
        // AudioSource �迭 �ʱ�ȭ
        audioSources = new AudioSource[soundClips.Length];

        // AudioSource ������Ʈ �߰� �� ����
        for (int i = 0; i < soundClips.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>(); // AudioSource �߰�
            audioSources[i].clip = soundClips[i];                     // ���� Ŭ�� �Ҵ�
        }
    }

    // Ư�� �ε����� ���带 ����ϴ� �޼���
    public void PlaySound(int index)
    {
        if (index >= 0 && index < soundClips.Length)
        {
            if (!audioSources[index].isPlaying)
            {
                audioSources[index].Play();  // ���� ���
            }
        }
        else
        {
            Debug.LogWarning("���� �ε��� ���.");
        }
    }

    // Ư�� �ε����� ���带 ���ߴ� �޼���
    public void StopSound(int index)
    {
        if (index >= 0 && index < audioSources.Length)
        {
            if (audioSources[index].isPlaying)
            {
                audioSources[index].Stop(); // ���� ����
            }
        }
        else
        {
            Debug.LogWarning("���� �ε��� ���.");
        }
    }
}
