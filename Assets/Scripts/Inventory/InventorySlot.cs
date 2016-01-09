using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {
    public int id;
    private Inventory inv;

    void Start() {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData) {
        ItemData dropItem = eventData.pointerDrag.GetComponent<ItemData>();
        if(inv.items[id].ID == -1)
        {
            Debug.Log("Item was moved to an empty space.");
            inv.items[dropItem.slotLocation] = new Item();
            inv.items[id] = dropItem.item;
            dropItem.slotLocation = id;
        }
        else if(dropItem.slotLocation != id && dropItem.slotLocation != -1)
        {
            Debug.Log("Item was swapped with another item.");
            Transform item = this.transform.GetChild(0);
            item.GetComponent<ItemData>().slotLocation = dropItem.slotLocation;
            item.transform.SetParent(inv.slots[dropItem.slotLocation].transform);
            item.transform.position = inv.slots[dropItem.slotLocation].transform.position;

            dropItem.slotLocation = id;
            dropItem.transform.SetParent(this.transform);
            dropItem.transform.position = this.transform.position;

            inv.items[dropItem.slotLocation] = item.GetComponent<ItemData>().item;
            inv.items[id] = dropItem.item;
        }
        else
        {
            Debug.Log("Attempting to move Item to same slot it was already in...not so smart.");
        }
    }
}
