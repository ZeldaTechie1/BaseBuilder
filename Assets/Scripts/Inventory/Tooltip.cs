using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    private Item item;
    private string data;
    private GameObject tooltip;

    void Start() {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    void Update() { 
        if(tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item) {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate() {
        tooltip.SetActive(false);
    }

    public void ConstructDataString() {

        if (item.Stackable)
        {
            data = "<color=#ff4f4f><b>" + item.Title + "</b></color>" +
                   "\n" + item.Description;
            tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
        }
        else
        {
            data = "<color=#ff4f4f><b>" + item.Title + "</b></color>" +
                   "\nStats: " +
                   "\nDamage: " + item.Damage +
                   "\nDefence: " + item.Defence +
                   "\n" + item.Description;
            tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
        }
    }
}
