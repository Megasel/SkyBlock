using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SlotData
{
    public int itemId;
    public int amount;
}


[System.Serializable]
public class InventoryData
{
    public List<SlotData> mainSlots;
    public List<SlotData> additionalSlots;
}

public class InventoryController : MonoBehaviour
{
    public Slot[,] MainSlots { get; private set; }
    public Slot[,] AdditionalSlots { get; private set; }
    [SerializeField] private Inventory inventory;

    [SerializeField] private GameObject slotPref;
    [SerializeField] private Transform mainSlotGrid;
    [SerializeField] private Transform additionalSlotsGrid;
    [SerializeField] private InventoryWindow inventoryWindow;
    private bool isCellsCreated = false;

    private void Start()
    {
        Init();
        inventoryWindow.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (inventory == null)
        {
            inventory = FindAnyObjectByType<Inventory>();
        }
    }

    public void Init()
    {
        
        if (!isCellsCreated)
        {
            MainSlots = new Slot[1, 9];
            AdditionalSlots = new Slot[3, 9];
            CreateSlotsPrefabs();
            isCellsCreated = true;
        }

        
        if (PlayerPrefs.HasKey("InventoryData"))
        {
            LoadInventory();
        }
        else
        {
            SaveInventory();
            LoadInventory();
        }
        UpdateHotPanel();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SaveInventory();
            PrintSlots(); 
        }

        if (Input.GetKeyDown(KeyCode.N)) 
        {
            Item newItem = ItemsManager.Instance.Items[1]; 
            AddItemToFirstEmptySlot(newItem, 5); 
        }
    }

    private void CreateSlotsPrefabs()
    {
        if (slotPref == null)
        {
            Debug.LogError("Slot prefab is not assigned!");
            return;
        }

        
        for (int i = 0; i < MainSlots.GetLength(1); i++)
        {
            var slot = Instantiate(slotPref, mainSlotGrid, false);
            slotPref.name = "Slot" + i.ToString();
            if (slot == null)
            {
                Debug.LogError("Failed to instantiate slot prefab for main slots.");
                continue;
            }
            MainSlots[0, i] = slot.AddComponent<Slot>();
        }

        
        for (int i = 0; i < AdditionalSlots.GetLength(0); i++)
        {
            for (int k = 0; k < AdditionalSlots.GetLength(1); k++)
            {
                var slot = Instantiate(slotPref, additionalSlotsGrid, false);
                slotPref.name = "Slot" + i.ToString() + ", " + k.ToString();
                if (slot == null)
                {
                    Debug.LogError("Failed to instantiate slot prefab for additional slots.");
                    continue;
                }
                AdditionalSlots[i, k] = slot.AddComponent<Slot>();
            }
        }
    }




    public void SaveInventory()
    {
        InventoryData inventoryData = new InventoryData
        {
            mainSlots = new List<SlotData>(),
            additionalSlots = new List<SlotData>()
        };

        
        for (int i = 0; i < MainSlots.GetLength(1); i++)
        {
            Slot slot = MainSlots[0, i];
           
            if (slot.HasItem)
            {
                int id = -999;
                foreach(Item item in ItemsManager.Instance.Items)
                {
                    if(item.Name == slot.Item.Item.Name)
                    {
                        id = ItemsManager.Instance.Items.IndexOf(item);
                        break;
                    }
                }
                SlotData slotData = new SlotData
                {
                    itemId = id,  
                    amount = slot.Item.Amount
                };
                inventoryData.mainSlots.Add(slotData);
            }
            else
            {
                inventoryData.mainSlots.Add(new SlotData { itemId = -1, amount = 0 });
            }
        }

        
        for (int i = 0; i < AdditionalSlots.GetLength(0); i++)
        {
            for (int k = 0; k < AdditionalSlots.GetLength(1); k++)
            {

                Slot slot = AdditionalSlots[i, k];
               
                if (slot.HasItem)
                {
                    int id = -999;
                    foreach (Item item in ItemsManager.Instance.Items)
                    {
                        if (item.Name == slot.Item.Item.Name)
                        {
                            id = ItemsManager.Instance.Items.IndexOf(item);
                            break;
                        }
                    }
                    SlotData slotData = new SlotData
                    {
                        itemId = id,  
                        amount = slot.Item.Amount
                    };
                    inventoryData.additionalSlots.Add(slotData);
                }
                else
                {
                    inventoryData.additionalSlots.Add(new SlotData { itemId = -1, amount = 0 });
                }
            }
        }

        
        string json = JsonUtility.ToJson(inventoryData);

        PlayerPrefs.SetString("InventoryData", json);
        PlayerPrefs.Save();
        UpdateHotPanel();
    }

    public void UpdateHotPanel()
    {
        
        if (inventory == null || inventory.hotInventorySlots == null || inventory.hotInventoryCounts == null)
        {
            Debug.LogError("Inventory or hot inventory UI elements are not initialized!");
            return;
        }

        
        if (MainSlots == null || MainSlots.GetLength(1) < inventory.hotInventorySlots.Length)
        {
            Debug.LogError("Main slots are not initialized or not enough slots available!");
            return;
        }

        
        for (int i = 0; i < inventory.hotInventorySlots.Length; i++)
        {
            
            if (MainSlots[0, i] != null && MainSlots[0, i].HasItem)
            {
                inventory.hotInventorySlots[i].sprite = MainSlots[0, i].Item.Item.Sprite;  
                inventory.hotInventorySlots[i].color = Color.white;  
                inventory.hotInventoryCounts[i].text = MainSlots[0, i].Item.Amount.ToString();  
            }
            else
            {
                
                inventory.hotInventorySlots[i].sprite = inventory.emptySlot;  
                inventory.hotInventorySlots[i].color = new Color(1, 1, 1, 0);  
                inventory.hotInventoryCounts[i].text = "";  
            }
        }

        inventory.UpdateHeldItem();  
        inventory.ShowHandOrBlock();  
    }


    public void LoadInventory()
    {
        string json = PlayerPrefs.GetString("InventoryData");
        InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);

        
        for (int i = 0; i < inventoryData.mainSlots.Count; i++)
        {
            SlotData slotData = inventoryData.mainSlots[i];
            if (slotData.itemId != -1)
            {
                Item item = ItemsManager.Instance.Items[slotData.itemId];
                MainSlots[0, i].SetItem(new ItemInSlot(item, slotData.amount));
            }
        }

        
        int index = 0;
        for (int i = 0; i < AdditionalSlots.GetLength(0); i++)
        {
            for (int k = 0; k < AdditionalSlots.GetLength(1); k++, index++)
            {
                SlotData slotData = inventoryData.additionalSlots[index];
                if (slotData.itemId != -1)
                {
                    Item item = ItemsManager.Instance.Items[slotData.itemId];
                    AdditionalSlots[i, k].SetItem(new ItemInSlot(item, slotData.amount));
                }
            }
        }
    }

    
    
    public void AddItemToFirstEmptySlot(Item item, int blockId)
    {
        
        if (!isCellsCreated)
        {
            Debug.LogError("Слоты инвентаря не были созданы!");
            return;
        }

        
        
        for (int i = 0; i < MainSlots.GetLength(1); i++)
        {
            if (MainSlots[0, i].HasItem && MainSlots[0, i].Item.Item.Name == item.Name)
            {
                
                MainSlots[0, i].Item.Amount += 1;
                
                
                var slotData = new SlotData
                {
                    itemId = blockId,  
                    amount = MainSlots[0, i].Item.Amount
                };
                MainSlots[0, i].RefreshUI();
                UpdateHotPanel(); 
                return; 
            }
        }

        
        for (int i = 0; i < AdditionalSlots.GetLength(0); i++)
        {
            for (int k = 0; k < AdditionalSlots.GetLength(1); k++)
            {
                if (AdditionalSlots[i, k].HasItem && AdditionalSlots[i, k].Item.Item.Name == item.Name)
                {
                    
                    AdditionalSlots[i, k].Item.Amount += 1;
                   
                    
                    var slotData = new SlotData
                    {
                        itemId = blockId,  
                        amount = AdditionalSlots[i, k].Item.Amount
                    };
                    AdditionalSlots[i, k].RefreshUI();
                    UpdateHotPanel(); 
                    return; 
                }
            }
        }

        
        
        for (int i = 0; i < MainSlots.GetLength(1); i++)
        {
            if (!MainSlots[0, i].HasItem)
            {
                MainSlots[0, i].SetItem(new ItemInSlot(item, 1));

                
                var slotData = new SlotData
                {
                    itemId = blockId,  
                    amount = 1
                };

                UpdateHotPanel(); 
                return; 
            }
        }

        
        for (int i = 0; i < AdditionalSlots.GetLength(0); i++)
        {
            for (int k = 0; k < AdditionalSlots.GetLength(1); k++)
            {
                if (!AdditionalSlots[i, k].HasItem)
                {
                    AdditionalSlots[i, k].SetItem(new ItemInSlot(item, 1));

                    
                    var slotData = new SlotData
                    {
                        itemId = blockId,  
                        amount = 1
                    };

                    UpdateHotPanel(); 
                    return; 
                }
            }
        }
        
        
        Debug.Log("Инвентарь заполнен, не удалось добавить предмет.");
    }
    public bool CanCraftItem(CraftRecipe recipe)
    {
        if (recipe == null || recipe.Ingredients == null)
        {
            Debug.LogWarning("Recipe or Ingredients is null.");
            return false;
        }

        foreach (var ingredient in recipe.Ingredients)
        {
            if (ingredient.Key == null)
            {
                Debug.LogWarning("Ingredient key is null.");
                continue;
            }

            int requiredAmount = ingredient.Value;
            int availableAmount = GetItemAmount(ingredient.Key); 

            if (availableAmount < requiredAmount)
            {
                return false; 
            }
        }

        return true; 
    }

    public int GetItemAmount(Item item)
    {
        int amount = 0;

        
        for (int i = 0; i < MainSlots.GetLength(1); i++)
        {
            if (MainSlots[0, i].HasItem && MainSlots[0, i].Item.Item == item)
            {
                amount += MainSlots[0, i].Item.Amount;
            }
        }

        
        for (int i = 0; i < AdditionalSlots.GetLength(0); i++)
        {
            for (int j = 0; j < AdditionalSlots.GetLength(1); j++)
            {
                if (AdditionalSlots[i, j].HasItem && AdditionalSlots[i, j].Item.Item == item)
                {
                    amount += AdditionalSlots[i, j].Item.Amount;
                }
            }
        }

        return amount; 
    }

    public void AddItemToFirstEmptySlot(Item item, int blockId, int amount)
    {
        bool isTool = item is ToolItem; 

        
        if (isTool)
        {
            
            for (int i = 0; i < MainSlots.GetLength(1); i++)
            {
                if (!MainSlots[0, i].HasItem)
                {
                    MainSlots[0, i].SetItem(new ItemInSlot(item, amount)); 
                    UpdateHotPanel();
                    SaveInventory();
                    return;
                }
            }

            
            for (int i = 0; i < AdditionalSlots.GetLength(0); i++)
            {
                for (int k = 0; k < AdditionalSlots.GetLength(1); k++)
                {
                    if (!AdditionalSlots[i, k].HasItem)
                    {
                        AdditionalSlots[i, k].SetItem(new ItemInSlot(item, amount)); 
                        UpdateHotPanel();
                        SaveInventory();
                        return;
                    }
                }
            }
        }
        else
        {
            
            for (int i = 0; i < MainSlots.GetLength(1); i++)
            {
                if (MainSlots[0, i].HasItem && MainSlots[0, i].Item.Item.Name == item.Name)
                {
                    MainSlots[0, i].Item.Amount += amount;
                    MainSlots[0, i].RefreshUI();
                    UpdateHotPanel();
                    SaveInventory();
                    return;
                }
            }

            for (int i = 0; i < AdditionalSlots.GetLength(0); i++)
            {
                for (int k = 0; k < AdditionalSlots.GetLength(1); k++)
                {
                    if (AdditionalSlots[i, k].HasItem && AdditionalSlots[i, k].Item.Item.Name == item.Name)
                    {
                        AdditionalSlots[i, k].Item.Amount += amount;
                        AdditionalSlots[i, k].RefreshUI();
                        UpdateHotPanel();
                        SaveInventory();
                        return;
                    }
                }
            }

            
            for (int i = 0; i < MainSlots.GetLength(1); i++)
            {
                if (!MainSlots[0, i].HasItem)
                {
                    MainSlots[0, i].SetItem(new ItemInSlot(item, amount));
                    UpdateHotPanel();
                    SaveInventory();
                    return;
                }
            }

            for (int i = 0; i < AdditionalSlots.GetLength(0); i++)
            {
                for (int k = 0; k < AdditionalSlots.GetLength(1); k++)
                {
                    if (!AdditionalSlots[i, k].HasItem)
                    {
                        AdditionalSlots[i, k].SetItem(new ItemInSlot(item, amount));
                        UpdateHotPanel();
                        SaveInventory();
                        return;
                    }
                }
            }
        }

        
        Debug.Log("Инвентарь заполнен, не удалось добавить предмет.");
    }



    
    public void CraftItem(Item craftedItem)
    {
        
        foreach (var ingredient in craftedItem.Recipe.ItemsOrder)
        {
            if (ingredient == null)
                continue;

            RemoveItemFromInventory(ingredient); 
        }

        
        AddItemToFirstEmptySlot(craftedItem, ItemsManager.Instance.Items.IndexOf(craftedItem), craftedItem.Recipe.Amount); 
    }

    
    public void RemoveItemFromInventory(Item item)
    {
        foreach (var slot in MainSlots)
        {
            if (slot == null) 
                continue;

            if (slot.HasItem && slot.Item.Item.Name == item.Name)
            {
                slot.Item.Amount--;
                if (slot.Item.Amount == 0)
                {
                    slot.ResetItem(); 
                }
                slot.RefreshUI();
                return;
            }
        }

        foreach (var slot in AdditionalSlots)
        {
            if (slot == null) 
                continue;

            if (slot.HasItem && slot.Item.Item.Name == item.Name)
            {
                slot.Item.Amount--;
                if (slot.Item.Amount == 0)
                {
                    slot.ResetItem(); 
                }
                slot.RefreshUI();
                return;
            }
        }
    }


    
    private List<ItemInSlot> GetAllItemsInInventory()
    {
        List<ItemInSlot> items = new List<ItemInSlot>();

        for (int i = 0; i < MainSlots.GetLength(1); i++)
        {
            if (MainSlots[0, i].HasItem)
            {
                items.Add(MainSlots[0, i].Item);
            }
        }

        for (int i = 0; i < AdditionalSlots.GetLength(0); i++)
        {
            for (int k = 0; k < AdditionalSlots.GetLength(1); k++)
            {
                if (AdditionalSlots[i, k].HasItem)
                {
                    items.Add(AdditionalSlots[i, k].Item);
                }
            }
        }

        return items;
    }

    
    private void PrintSlots()
    {
        Debug.Log("Main Slots:");
        for (int i = 0; i < MainSlots.GetLength(1); i++)
        {
            if (MainSlots[0, i].HasItem)
            {
                Debug.Log($"Slot {i}: {MainSlots[0, i].Item.Item.Name}, Amount: {MainSlots[0, i].Item.Amount}");
            }
            else
            {
                Debug.Log($"Slot {i}: Empty");
            }
        }
        
        Debug.Log("Additional Slots:");
        for (int i = 0; i < AdditionalSlots.GetLength(0); i++)
        {
            for (int k = 0; k < AdditionalSlots.GetLength(1); k++)
            {
                if (AdditionalSlots[i, k].HasItem)
                {
                    Debug.Log($"Slot {i},{k}: {AdditionalSlots[i, k].Item.Item.Name}, Amount: {AdditionalSlots[i, k].Item.Amount}");
                }
                else
                {
                    Debug.Log($"Slot {i},{k}: Empty");
                }
            }
        }
    }
}
