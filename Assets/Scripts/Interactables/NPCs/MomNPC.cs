using System.Collections;
using System.Linq;
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
        if (!QuestManager.instance.momChildCompleted)
        {
            if(GameManager.instance.dayPhase != 2) //Fix this to be the proper time
                currentDialog = IntroText;
            else
                currentDialog = InProgressText;
        }
        else
        {
            currentDialog = CompletedText;
            if(Player.instance.conversations.Find(x => x.itemName == "time") == null)
                Player.instance.conversations.Add(new ConversationItem("time", "I wish we had more of it"));
        }
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
