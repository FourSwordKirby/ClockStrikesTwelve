using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemUpdate : MonoBehaviour, ISelectHandler {

	public void OnSelect(BaseEventData eventData)
    {
        InventoryItem item = Player.instance.items[int.Parse(this.name.Substring(5)) - 1];
        InventoryUI.instance.itemImage.sprite = item.itemSprite;
        InventoryUI.instance.itemDesc.text = item.description;
    }
}
