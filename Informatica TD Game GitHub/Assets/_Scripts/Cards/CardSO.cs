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

    public Color typeColor;

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

    public string GetCost()
    {
        return "Cost: " + baseCost.ToString();
    }

    public string GetStats()
    {
        if (cardType == CardType.Tower)
        {
            string stats = null;

            float damage = UpgradeManager.Instance.ReturnValueOf(tower.towerType, UpgradeSO.UpgradeType.Damage);
            float fireRate = UpgradeManager.Instance.ReturnValueOf(tower.towerType, UpgradeSO.UpgradeType.FireRate);
            float range = UpgradeManager.Instance.ReturnValueOf(tower.towerType, UpgradeSO.UpgradeType.Range);
            float special = UpgradeManager.Instance.ReturnValueOf(tower.towerType, UpgradeSO.UpgradeType.Special);

            if (damage > 0) stats = "Damage: " + damage;
            if (fireRate > 0 && stats != null) stats += "\nFire Rate: " + fireRate;
            if (fireRate > 0 && stats == null) stats = "Fire Rate " + fireRate;
            if (range > 0 && stats != null) stats += "\nRange: " + range;
            if (range > 0 && stats == null) stats = "Range: " + range;
            if (special > 0 && stats != null) stats += "\nSpecial: " + special;
            if (special > 0 && stats == null) stats = "Special: " + special;

            return stats;
        }
        else if (cardType == CardType.Upgrade)
        {
            string upgradeType = "";

            switch (upgrade.upgradeType)
            {
                case UpgradeSO.UpgradeType.Damage:
                    upgradeType = "Damage: ";
                    break;
                case UpgradeSO.UpgradeType.FireRate:
                    upgradeType = "Fire Rate: ";
                    break;
                case UpgradeSO.UpgradeType.Range:
                    upgradeType = "Range: ";
                    break;
                case UpgradeSO.UpgradeType.Special:
                    upgradeType = "Special: ";
                    break;
            }

            string upgradePower = upgrade.upgradePower.ToString();

            string stats = upgradeType + "( +" + upgradePower + ")";
            return stats;
        }

        else return null;
    }

    public string GetCardType()
    {
        if (cardType == CardType.Tower)
        {
            return "Type: Tower";
        }
        else if (cardType == CardType.Upgrade)
        {
            return "Type: Upgrade";
        }
        else return null;
    }

    public Color GetCardTypeColor()
    {
        return typeColor;
    }
}
