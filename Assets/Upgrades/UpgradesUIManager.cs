using System.Collections.Generic;
using UnityEngine;

public class UpgradesUIManager : MonoBehaviour
{
    public static UpgradesUIManager Instance { get; private set; }

    [SerializeField] Transform upgradeCardsParent;
    [SerializeField] GameObject upgradeCardPrefab;
    public GameObject upgradesUI;
    [SerializeField] private List<StatsUpgradeSO> upgradesList;
    private HashSet<int> displayedUpgradesIndices = new HashSet<int>();
    private const string upgradesPath = "Assets/Upgrades/UpgradeObjects";
    private int cardsAmount = 5;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
       upgradesList = HelperFunctions.GetScriptableObjects<StatsUpgradeSO>(upgradesPath);
    }

    public void DisplayRandomCards()
    {
        displayedUpgradesIndices = new HashSet<int>();
        cardsAmount = Mathf.Clamp(cardsAmount,0, upgradesList.Count);
        for (int i = 0; i < cardsAmount; i++)
        {
            StatsUpgradeSO randomUpgrade = GetRandomCard();
            GameObject upgradeCard = Instantiate(upgradeCardPrefab, upgradeCardsParent);
            upgradeCard.GetComponent<UpgradeCardUI>().statsUpgradeSO = randomUpgrade;
        }
    }

    private StatsUpgradeSO GetRandomCard()
    {
        int rand;
        do
        {
            rand = UnityEngine.Random.Range(0, upgradesList.Count);
        } while (displayedUpgradesIndices.Contains(rand));

        displayedUpgradesIndices.Add(rand);
        StatsUpgradeSO upgrade = upgradesList[rand];
        return upgrade;
    }

    public void RemoveUpgradeFromList(StatsUpgradeSO upgrade)
    {
        upgradesList.Remove(upgrade);
    }

    public void CleanUp()
    {
        for(int i = 0; i < upgradeCardsParent.childCount; i++)
        {
            Destroy(upgradeCardsParent.GetChild(i).gameObject);
        }
    }
}
