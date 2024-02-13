using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CardSO")]
public class CardSO : ScriptableObject
{
    public string cardName;
    public string discription;
    public Sprite icon;

    public int weight;
}
