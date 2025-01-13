using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    private Dictionary<string, (DrinkSlot slot, int quantity)> purchasedDrinks = new Dictionary<string, (DrinkSlot slot, int quantity)>();

    public void AddPurchasedDrink(DrinkSlot slot)
    {
        if (purchasedDrinks.ContainsKey(slot.DrinkName))
        {
            purchasedDrinks[slot.DrinkName] = (slot, purchasedDrinks[slot.DrinkName].quantity + 1);
        }
        else
        {
            purchasedDrinks.Add(slot.DrinkName, (slot, 1));
        }
    }

    public Dictionary<string, (DrinkSlot slot, int quantity)> GetPurchasedDrinks()
    {
        return purchasedDrinks;
    }

    public bool IsDrinkRemoved(string drinkName)
    {
        return !purchasedDrinks.ContainsKey(drinkName);
    }

    public void RemovePurchasedDrink(string drinkName)
    {
        if (purchasedDrinks.ContainsKey(drinkName))
        {
            purchasedDrinks.Remove(drinkName);
        }
    }

    public int GetPurchasedDrinksCount()
    {
        int total = 0;
        foreach (var entry in purchasedDrinks)
        {
            total += entry.Value.quantity;
        }
        return total;
    }
}
