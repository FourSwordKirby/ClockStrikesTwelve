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

    public bool inArcade;
    public bool TalkedOnce;
    public bool NoQuarters;
    public bool SentHome;

    private TextAsset currentDialog;

    void Start()
    {
        base.Start();
        inArcade = GameManager.instance.GetSceneName() == "ArcadeRoom" && !QuestManager.instance.sentHome;
    }

    public void Update()
    {
        if(GameManager.instance.dayPhase >= 2)
        {
            if (inArcade)
                this.gameObject.SetActive(true);
            else
                this.gameObject.SetActive(false);
        }
        else
        {
            if (inArcade)
                this.gameObject.SetActive(false);
            else
                this.gameObject.SetActive(true);
        }

    }

    public override void Interact()
    {
        base.Interact();
        SetCurrentDialog();
        StartCoroutine(Talk());
    }

    private void SetCurrentDialog()
    {
        if (inArcade && QuestManager.instance.changeLockedOut)
        {
            currentDialog = ArcadeIntroText;
        }
        else if (inArcade && !QuestManager.instance.changeLockedOut)
        {
            currentDialog = ArcadeNoMoneyText;
            QuestManager.instance.sentHome = true;
            QuestManager.instance.momChildCompleted = true;
        }
        else if (QuestManager.instance.sentHome && inArcade)
        {
            currentDialog = SentBackText;
        }
        else if (QuestManager.instance.sentHome && !inArcade)
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
