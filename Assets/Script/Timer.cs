using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    private static Timer instance;

    public static Timer Instance;

    public TextMeshProUGUI timer;
    public float time = 0;
    void Update()
    {
        time += Time.deltaTime;
        timer.text = "late time : " + Mathf.Round(time);
    }
}
