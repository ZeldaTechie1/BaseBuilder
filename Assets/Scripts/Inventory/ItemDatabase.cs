﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class ItemDatabase : MonoBehaviour {
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start() {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Database/Items.json"));
        ConstructItemDatabase();
    }

    public Item FetchItemByID(int id) {
        for (int i = 0; i < database.Count; i++)
            if(database[i].ID == id)
            return database[i];
        return null;
    }

    void ConstructItemDatabase() {
        for (int i = 0; i < itemData.Count; i++) {
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["value"],
            (int)itemData[i]["attributes"]["strength"], (int)itemData[i]["attributes"]["defence"], (int)itemData[i]["attributes"]["damage"], 
            itemData[i]["description"].ToString(), (bool)itemData[i]["stackable"], (int)itemData[i]["rarity"], itemData[i]["slug"].ToString()));
        }
    }
}

public class Item {
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Strength { get; set; }
    public int Defence { get; set; }
    public int Damage { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item(int id, string title, int value, int strength, int defence, int damage, string description, bool stackable, int rarity, string slug) {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Strength = strength;
        this.Defence = defence;
        this.Damage = damage;
        this.Description = description;
        this.Stackable = stackable;
        this.Rarity = rarity;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Inventory/Items/" + slug);
    }

    public Item(int id, string title) {
        this.ID = id;
        this.Title = title;
    }

    public Item() {
        this.ID = -1;
    }

}