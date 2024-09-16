using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipe
{
    public Item[,] Items { get; private set; }     public int Amount { get; private set; }     public Item[] ItemsOrder { get; private set; }     public Dictionary<Item, int> Ingredients { get; private set; } 
        public CraftRecipe(Item[,] items, int amount)
    {
        Items = items;
        Amount = amount;
        ItemsOrder = new Item[Items.Length];
        for (int orderId = 0, i = 0; i < Items.GetLength(0); i++)
        {
            for (int k = 0; k < Items.GetLength(1); k++)
            {
                ItemsOrder[orderId++] = Items[i, k];
            }
        }

                Ingredients = new Dictionary<Item, int>();
        foreach (Item item in ItemsOrder)
        {
            if (item == null) continue;

            if (Ingredients.ContainsKey(item))
            {
                Ingredients[item]++;
            }
            else
            {
                Ingredients[item] = 1;
            }
        }
    }

        public CraftRecipe(Dictionary<Item, int> ingredients, int amount)
    {
        Ingredients = ingredients;
        Amount = amount;
    }
}
