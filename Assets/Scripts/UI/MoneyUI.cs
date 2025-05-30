using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.Player != null)
        {
            GameManager.Instance.Player.Money.Subscribe(UpdateUI);
            UpdateUI(GameManager.Instance.Player.Money.Value);
        }
        else
        {
            Debug.LogWarning("[MoneyUI] GameManager 또는 PlayerManager가 아직 준비되지 않았습니다.");
        }
    }



    private void UpdateUI(int money)
    {
        moneyText.text = $"{money}";
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Player.Money.Unsubscribe(UpdateUI);
    }
}