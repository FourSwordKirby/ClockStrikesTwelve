using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationUI : MonoBehaviour {

    public List<Button> InventorySlots;
    public Text itemDesc;
    public static ConversationUI instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnEnable()
    {
        itemDesc.text = "";
        for (int i = 0; i < 10; i++)
        {
            InventorySlots[i].gameObject.SetActive(false);
        }
        int idx = 0;
        foreach (ConversationItem item in Player.instance.conversations)
        {
            InventorySlots[idx].gameObject.SetActive(true);
            InventorySlots[idx].GetComponentInChildren<Text>().text = item.itemName;
            idx++;
        }
    }
}
