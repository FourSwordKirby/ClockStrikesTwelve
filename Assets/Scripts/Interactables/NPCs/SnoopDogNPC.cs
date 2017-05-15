using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnoopDogNPC : NPC
{
    public TextAsset InitialRequest; //False false
    public TextAsset RepeatedRequest; //True false (nothing in inventory)
    public TextAsset SatisfiedRequest; //True false (soda in inventory)
    public TextAsset Rapper; //True true
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
        if (!QuestManager.instance.snoopMet)
        {
            currentDialog = InitialRequest;
            QuestManager.instance.snoopMet = true;
        }
        else if (QuestManager.instance.tvOff)
        {
            if (!QuestManager.instance.showerCompleted)
                currentDialog = YourName;
            else
                currentDialog = Rapper;
        }
        else
        {
            var ourSoda = Player.instance.items.Find(x => x.itemName == "Mao 10 Dude Soda");
            if (ourSoda == null)
            {
                currentDialog = RepeatedRequest;
            }
            else
            {
                ourSoda.itemName = "Empty Mao 10 Dude Soda";
                currentDialog = SatisfiedRequest;
                QuestManager.instance.tvOff = true;
            }
        }


    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}