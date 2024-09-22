using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;       // �̱��� �ν��Ͻ�
    public AudioClip[] soundClips;             // ���� Ŭ�� �迭
    private AudioSource[] audioSources;        // ���� AudioSource �迭
    public AudioMixer audioMixer;              // AudioMixer ����
    public AudioMixerGroup masterGroup;        // Master AudioMixerGroup ����

    public Slider volumeSlider;

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
        // �����̴� �ʱⰪ ����
        volumeSlider.value = 1.0f;

        // AudioSource �迭 �ʱ�ȭ
        audioSources = new AudioSource[soundClips.Length];

        // �����̴� �� ���� �̺�Ʈ �߰�
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // AudioSource ������Ʈ �߰� �� ����
        for (int i = 0; i < soundClips.Length; i++)
        {
            // AudioSource �߰�
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            // ���� Ŭ�� �Ҵ�
            audioSources[i].clip = soundClips[i];
            // AudioSource�� Output�� Master �׷����� ����
            audioSources[i].outputAudioMixerGroup = masterGroup;
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

    // ���� ���� �Լ�
    public void SetVolume(float volume)
    {
        Debug.Log("Volume: " + volume);  // ���� �� ���
        if (volume <= 0.0001f)
        {
            audioMixer.SetFloat("Master", -80f);  // ���Ұ�
        }
        else
        {
            audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        }
    }
}
