using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationItem {

    public string itemName;
    public string description;

    public ConversationItem(string v1, string v2)
    {
        this.itemName = v1;
        this.description = v2;
    }
}
