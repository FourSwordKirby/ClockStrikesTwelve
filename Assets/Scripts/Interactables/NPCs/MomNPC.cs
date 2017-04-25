using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomNPC : NPC {
    public TextAsset IntroText;
    public TextAsset InProgressText;
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
            if(GameManager.instance.currentTime < 20) //Fix this to be the proper time
                currentDialog = IntroText;
            else
                currentDialog = InProgressText;
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
