using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VendingMachine : Interactable {

    public InventoryItem inventoryItem;
    public TextAsset arcadeText;
    private List<string> dialogComponents;

    private void Awake()
    {
        dialogComponents = new List<string>(arcadeText.text.Split('\n'));
        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
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
