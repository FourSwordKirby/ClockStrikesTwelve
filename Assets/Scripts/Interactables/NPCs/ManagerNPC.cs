using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNPC : NPC
{
    public TextAsset LobbyResponseAntiSocial;
    public TextAsset LobbyHintShower;
    public TextAsset LobbyHintWriter;
    public TextAsset LobbyHintMom;
    public TextAsset BoothIntro1;
    public TextAsset BoothIntro2;

    private TextAsset currentDialog;
    
    public override void Interact()
    {
        base.Interact();
        SetCurrentDialog();
        StartCoroutine(Talk());
    }

    private void SetCurrentDialog()
    {
        if (!QuestManager.instance.talkedToManager)
        {
            currentDialog = BoothIntro1;
            QuestManager.instance.talkedToManager = true;
        }
        else if (!QuestManager.instance.talkedToManagerPart2)
        {
            currentDialog = BoothIntro2;
        }
        else
        {
            List<TextAsset> potentialDialogs = new List<TextAsset>();
            potentialDialogs.Add(LobbyResponseAntiSocial);
            if(QuestManager.instance.showerCompleted)
                potentialDialogs.Add(LobbyHintShower);
            else if (QuestManager.instance.writerCompleted)
                potentialDialogs.Add(LobbyHintWriter);
            else if (QuestManager.instance.momChildCompleted)
                potentialDialogs.Add(LobbyHintMom);

            currentDialog = potentialDialogs[Random.Range(0, potentialDialogs.Count)];
        }
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
