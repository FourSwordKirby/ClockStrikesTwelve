using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    public List<Image> InventorySlots;

	// Update is called once per frame
	void Update ()
    {
        for(int i = 0; i < InventorySlots.Count; i++)
        {
            if(i < Player.instance.items.Count)
            {
                InventorySlots[i].sprite = Player.instance.items[i].itemSprite;
                InventorySlots[i].color = Color.white;
            }
            else
            {
                InventorySlots[i].sprite = null;
                InventorySlots[i].color = Color.clear;
            }
        }
    }
}
