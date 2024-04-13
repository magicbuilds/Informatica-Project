using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CardSO")]
public class CardSO : ScriptableObject
{
    public string cardName;

    public Rarity rarity;
    public CardType cardType;
    public TowerSO tower;
    public UpgradeSO upgrade;

    public float baseCost;

    public string discription;
    public Sprite icon;

    public int weight;

    public enum Rarity
    {
        common, uncommon, rare, epic, legendary, mythical
    }

    public enum CardType
    {
        Tower,
        Upgrade
    }

    public string GetStats()
    {
        if (cardType == CardType.Tower)
        {
            return "Range: " + UpgradeManager.Instance.ReturnValueOf(tower.towerType, UpgradeSO.UpgradeType.Range) + "\n" + "Damage: " +
                 UpgradeManager.Instance.ReturnValueOf(tower.towerType, UpgradeSO.UpgradeType.Damage) + "\n" + "Fire Rate: " +
                 UpgradeManager.Instance.ReturnValueOf(tower.towerType, UpgradeSO.UpgradeType.FireRate);
        }
        else return "Upgrade";
    }
}
