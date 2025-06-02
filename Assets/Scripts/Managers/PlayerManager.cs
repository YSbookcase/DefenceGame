using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class PlayerManager : Singleton<PlayerManager>
{
    public ObservableProperty<int> Money { get; private set; } = new(0);


    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.M))
        {
            AddMoney(5000);
        }
    }

    private void OnDestroy()
    {
        Debug.LogWarning("PlayerManager가 Destroy 되었습니다!");
    }

    private void OnDisable()
    {
        Debug.LogWarning("PlayerManager가 비활성화되었습니다!");
    }



    public void AddMoney(int amount)
    {
        Money.Value += amount;
    }

    public bool TrySpendMoney(int amount)
    {
        if (Money.Value >= amount)
        {
            Money.Value -= amount;
            return true;
        }
        return false;
    }
}