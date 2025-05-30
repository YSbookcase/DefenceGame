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
            Debug.LogWarning("[MoneyUI] GameManager �Ǵ� PlayerManager�� ���� �غ���� �ʾҽ��ϴ�.");
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