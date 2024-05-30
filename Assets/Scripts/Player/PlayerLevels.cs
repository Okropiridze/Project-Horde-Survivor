using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevels : MonoBehaviour
{
    private int exp = 0;
    private int xPGoal = 5;
    private int xpIncreasePerLevel = 5;
    private float expCollectorRadius = 4.5f;
    private Dictionary<int, int> expByLevels = new Dictionary<int, int>();
    private int currentLevel = 1;
    [SerializeField] LayerMask whatIsExp;

    private void Start()
    {
        AssignLevelExps();
        PlayerUI.Instance.SetExpText(exp, expByLevels[currentLevel]);
    }

    private void Update()
    {
        TryCollectExp();
    }

    private void TryCollectExp()
    {
        Collider2D[] expiriences = Physics2D.OverlapCircleAll(transform.position, expCollectorRadius, whatIsExp);
        foreach (Collider2D expirience in expiriences)
        {
            expirience.GetComponent<Exp>().PickUp();
        }
    }

    public void AddExp()
    {
        exp++;
        if (exp >= expByLevels[currentLevel])
        {
            LevelUp();
        }
        PlayerUI.Instance.SetExpText(exp, expByLevels[currentLevel]);
    }

    void LevelUp()
    {
        exp = 0;
        currentLevel++;
        UpgradesUIManager.Instance.upgradesUI.SetActive(true);
        Time.timeScale = 0;
        UpgradesUIManager.Instance.DisplayRandomCards();
    }

    void AssignLevelExps()
    {
        for(int i = 1; i < 51; i++)
        {
            expByLevels[i] = xPGoal;
            xPGoal += xpIncreasePerLevel;
        }
    }
}
