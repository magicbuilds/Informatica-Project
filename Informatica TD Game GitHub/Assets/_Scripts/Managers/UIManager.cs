using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI enemysLeftText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateEnemysLeftUI(int enemysLeft)
    {
        enemysLeftText.text = enemysLeft.ToString();
    }
}
