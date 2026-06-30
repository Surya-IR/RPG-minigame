using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Ins;
    private List<Items> activeItemList = new List<Items>();

    public Items selectedItem;

    private List<Items> itemDetailsList = new List<Items>();

    public struct InventoryInfo
    {
        public string itemName;
        public int amount;
    }

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
    }
    void Start()
    {
        LoadPlayerInventoryItem();
    }

    private void LoadPlayerInventoryItem()
    {
        LoadItemDetails();
        var inventoryName = "ActiveInventory.json";
        var sr = new StreamReader(Application.streamingAssetsPath + "/InventorySystems/ActiveInventory/" + inventoryName);
        var inventoryText = sr.ReadToEnd();

        var activeItemDict = JsonConvert.DeserializeObject<ActiveInventoryWrapper>(inventoryText);

        foreach (ActiveItem items in activeItemDict.list)
        {
            foreach (Items i in itemDetailsList)
            {
                if (items.id == i.itemID)
                {
                    Items generatedItem = ScriptableObject.CreateInstance<Items>();
                    generatedItem.itemName = i.itemName;
                    generatedItem.type = i.type;
                    generatedItem.effectNumber= i.effectNumber;
                    generatedItem.amount = items.amount;

                    activeItemList.Add(generatedItem);
                }
            }
        }
    }

    private void LoadItemDetails()
    {
        var itemDetails = "ItemDetails.json";
        var rd = new StreamReader(Application.streamingAssetsPath + "/InventorySystems/" + itemDetails);
        var rdText = rd.ReadToEnd();

        ItemDetailsWrapper jsonHolder = JsonConvert.DeserializeObject<ItemDetailsWrapper>(rdText);

        itemDetailsList = jsonHolder.list.ToList();
    }

    public void SelectItem(string itemName)
    {
        foreach (Items item in activeItemList)
        {
            if (item.itemName == itemName)
            {
                selectedItem = item;
                Debug.Log("Item Type: " + item.type);
                CommandManager.Ins.EnableButton("items");
            }
        }
    }

    public List<Items> GetItemsList()
    {
        List<Items> FilteredItems = activeItemList.Where(x => x.amount > 0).ToList();
        return FilteredItems;
    }

    void Update()
    {
        
    }
}
