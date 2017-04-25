using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SodaMachine : Interactable {

    public InventoryItem inventoryItem;
    public TextAsset SodaGetText;
    public TextAsset NoSodaText;

    private List<string> dialogComponents;

    private void Awake()
    {
    }

    public override void Interact()
    {
        StartCoroutine(arcade());
    }

    IEnumerator arcade()
    {
        yield return Dialog.DisplayDialog(dialogComponents);

        Destroy(this.gameObject);
        GameManager.instance.playSound(SoundType.Item, "ItemGet");

        Player.instance.items.Add(inventoryItem);
        Player.instance.currentInteractable = null;
        StartCoroutine(Player.instance.HideSymbol());
        yield return null;
    }
}
