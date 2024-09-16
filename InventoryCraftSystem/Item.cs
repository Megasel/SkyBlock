using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item 
{
    public string Name { get; set; }
    public Sprite Sprite { get; set; }
    public CraftRecipe Recipe { get; set; }
    public bool HasRecipe => Recipe != null;
    public Item(string name, Sprite sprite, CraftRecipe recipe = null)
    {
        Name = name;
        Sprite = sprite;
        Recipe = recipe;
    }
}
