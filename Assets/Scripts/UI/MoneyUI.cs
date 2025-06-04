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

    private void OnDisable()
    {
        try
        {
            var gm = GameManager.Instance;
            if (gm != null && gm.Player != null && gm.Player.Money != null)
                gm.Player.Money.Unsubscribe(UpdateUI);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"[MoneyUI] OnDisable ����: {e.Message}");
        }
    }
}