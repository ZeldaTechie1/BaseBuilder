using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    void Start() {
        database = GetComponent<ItemDatabase>();

        slotAmount = 40;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        for (int i = 0; i < slotAmount; i++) {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<InventorySlot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }
    }

    public void AddItem(int id) {
        Item itemToAdd = database.FetchItemByID(id);
        int location = 0;
        if (itemToAdd.Stackable && CheckIfItemExists(itemToAdd, ref location))
        {
            ItemData data = slots[location].transform.GetChild(0).GetComponent<ItemData>();
            data.amount++;
            data.transform.GetChild(0).GetComponent<Text>().text = ((int)data.amount).ToString();
        }
        else 
        { 
            for(int i = 0; i < items.Count; i++) {
                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.GetComponent<ItemData>().slotLocation = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.position = slots[i].transform.position;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = itemToAdd.Title;

                    break;
                }
            }
        }   
    }

    bool CheckIfItemExists(Item item, ref int location)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].ID == item.ID)
            {
                //Debug.Log("Item exists and has a copy!");
                location = i;
                return true;
            }
        }
        return false;
    }

}
