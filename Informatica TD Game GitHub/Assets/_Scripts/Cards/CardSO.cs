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
}
