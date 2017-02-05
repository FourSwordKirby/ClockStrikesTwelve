using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour {

    public TextAsset description;
    public ItemDesignation designation;
    public Sprite itemSprite;

    //This is completely optional, may be useful later
    public Interactable associatedInteractable;
}
