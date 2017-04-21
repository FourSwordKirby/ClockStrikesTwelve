﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnoopDogNPC : NPC
{
    public bool met = false;
    public bool satisfied = false;
    public TextAsset InitialRequest; //False false
    public TextAsset RepeatedRequest; //True false (nothing in inventory)
    public TextAsset SatisfiedRequest; //True false (soda in inventory)
    public TextAsset YourName; //huehue //True true

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
        if (!met)
        {
            currentDialog = InitialRequest;
            met = true;
        }
        else if (satisfied)
        {
            currentDialog = YourName;
        }
        else if (Player.instance.items.Find(x => x.designation == ItemDesignation.Soda) == null)
        {
            currentDialog = RepeatedRequest;
            
        }
        else
        {
            satisfied = true;
            currentDialog = SatisfiedRequest;
        }

    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}