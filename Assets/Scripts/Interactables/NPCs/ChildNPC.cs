using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildNPC : NPC
{
    public TextAsset ArcadeIntroText;
    public TextAsset ArcadeNoMoneyText;
    public TextAsset HomeDefault;
    public TextAsset SentBackText;
    public TextAsset ReturnedHomeText;

    public bool TalkedOnce;
    public bool NoQuarters;
    public bool SentHome;

    private TextAsset currentDialog;

    public override void Interact()
    {
        base.Interact();
        SetCurrentDialog();
        StartCoroutine(Talk());
        // TODO: move to room if SentHome and in arcade
    }

    private void SetCurrentDialog()
    {
        if (!TalkedOnce)
        {
            currentDialog = ArcadeIntroText;
        }
        else if (!NoQuarters)
        {
            currentDialog = ArcadeNoMoneyText;
        }
        else if (SentHome)
        {
            currentDialog = ReturnedHomeText;
        }
        else
        {
            currentDialog = HomeDefault;
        }
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
