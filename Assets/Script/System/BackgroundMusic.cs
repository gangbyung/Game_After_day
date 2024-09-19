using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundMusic; // 배경음악 클립
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource 컴포넌트를 참조
        audioSource = GetComponent<AudioSource>();

        // 배경음악 설정 및 반복 재생
        audioSource.clip = backgroundMusic;
        audioSource.loop = true; // 반복 재생 활성화
        audioSource.Play(); // 배경음악 재생
    }
}
