using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeatherCharm : Interactable
{
    public InventoryItem inventoryItem;
    public TextAsset Instructions;
    private List<string> dialogComponents;

    void Awake()
    {

        dialogComponents = new List<string>(Instructions.text.Split('\n'));
        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
    }

    void Start()
    {
        if (Player.instance.items.Find(x => x.itemName == inventoryItem.itemName))
            Destroy(this.gameObject);
    }

    public override void Interact()
    {
        StartCoroutine(CharmGet());
    }

    IEnumerator CharmGet()
    {
        yield return Dialog.DisplayDialog(dialogComponents);

        //We picked up the object, do stuff to it
        Destroy(this.gameObject);
        GameManager.instance.playSound(SoundType.Item, "ItemGet");

        Player.instance.items.Add(inventoryItem);
        Player.instance.currentInteractable = null;
        StartCoroutine(Player.instance.HideSymbol());
        yield return null;
    }
}
