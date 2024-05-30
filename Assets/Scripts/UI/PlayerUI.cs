using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; private set; }

    [SerializeField] TMP_Text ammoText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text expText;


    private void Awake()
    {
        Instance = this;
    }

    public void SetAmmoText(float ammoLeft, float maxAmmo)
    {
        ammoText.text = ammoLeft + " / " + maxAmmo;
    }

    public void SetTimeText(int minutes, int seconds)
    {
        string FormatTime(int time)
        {
            if (time < 10) return "0" + time;
            else return time.ToString();
        }
        timeText.text = FormatTime(minutes) + ":" + FormatTime(seconds);
    }

    public void SetExpText(int exp, int expGoal)
    {
        expText.text = exp + "/" + expGoal + " EXP";
    }


}
