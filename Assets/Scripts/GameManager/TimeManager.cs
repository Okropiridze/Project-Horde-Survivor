using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SlowDown(float intencity, float duration)
    {
        Time.timeScale = (100 - intencity) / 100;
        Invoke("ResetTime", duration);
    }

    private void ResetTime()
    {
        Time.timeScale = 1;
    }
}
