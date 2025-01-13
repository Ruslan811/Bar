using UnityEngine;

public class CoinService : ICoinService
{
    private const string CoinKey = "CoinBalance";
    private int _balance;

    public CoinService()
    {
        _balance = PlayerPrefs.GetInt(CoinKey, 0);
    }

    public int GetBalance()
    {
        return _balance;
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;

        _balance += amount;
        Save();
    }

    public bool SpendCoins(int amount)
    {
        if (amount > _balance) return false;

        _balance -= amount;
        Save();
        return true;
    }

    private void Save()
    {
        PlayerPrefs.SetInt(CoinKey, _balance);
        PlayerPrefs.Save();
    }
}

public interface ICoinService
{
    int GetBalance();
    void AddCoins(int amount);
    bool SpendCoins(int amount);
}
