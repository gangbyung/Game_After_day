using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundMusic; // ������� Ŭ��
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource ������Ʈ�� ����
        audioSource = GetComponent<AudioSource>();

        // ������� ���� �� �ݺ� ���
        audioSource.clip = backgroundMusic;
        audioSource.loop = true; // �ݺ� ��� Ȱ��ȭ
        audioSource.Play(); // ������� ���
    }
}
