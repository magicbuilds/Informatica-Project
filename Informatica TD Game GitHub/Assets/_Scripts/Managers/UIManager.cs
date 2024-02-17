using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    //EnemiesLeft
    [SerializeField] private TextMeshProUGUI enemiesLeftText;

    //ExtraCardInformation
    [SerializeField] private GameObject extraCardInformationUI;

    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardStatsText;
    [SerializeField] private TextMeshProUGUI cardDiscription;
    [SerializeField] private Image cardIcon;


    private void Awake()
    {
        Instance = this;
    }

    public void UpdateEnemysLeftUI(int enemysLeft)
    {
        enemiesLeftText.text = enemysLeft.ToString();
    }

    public void ActivateExtraInformationUI(CardSO card)
    {
        foreach (GameObject spawnedCard in CardDrawManager.Instance.spawnedCards)
        {
            spawnedCard.SetActive(false);
        }

        extraCardInformationUI.SetActive(true);

        cardNameText.text = card.cardName;
        cardStatsText.text = card.stats;
        cardDiscription.text = card.discription;
        cardIcon.sprite = card.icon;
    }

    public void DeactivateExtraInformationUI()
    {
        extraCardInformationUI.SetActive(false);

        foreach (GameObject spawnedCard in CardDrawManager.Instance.spawnedCards)
        {
            spawnedCard.SetActive(true);
        }
    }
}
