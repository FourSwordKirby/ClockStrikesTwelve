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
    public TextAsset UnresponsiveText;

    public bool inArcade;
    public bool TalkedOnce;
    public bool NoQuarters;
    public bool SentHome;

    private TextAsset currentDialog;

    private bool atHome;

    void Start()
    {
        base.Start();
        inArcade = GameManager.instance.GetSceneName() == "ArcadeRoom";
        atHome = QuestManager.instance.sentHome;
    }

    public void Update()
    {
        if (GameManager.instance.dayPhase >= 2)
        {
            this.gameObject.SetActive(inArcade ^ atHome);
        }
        else
        {
            this.gameObject.SetActive(!inArcade);
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
        if (inArcade)
        {
            if (!QuestManager.instance.momChildStarted)
            {
                currentDialog = UnresponsiveText;
            }
            else if (QuestManager.instance.sentHome)
            {
                currentDialog = SentBackText;
            }
            else if (QuestManager.instance.changeLockedOut)
            {
                currentDialog = ArcadeIntroText;
                QuestManager.instance.childFailed = true;
            }
            else if (!QuestManager.instance.changeLockedOut)
            {
                currentDialog = ArcadeNoMoneyText;
                QuestManager.instance.sentHome = true;
            }
        } 
        else
        {
            if (QuestManager.instance.sentHome)
            {
                currentDialog = ReturnedHomeText;
            }
            else
            {
                currentDialog = HomeDefault;
            }
        }
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
