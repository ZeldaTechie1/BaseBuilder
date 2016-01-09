using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemPickup : MonoBehaviour
{
    public GameObject inventory;
    public ItemDatabase database;
    public Inventory inv;
    Item itemToAdd;
    float range = 10f; // debugging with a drawline
    int id;
    RaycastHit hit;
    Ray ray;

    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject obj = hit.collider.gameObject;

            Debug.DrawLine(transform.position, hit.point, Color.red);

            // check if obj is an item, if true, add it to the inventory and destroy the game obj
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject item = hit.collider.gameObject;
               
                if (item.tag == "Item")
                {
                    //Debug.Log("Hitting this item: " + hit.collider.gameObject.name);
                    //Debug.Log("Found on db: " + database.FetchItemByName(item.name).Slug);
                    Item itemToAdd = database.FetchItemByName(item.name);
                    Debug.Log(itemToAdd.Slug);
                    Destroy(hit.collider.gameObject);

                    inv.AddItem(itemToAdd.ID);
                }
            }
        }
    }
}
