using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager Instance;
    public List<Item> Items = new List<Item>();
    public List<Item> ItemsBook = new List<Item>();
    public Sprite[] ItemSprites;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (ItemSprites.Length > 0)
        {
            GenerateItems();
            GenerateBookItems();
        }
        else
        {
            Debug.LogError("ItemSprites array is empty! Please assign item sprites in the inspector.");
        }
    }
    private void GenerateBookItems()
    {
        ItemsBook.Clear();

        

        
        ItemsBook.Add(new Item("Oak", ItemSprites[0]));

        
        var woodenPlanksRecipe = new Dictionary<Item, int>
{
    { Items[0], 1 } 
};
        ItemsBook.Add(new Item("Wooden Planks", ItemSprites[1], new CraftRecipe(woodenPlanksRecipe, 4)));

        var stickRecipe = new Dictionary<Item, int>
{
    { Items[1], 2 } 
};
        ItemsBook.Add(new Item("Stick", ItemSprites[2], new CraftRecipe(stickRecipe, 2)));

        var workBenchRecipe = new Dictionary<Item, int>
{
    { Items[1], 4 } 
};
        ItemsBook.Add(new Item("Workbench", ItemSprites[3], new CraftRecipe(workBenchRecipe, 1)));

        
        ItemsBook.Add(new Item("Stone", ItemSprites[4]));
        ItemsBook.Add(new Item("Wheat", ItemSprites[5]));

        
        var breadRecipe = new Dictionary<Item, int>
{
    { Items[5], 3 } 
};
        ItemsBook.Add(new Item("Bread", ItemSprites[6], new CraftRecipe(breadRecipe, 3)));

        ItemsBook.Add(new Item("FarmDirt", ItemSprites[7]));
        ItemsBook.Add(new Item("Pshenitsa", ItemSprites[8]));
        ItemsBook.Add(new Item("Gravel", ItemSprites[9]));
        ItemsBook.Add(new Item("Apple", ItemSprites[10]));
        ItemsBook.Add(new Item("OakSeeding", ItemSprites[11]));
        ItemsBook.Add(new Item("EmptyBucket", ItemSprites[12]));
        ItemsBook.Add(new Item("WaterBucket", ItemSprites[13]));
        ItemsBook.Add(new Item("LavaBucket", ItemSprites[14]));
        ItemsBook.Add(new Item("Dirt", ItemSprites[15]));

        
        var chestRecipe = new Dictionary<Item, int>
{
    { Items[1], 8 } 
};
        ItemsBook.Add(new Item("Chest", ItemSprites[17], new CraftRecipe(chestRecipe, 1)));

        ItemsBook.Add(new Item("Grass", ItemSprites[16]));
        ItemsBook.Add(new Item("Leaves", ItemSprites[18]));
        ItemsBook.Add(new Item("Ceeds", ItemSprites[19]));

        
        var woodenSwordRecipe = new Dictionary<Item, int>
{
    { Items[1], 2 }, 
    { Items[2], 1 }  
};
        ItemsBook.Add(new Item("WoodenSword", ItemSprites[20], new CraftRecipe(woodenSwordRecipe, 1)));

        var woodenShovelRecipe = new Dictionary<Item, int>
{
    { Items[1], 1 }, 
    { Items[2], 2 }  
};
        ItemsBook.Add(new Item("WoodenShovel", ItemSprites[21], new CraftRecipe(woodenShovelRecipe, 1)));

        var woodenPickaxeRecipe = new Dictionary<Item, int>
{
    { Items[1], 3 }, 
    { Items[2], 2 }  
};
        ItemsBook.Add(new Item("WoodenPickaxe", ItemSprites[22], new CraftRecipe(woodenPickaxeRecipe, 1)));

        var woodenHoeRecipe = new Dictionary<Item, int>
{
    { Items[1], 2 }, 
    { Items[2], 2 }  
};
        ItemsBook.Add(new Item("WoodenHoe", ItemSprites[23], new CraftRecipe(woodenHoeRecipe, 1)));

        var woodenAxeRecipe = new Dictionary<Item, int>
{
    { Items[1], 3 }, 
    { Items[2], 2 }  };
        ItemsBook.Add(new Item("WoodenAxe", ItemSprites[24], new CraftRecipe(woodenAxeRecipe, 1)));

                var stoneSwordRecipe = new Dictionary<Item, int>
{
    { Items[4], 2 },     { Items[2], 1 }  };
        ItemsBook.Add(new Item("StoneSword", ItemSprites[25], new CraftRecipe(stoneSwordRecipe, 1)));

        var stoneShovelRecipe = new Dictionary<Item, int>
{
    { Items[4], 1 },     { Items[2], 2 }  };
        ItemsBook.Add(new Item("StoneShovel", ItemSprites[26], new CraftRecipe(stoneShovelRecipe, 1)));

        var stonePickaxeRecipe = new Dictionary<Item, int>
{
    { Items[4], 3 },     { Items[2], 2 }  };
        ItemsBook.Add(new Item("StonePickaxe", ItemSprites[27], new CraftRecipe(stonePickaxeRecipe, 1)));

        var stoneHoeRecipe = new Dictionary<Item, int>
{
    { Items[4], 2 },     { Items[2], 2 }  };
        ItemsBook.Add(new Item("StoneHoe", ItemSprites[28], new CraftRecipe(stoneHoeRecipe, 1)));

        var stoneAxeRecipe = new Dictionary<Item, int>
{
    { Items[4], 3 },     { Items[2], 2 }  };
        ItemsBook.Add(new Item("StoneAxe", ItemSprites[29], new CraftRecipe(stoneAxeRecipe, 1)));

        ItemsBook.Add(new Item("Chestslot", ItemSprites[30]));
        ItemsBook.Add(new Item("WaterStart", ItemSprites[31]));
        ItemsBook.Add(new Item("LavaStart", ItemSprites[32]));

                var furnaceRecipe = new Dictionary<Item, int>
{
    { Items[4], 8 } };
        ItemsBook.Add(new Item("Furnace", ItemSprites[33], new CraftRecipe(furnaceRecipe, 1)));

                ItemsBook.Add(new Item("Coal", ItemSprites[34]));
        var coalBlockRecipe = new Dictionary<Item, int>
{
    { Items[34], 9 } };
        ItemsBook.Add(new Item("CoalBlock", ItemSprites[35], new CraftRecipe(coalBlockRecipe, 1)));

                var wheatBlockRecipe = new Dictionary<Item, int>
{
    { Items[5], 9 } };
        ItemsBook.Add(new Item("WheatBlock", ItemSprites[36], new CraftRecipe(wheatBlockRecipe, 1)));

                var polWoodRecipe = new Dictionary<Item, int>
{
    { Items[1], 3 } };
        ItemsBook.Add(new Item("PolWood", ItemSprites[37], new CraftRecipe(polWoodRecipe, 6)));

                var polStoneRecipe = new Dictionary<Item, int>
{
    { Items[4], 3 } };
        ItemsBook.Add(new Item("PolStone", ItemSprites[38], new CraftRecipe(polStoneRecipe, 6)));

                var woodUpstairsRecipe = new Dictionary<Item, int>
{
    { Items[1], 6 } };
        ItemsBook.Add(new Item("WoodUpstairs", ItemSprites[39], new CraftRecipe(woodUpstairsRecipe, 4)));

        var stoneUpstairsRecipe = new Dictionary<Item, int>
{
    { Items[4], 6 } };
        ItemsBook.Add(new Item("StoneUpstairs", ItemSprites[40], new CraftRecipe(stoneUpstairsRecipe, 4)));

                var wheatRecipe = new Dictionary<Item, int>
{
    { Items[36], 1 } };
        Items[5].Recipe = new CraftRecipe(wheatRecipe, 9);

        var coalRecipe = new Dictionary<Item, int>
{
    { Items[35], 1 } };
        Items[34].Recipe = new CraftRecipe(coalRecipe, 9);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log(JsonUtility.ToJson(Items));
        }
    }
    private void GenerateItems()
    {
        
      Items = new List<Item>();
       
        
        
                
                Items.Add(new Item("Oak", ItemSprites[0]));

                var woodenPlanksRecipe = new Item[,]
        {
            { Items[0] }
        };
        Items.Add(new Item("Wooden Planks", ItemSprites[1], new CraftRecipe(woodenPlanksRecipe, 4)));
        var stickRecipe = new Item[,]
        {
             { Items[1],},
             { Items[1]}
            
        };
        Items.Add(new Item("Stick", ItemSprites[2], new CraftRecipe(stickRecipe, 2)));
        var workBenchRecipe = new Item[,]
        {
            { Items[1], Items[1] },
            { Items[1], Items[1] }
        };

        Items.Add(new Item("Workbench", ItemSprites[3], new CraftRecipe(workBenchRecipe, 1)));
        
        Items.Add(new Item("Stone", ItemSprites[4]));
       
        Items.Add(new Item("Wheat", ItemSprites[5]));

        var breadRecipe = new Item[,]
        {
             { Items[5], Items[5],Items[5] },
        };
        Items.Add(new Item("Bread", ItemSprites[6],new CraftRecipe(breadRecipe, 3)));
        Items.Add(new Item("FarmDirt", ItemSprites[7]));
        Items.Add(new Item("Pshenitsa", ItemSprites[8]));
        Items.Add(new Item("Gravel", ItemSprites[9]));
        Items.Add(new Item("Apple", ItemSprites[10]));
        Items.Add(new Item("OakSeeding", ItemSprites[11]));
        Items.Add(new Item("EmptyBucket", ItemSprites[12]));
        Items.Add(new Item("WaterBucket", ItemSprites[13]));
        Items.Add(new Item("LavaBucket", ItemSprites[14]));
        Items.Add(new Item("Dirt", ItemSprites[15]));
       
        var ChestRecipe = new Item[,]
        {
            { Items[1], Items[1],Items[1] },
            {Items[1], null, Items[1] },
             { Items[1],Items[1], Items[1] }
        };
        Items.Add(new Item("Grass", ItemSprites[16]));
        Items.Add(new Item("Chest", ItemSprites[17], new CraftRecipe(ChestRecipe, 1)));
        Items.Add(new Item("Leaves", ItemSprites[18]));
        
        Items.Add(new Item("Ceeds", ItemSprites[19]));
       
        var WoodenSwordRecipe = new Item[,]
        {
            {  Items[1] },
            {  Items[1] },
            {  Items[2] }
        };
        var WoodenShowelRecipe = new Item[,]
        {
             {  Items[1] },
            {  Items[2] },
            {  Items[2] }
        };
        var WoodenPickaxeRecipe = new Item[,]
        {
            { Items[1], Items[1],Items[1] },
            {null, Items[2], null },
             { null, Items[2], null }
        };
        var WoodenHoeRecipe = new Item[,]
        {
            { Items[1],Items[1] },
            {Items[2], null},
             {  Items[2], null }
        };
        var WoodenAxeRecipe = new Item[,]
        {
           {  Items[1],Items[1] },
            {  Items[2], Items[1]},
             {  Items[2], null}
        };
        var StoneSwordRecipe = new Item[,]
        {
            {  Items[4] },
            {  Items[4] },
            {  Items[2] }
        };
        var StoneShowelRecipe = new Item[,]
        {
           {  Items[4] },
            {  Items[2] },
            {  Items[2] }
        };
        var StonePickaxeRecipe = new Item[,]
        {
            { Items[4], Items[4],Items[4] },
            {null, Items[2], null },
             { null, Items[2], null }
        };
        var StoneHoeRecipe = new Item[,]
        {
            { Items[4],Items[4] },
            {Items[2], null},
             {  Items[2], null }
        };
        var StoneAxeRecipe = new Item[,]
        {
           {  Items[4],Items[4] },
            {  Items[2], Items[4]},
             {  Items[2], null}
        };
        
        Items.Add(new Item("WoodenSword", ItemSprites[20],new CraftRecipe(WoodenSwordRecipe, 1)));
        Items.Add(new Item("WoodenShowel", ItemSprites[21], new CraftRecipe(WoodenShowelRecipe, 1)));
        Items.Add(new Item("WoodenPickaxe", ItemSprites[22], new CraftRecipe(WoodenPickaxeRecipe, 1)));
        Items.Add(new Item("WoodenHoe", ItemSprites[23], new CraftRecipe(WoodenHoeRecipe, 1)));
        Items.Add(new Item("WoodenAxe", ItemSprites[24], new CraftRecipe(WoodenAxeRecipe, 1)));
        Items.Add(new Item("StoneSword", ItemSprites[25], new CraftRecipe(StoneSwordRecipe, 1)));
        Items.Add(new Item("StoneShowel", ItemSprites[26], new CraftRecipe(StoneShowelRecipe, 1)));
        Items.Add(new Item("StonePickaxe", ItemSprites[27], new CraftRecipe(StonePickaxeRecipe, 1)));
        Items.Add(new Item("StoneHoe", ItemSprites[28], new CraftRecipe(StoneHoeRecipe, 1)));
        Items.Add(new Item("StoneAxe", ItemSprites[29], new CraftRecipe(StoneAxeRecipe, 1)));
        Items.Add(new Item("Chestslot", ItemSprites[30]));
        Items.Add(new Item("WaterStart", ItemSprites[31]));
        Items.Add(new Item("LavaStart", ItemSprites[32]));
        var FurnaceRecipe = new Item[,]
        {
             { Items[4], Items[4],Items[4] },
            {Items[4], null, Items[4] },
             { Items[4],Items[4], Items[4] }
        };
        Items.Add(new Item("Furnace", ItemSprites[33], new CraftRecipe(FurnaceRecipe,1)));
       
        Items.Add(new Item("Coal", ItemSprites[34]));
        var coalBlockRecipe = new Item[,]
        {
             { Items[34], Items[34],Items[34] },
            {Items[34], null, Items[34] },
             { Items[34],Items[34], Items[34] }
        };
        var woodUpstairs = new Item[,]
        {
            { null,null,Items[1] },
            {null, Items[1], Items[1] },
             { Items[1],Items[1], Items[1] }
        };
        var stoneUpstairs = new Item[,]
         {
            { null,null,Items[4] },
            {null, Items[4], Items[4] },
             { Items[4],Items[4], Items[4] }
         };

        Items.Add(new Item("CoalBlock", ItemSprites[35],new CraftRecipe(coalBlockRecipe,1)));
        var wheatBlockRecipe = new Item[,]
    {
             { Items[5], Items[5],Items[5] },
            {Items[5], null, Items[5] },
             { Items[5],Items[5], Items[5] }
    };
        Items.Add(new Item("WheatBlock", ItemSprites[36],new CraftRecipe(wheatBlockRecipe,1)));
        var polWood = new Item[,]
       {
            {Items[1], Items[1], Items[1] }
       };
        Items.Add(new Item("PolWood", ItemSprites[37],new CraftRecipe(polWood,6)));
        var polStone = new Item[,]
        {
            {Items[4], Items[4], Items[4] }
        };
        Items.Add(new Item("PolStone", ItemSprites[38],new CraftRecipe(polStone,6)));
        Items.Add(new Item("WoodUpstairs", ItemSprites[39],new CraftRecipe(woodUpstairs,4)));
        Items.Add(new Item("StoneUpstairs", ItemSprites[40],new CraftRecipe(stoneUpstairs,4)));
        var wheatRecipe = new Item[,]
       {
            { Items[36]}
       };
        Items[5].Recipe = new CraftRecipe(wheatRecipe, 9);
        var coal = new Item[,]
       {
            {Items[35] }
       };
        Items[34].Recipe = new CraftRecipe(coal, 9);
    }
    public Item GetItemById(int id)
    {
        if (id >= 0 && id < Items.Count)
        {
            return Items[id];
        }

        Debug.LogWarning($"Item with ID {id} not found!");
        return null;
    }
    public Item GetItemByName(string name)
    {
        foreach (var item in Items)
        {
            if (item.Name == name)
            {
                return item;
            }
        }
        Debug.LogWarning("Item with name " + name + " not found!");
        return null;
    }
}
