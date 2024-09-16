public class ItemInSlot
{
    public Item Item { get; private set; }
    public int Amount { get; set; }
    public int Prochnost { get; set; } 
    public ItemInSlot(Item item, int amount, int prochnost = 10)     {
        Item = item;
        Amount = amount;
        Prochnost = prochnost;
    }
}
