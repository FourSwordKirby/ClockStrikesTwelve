﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SleepPoint : Interactable {

    public TextAsset Instructions;
    private List<string> dialogComponents;

    void Awake()
    {
        dialogComponents = new List<string>(Instructions.text.Split('\n'));
        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
    }

    public override void Interact()
    {
        StartCoroutine(SleepTime());
    }

    IEnumerator SleepTime()
    {
        GameManager.instance.paused = true;
        Player.instance.enabled = false;
        for (int i = 0; i < dialogComponents.Count; i++)
        {
            string[] dialogPieces = dialogComponents[i].Split(new string[] { " : " }, System.StringSplitOptions.None);
            string speaker = "";
            string dialog = "";
            if (dialogPieces.Count() > 1)
            {
                speaker = dialogPieces[0];
                dialog = dialogPieces[1];
            }
            else
                dialog = dialogPieces[0];
            UIController.instance.dialog.displayDialog(dialog, speaker);
            while (!UIController.instance.dialog.dialogCompleted)
            {
                yield return new WaitForSeconds(0.1f);
            }
            //Replace this with things in the control set
            while (!Input.GetKey(KeyCode.Mouse0))
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        UIController.instance.choiceScreen.OpenChoices(new List<string>() { "Yes", "No" });
        while(!UIController.instance.choiceScreen.choiceMade)
        {
            yield return new WaitForSeconds(0.1f);
        }

        UIController.instance.choiceScreen.CloseChoices();
        if (UIController.instance.choiceScreen.currentChoice == 0)
        {
            StartCoroutine(GameManager.instance.ResetDay());
        }
        else
        {
            UIController.instance.dialog.closeDialog();
            GameManager.instance.paused = false;
            Player.instance.enabled = true;
        }

        yield return null;
    }
}
