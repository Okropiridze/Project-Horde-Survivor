using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeGameManager : MonoBehaviour
{
    public static HordeGameManager Instance { get; private set; }
    private float sessionTime = 900f;
    private float timeLeft;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timeLeft = sessionTime;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        PlayerUI.Instance.SetTimeText(GetMinutesLeft(), GetSecondsLeft());
    }

    int GetMinutesLeft()
    {
        return Mathf.FloorToInt(Mathf.CeilToInt(timeLeft) / 60);
    }

    int GetSecondsLeft()
    {
        return Mathf.CeilToInt(timeLeft) % 60;
    }

    public float GetSessionTimeLeft()
    {
        return timeLeft;
    }
}
