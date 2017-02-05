using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransferUI : MonoBehaviour {

    public Sprite defaultSprite;
    public List<Image> myItems;
    public List<Image> storedItems;

    private List<InventoryItem> playerItems;
    private List<InventoryItem> storageItems;
    public int currentChoice;

    public bool transferComplete;

    // Update is called once per frame
    void Update()
    {
        //initialization
        for (int i = 0; i < myItems.Count + storedItems.Count; i++)
        {
            if (i < myItems.Count)
            {
                myItems[i].color = Color.white;
                myItems[i].sprite = defaultSprite;
            }
            else
            {
                storedItems[i - myItems.Count].color = Color.white;
                storedItems[i - myItems.Count].sprite = defaultSprite;
            }
        }

        //Indicator Colors
        for (int i = 0; i < playerItems.Count + storageItems.Count; i++)
        {
            Debug.Log("hi?");
            if (i == currentChoice)
            {
                if (i < playerItems.Count)
                    myItems[i].color = Color.green;
                else
                    storedItems[i - playerItems.Count].color = Color.green;
            }
            else
            {
                if (i < playerItems.Count)
                    myItems[i].color = Color.white;
                else
                    storedItems[i - playerItems.Count].color = Color.white;
            }
        }

        //Sprites
        for (int i = 0; i < playerItems.Count + storageItems.Count; i++)
        {
            if (i < playerItems.Count)
                myItems[i].sprite = playerItems[i].itemSprite;
            else
                storedItems[i - playerItems.Count].sprite = storageItems[i-playerItems.Count].itemSprite;
        }

        //Inputs Change this
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentChoice--;
            if (currentChoice < 0)
                currentChoice = playerItems.Count + storageItems.Count-1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentChoice++;
            if (currentChoice > playerItems.Count + storageItems.Count - 1)
                currentChoice = 0;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (myItems.Count + storageItems.Count == 0)
                return;

            if (currentChoice < playerItems.Count)
            {
                storageItems.Add(playerItems[currentChoice]);
                playerItems.RemoveAt(currentChoice);
            }
            else
            {
                int i = currentChoice - playerItems.Count;
                playerItems.Add(storageItems[i]);
                storageItems.RemoveAt(i);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
            CloseTransfer();
    }

    public void OpenTransfer()
    {
        transferComplete = false;
        this.gameObject.SetActive(true);
        playerItems = GameManager.instance.player.items;
        storageItems = GameManager.instance.chestStoredItems;
    }

    public void CloseTransfer()
    {
        transferComplete = true;
        this.gameObject.SetActive(false);
        GameManager.instance.player.items = playerItems;
        GameManager.instance.chestStoredItems = storageItems;
    }
}
