using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VendingMachine : Interactable {

    public InventoryItem inventoryItem;
    public TextAsset VendingCorrectText5;
    public TextAsset VendingCorrectText4;
    public TextAsset VendingCorrectText3;
    public TextAsset VendingCorrectText2;
    public TextAsset VendingCorrectText1;
    public TextAsset VendingCorrectText0;
    public TextAsset VendingWrongText;

    public bool correctPassword;
    public int coinsRemaining;

    private List<string> dialogComponents;

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
