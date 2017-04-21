using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomNPC : NPC {
    public TextAsset IntroText;
    public TextAsset CompletedText;
    public bool QuestCompleted;

    private TextAsset currentDialog;

    public override void Interact()
    {
        base.Interact();
        SetCurrentDialog();
        StartCoroutine(Talk());
    }

    private void SetCurrentDialog()
    {
        if (!QuestCompleted)
        {
            currentDialog = IntroText;
        }
        else
        {
            currentDialog = CompletedText;
        }
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
