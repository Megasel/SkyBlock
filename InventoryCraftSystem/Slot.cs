using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image image;
    public Image itemImage;
    private TMP_Text itemAmount;
    private InventoryController inventoryController;
    [SerializeField] private Color defaultColor = new Color32(140, 140, 140, 0);
    [SerializeField] Color highlightedColor = new Color32(121, 121, 121, 10);
    public ItemInSlot Item { get; private set; }
    public bool HasItem => Item != null;

    public Chest chest; 
    public Inventory inventory;
    private void Awake()
    {
        
        image = GetComponent<Image>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemAmount = transform.GetChild(1).GetComponent<TMP_Text>();

        itemImage.preserveAspect = true;
    }

    private void OnEnable()
    {
        if (inventoryController == null)
        {
            inventoryController = FindAnyObjectByType<InventoryController>();
        }
        
    }
    
    public void SetItem(ItemInSlot item)
    {
        Item = item;
        RefreshUI();
    }

    public void AddItem(ItemInSlot item, int amount)
    {
        item.Amount -= amount;
        if (!HasItem)
        {
            SetItem(new ItemInSlot(item.Item, amount));
        }
        else
        {
            Item.Amount += amount;
            RefreshUI();
        }
    }

    public void ResetItem()
    {
        
        if (itemImage == null || itemAmount == null)
        {
            Debug.LogWarning("UI Elements not initialized in Slot. Skipping Reset.");
            return;
        }

        Item = null;
        RefreshUI();
    }

    public void RefreshUI()
    {
        
        if (itemImage == null || itemAmount == null)
        {
            Debug.LogWarning("UI Elements not initialized in Slot. Skipping Refresh.");
            return;
        }

        
        itemImage.gameObject.SetActive(HasItem);
      
            itemImage.sprite = Item?.Item.Sprite;
        
        
       

        itemAmount.gameObject.SetActive(HasItem && Item.Amount > 1);
        itemAmount.text =  Item?.Amount.ToString();
    }
    public void OnPointerDown(PointerEventData eventData) { 
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }
        else
        {
            RightClick();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public virtual void LeftClick()
    {
        var currItem = InventoryWindow.Instance.CurrentItem;

        if (HasItem)
        {
            Debug.Log("Curr");
            if (currItem == null || Item.Item != currItem.Item)
            {
                InventoryWindow.Instance.SetCurrentItem(Item);
                ResetItem();
            }
            else
            {
                InventoryWindow.Instance.targetSlot.AddItem(currItem, currItem.Amount);
                InventoryWindow.Instance.CheckCurrentItem();
                return;
            }
        }
        else
        {
            InventoryWindow.Instance.ResetCurrentItem();
        }

        if (currItem != null)
        {
            Debug.Log("CurrNull");
            InventoryWindow.Instance.targetSlot.SetItem(currItem);

            
            if (chest != null)
            {
                chest.SaveChestContents();
            }

            inventoryController.UpdateHotPanel();
        }
        inventoryController.SaveInventory();
    }

    public virtual void RightClick()
    {
        if (!InventoryWindow.Instance.HasCurrentItem)
        {
            return;
        }

        if (!HasItem || InventoryWindow.Instance.CurrentItem.Item == Item.Item)
        {
            InventoryWindow.Instance.targetSlot.AddItem(InventoryWindow.Instance.CurrentItem, 1);
            InventoryWindow.Instance.CheckCurrentItem();
        }

        
        if (chest != null)
        {
            chest.SaveChestContents();
        }

        inventoryController.UpdateHotPanel();
        inventoryController.SaveInventory();
    }
    public void UseItem()
    {
        if (HasItem)
        {
                                                                                                                                            }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryWindow.Instance.targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    
    public void ClearUI()
    {
        if (itemImage == null || itemAmount == null)
        {
            Debug.LogWarning("UI Elements not initialized in Slot. Skipping ClearUI.");
            return;
        }

        itemImage.sprite = null;
        itemAmount.text = string.Empty;
        itemImage.gameObject.SetActive(false);
        itemAmount.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            var currItem = InventoryWindow.Instance.CurrentItem;

            if (HasItem)
            {
                if (currItem != null && Item.Item == currItem.Item)
                {
                    Debug.Log(currItem.Amount);
                    InventoryWindow.Instance.targetSlot.AddItem(currItem, currItem.Amount);
                    InventoryWindow.Instance.CheckCurrentItem();
                }
                else
                {
                    InventoryWindow.Instance.SetCurrentItem(Item);
                    InventoryWindow.Instance.targetSlot.ResetItem();
                }
            }
            else
            {
                if (currItem != null)
                {
                    InventoryWindow.Instance.targetSlot.SetItem(currItem);

                    if (InventoryWindow.Instance.targetSlot is CraftSlot)
                    {
                        InventoryWindow.Instance.CraftController.CheckCraft();
                    }

                    if (chest != null)
                    {
                        chest.SaveChestContents();
                    }

                    inventoryController.UpdateHotPanel();
                }

                InventoryWindow.Instance.ResetCurrentItem();
            }

            inventoryController.SaveInventory();
        }
    }


}
