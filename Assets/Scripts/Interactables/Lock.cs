﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Lock : Interactable {

    public int designation;

    public string correctPassword;

    public TextAsset Instructions;
    public TextAsset SuccessText;
    public TextAsset FailText;

    private List<string> instructionDialogComponents;
    private List<string> successDialogComponents;
    private List<string> failureDialogComponents;

    void Awake()
    {
        instructionDialogComponents = new List<string>(Instructions.text.Split('\n'));
        instructionDialogComponents = instructionDialogComponents.Select(x => x.Trim()).ToList();
        instructionDialogComponents = instructionDialogComponents.Where(x => x != "").ToList();

        successDialogComponents = new List<string>(SuccessText.text.Split('\n'));
        successDialogComponents = successDialogComponents.Select(x => x.Trim()).ToList();
        successDialogComponents = successDialogComponents.Where(x => x != "").ToList();

        failureDialogComponents = new List<string>(FailText.text.Split('\n'));
        failureDialogComponents = failureDialogComponents.Select(x => x.Trim()).ToList();
        failureDialogComponents = failureDialogComponents.Where(x => x != "").ToList();
    }

    void Update()
    {
        if (this.designation == 0)
            this.gameObject.SetActive(!QuestManager.instance.lockClear0);
        else if (this.designation == 1)
            this.gameObject.SetActive(!QuestManager.instance.lockClear1);
        else if (this.designation == 2)
            this.gameObject.SetActive(!QuestManager.instance.lockClear2);
    }

    public override void Interact()
    {
        StartCoroutine(PasswordTest());
    }

    IEnumerator PasswordTest()
    {
        GameManager.instance.SuspendGame();
        for (int i = 0; i < instructionDialogComponents.Count; i++)
        {
            string[] dialogPieces = instructionDialogComponents[i].Split(new string[] { " : " }, System.StringSplitOptions.None);
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

            yield return new WaitForSeconds(0.1f);
            while (!UIController.instance.dialog.dialogCompleted)
            {
                yield return new WaitForSeconds(0.1f);
            }
            //Replace this with things in the control set
            while (!Controls.confirmInputHeld())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        UIController.instance.dialog.closeDialog();
        UIController.instance.passwordPrompt.openPassword();

        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            if (Controls.cancelInputHeld() || Controls.confirmInputHeld())
            {
                if (UIController.instance.passwordPrompt.getAnswer().Length > 0)
                {
                    break;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        UIController.instance.passwordPrompt.closePassword();

        string enteredPassword = UIController.instance.passwordPrompt.getAnswer().ToLower().Trim();
        print(enteredPassword);
        print(correctPassword);
        if(enteredPassword == correctPassword)
        {
            for (int i = 0; i < successDialogComponents.Count; i++)
            {
                string[] dialogPieces = successDialogComponents[i].Split(new string[] { " : " }, System.StringSplitOptions.None);
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

                yield return new WaitForSeconds(0.1f);
                while (!UIController.instance.dialog.dialogCompleted)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                //Replace this with things in the control set
                while (!Controls.confirmInputHeld())
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }

            //Clearing the lock
            if (this.designation == 0)
                QuestManager.instance.lockClear0 = true;
            else if (this.designation == 1)
                QuestManager.instance.lockClear1 = true;
            else if (this.designation == 2)
                QuestManager.instance.lockClear2 = true;
        }
        else
        {
            for (int i = 0; i < failureDialogComponents.Count; i++)
            {
                string[] dialogPieces = failureDialogComponents[i].Split(new string[] { " : " }, System.StringSplitOptions.None);
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

                yield return new WaitForSeconds(0.1f);
                while (!UIController.instance.dialog.dialogCompleted)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                //Replace this with things in the control set
                while (!Controls.confirmInputHeld())
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        UIController.instance.dialog.closeDialog();
        GameManager.instance.UnsuspendGame();
        yield return null;
    }
}
