using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade")]
public abstract class UpgradeSO : SerializedScriptableObject
{
    public Sprite icon { get; private set; }
    public string upgradeName;
    [SerializeField] public string upgradeDescription { get; private set; }

    [Button]
    public abstract void DoUpgrade();
}



