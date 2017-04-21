using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildNPC : NPC
{
    public TextAsset IntroText;
    public TextAsset GoAwayText;
    public TextAsset SendBackText;
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
            currentDialog = IntroText;
        }
        else if (!NoQuarters)
        {
            currentDialog = GoAwayText;
        }
        else if (!SentHome)
        {
            currentDialog = SendBackText;
        }
        else
        {
            currentDialog = ReturnedHomeText;
        }
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
