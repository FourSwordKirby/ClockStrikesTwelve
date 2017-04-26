using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SodaMachine : Interactable {

    public InventoryItem inventoryItem;
    public TextAsset SodaGetText;
    public TextAsset NoSodaText;

    private TextAsset currentDialog;

    public bool hasSoda;

    private List<string> dialogComponents;

    public override void Interact()
    {
        SetCurrentDialog();
        StartCoroutine(Talk());
    }

    private void SetCurrentDialog()
    {
        if (hasSoda)
        {
            hasSoda = false;
            currentDialog = SodaGetText;
        }
        else
            currentDialog = NoSodaText;
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
