using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConversationItemUpdate : MonoBehaviour, ISelectHandler {

    public void OnSelect(BaseEventData eventData)
    {
        ConversationItem item = Player.instance.conversations[int.Parse(this.name.Substring(5)) - 1];
        ConversationUI.instance.itemDesc.text = item.description;
    }
}
