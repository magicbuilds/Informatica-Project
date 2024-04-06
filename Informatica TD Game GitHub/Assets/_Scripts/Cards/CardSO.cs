using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CardSO")]
public class CardSO : ScriptableObject
{
    public string cardName;

    public Rarity rarity;
    public TowerSO tower;

    public float baseCost;

    public string discription;
    public Sprite icon;

    public int weight;

    public enum Rarity
    {
        common, uncommon, rare, epic, legendary, mythical
    }
}
