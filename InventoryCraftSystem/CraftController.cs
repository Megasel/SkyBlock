using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CraftController : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPref;
    [SerializeField]
    private Transform craftGrid;
    public CraftSlot[,] CraftTable { get; private set; }

    public CraftResultSlot ResultSlot;
    public bool HasResultItem => ResultSlot.Item != null;
    private bool isCellsCreated = false;
    [SerializeField] private int slotsCount;
    public void Init()
    {
        if (!isCellsCreated)
        {
            CraftTable = new CraftSlot[slotsCount, slotsCount];
            CreateSlotsPrefabs();
            isCellsCreated = true; 
        }
    }

    private void CreateSlotsPrefabs()
    {
        for (int i = 0; i < CraftTable.GetLength(0); i++)
        {
            for (int k = 0; k < CraftTable.GetLength(1); k++)
            {
                
                
                    var slot = Instantiate(slotPref, craftGrid, false);
                    CraftTable[i, k] = slot.AddComponent<CraftSlot>();
                
            }
        }
    }

    public void CheckCraft()
    {
        ItemInSlot newItem = null;
        int currRecipeW = 0, currRecipeH = 0;
        int currRecipeWStartIndex = -1, currRecipeHStartIndex = -1;

        
        for (int i = 0; i < CraftTable.GetLength(0); i++)
        {
            for (int k = 0; k < CraftTable.GetLength(1); k++)
            {
                if (CraftTable[i, k].HasItem)
                {
                    if (currRecipeHStartIndex == -1)
                        currRecipeHStartIndex = i;
                    currRecipeH++;
                    break;
                }
            }
        }

        
        for (int i = 0; i < CraftTable.GetLength(1); i++)
        {
            for (int k = 0; k < CraftTable.GetLength(0); k++)
            {
                if (CraftTable[k, i].HasItem)
                {
                    if (currRecipeWStartIndex == -1)
                        currRecipeWStartIndex = i;
                    currRecipeW++;
                    break;
                }
            }
        }

        
        if (currRecipeHStartIndex == -1 || currRecipeWStartIndex == -1 ||
            currRecipeH == 0 || currRecipeW == 0)
        {
            ResultSlot.ResetItem();
            return;
        }

        
        var craftOrder = new Item[currRecipeH * currRecipeW];
        for (int orderId = 0, i = currRecipeHStartIndex; i < currRecipeHStartIndex + currRecipeH; i++)
        {
            for (int k = currRecipeWStartIndex; k < currRecipeWStartIndex + currRecipeW; k++)
            {
                craftOrder[orderId++] = CraftTable[i, k].Item?.Item;
            }
        }

        
        foreach (var item in ItemsManager.Instance.Items)
        {
            if (item.HasRecipe && item.Recipe.ItemsOrder != null && craftOrder != null)
            {
                
                if (item.Recipe.ItemsOrder.Length == craftOrder.Length)
                {
                    
                    if (item.Recipe.ItemsOrder.SequenceEqual(craftOrder))
                    {
                        newItem = new ItemInSlot(item, item.Recipe.Amount);
                        break;
                    }
                }
            }
        }

        if (newItem != null)
            ResultSlot.SetItem(newItem);
        else
            ResultSlot.ResetItem();
    }

    public void CraftItem()
    {
        for (int i = 0; i < CraftTable.GetLength(0); i++)
        {
            for (int k = 0; k < CraftTable.GetLength(1); k++)
            {
                if (CraftTable[i, k].Item != null)
                    CraftTable[i, k].DecreaseItemAmount(1);
            }
        }
        CheckCraft();
    }
}
