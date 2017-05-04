using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SodaMachine : Interactable {

    public InventoryItem inventoryItem;
    public TextAsset SodaGetText;
    public TextAsset NoSodaText;

    private TextAsset currentDialog;

    private List<string> dialogComponents;

    public override void Interact()
    {
        SetCurrentDialog();
        StartCoroutine(Talk());
    }

    private void SetCurrentDialog()
    {
        if (!QuestManager.instance.drinkTaken && Player.instance.items.Find(x => x.designation == ItemDesignation.Soda) == null)
        {
            QuestManager.instance.drinkTaken = true;
            currentDialog = SodaGetText;
            GameManager.instance.playSound(SoundType.Item, "ItemGet");
            Player.instance.items.Add(inventoryItem);
        }
        else
            currentDialog = NoSodaText;
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
