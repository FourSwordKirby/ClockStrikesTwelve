﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomNPC : NPC {
    public TextAsset PlaceholderText;

    private TextAsset currentDialog;

    public override void Interact()
    {
        base.Interact();
        SetCurrentDialog();
        StartCoroutine(Talk());
    }

    private void SetCurrentDialog()
    {
        //TODO: this is just placeholder
        if (true)
        {
            currentDialog = PlaceholderText;
        }
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
