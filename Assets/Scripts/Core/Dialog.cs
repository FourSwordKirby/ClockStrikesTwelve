using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public delegate void CorrectReponseFunction();

public class Dialog {

    public static List<string> CreateDialogComponents(string text)
    {
        List<string> dialogComponents = new List<string>(text.Split('\n'));
        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
        return dialogComponents;
    }


	public static IEnumerator DisplayDialog(List<string> dialogComponents) {
        GameManager.instance.SuspendGame();
        for (int i = 0; i < dialogComponents.Count; i++)
        {
            string[] dialogPieces = dialogComponents[i].Split(new string[] { " : " }, System.StringSplitOptions.None);
            string speaker = "";
            string dialog = "";
            if (dialogPieces.Length > 1)
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
            while (!Controls.confirmInputHeld())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        UIController.instance.dialog.closeDialog();
        GameManager.instance.UnsuspendGame();
    }

    public static IEnumerator DisplayPrompt(List<string> promptComponents, string correctAnswer, List<string> correctComponents, List<string> incorrectComponents, CorrectReponseFunction func)
    {
        GameManager.instance.SuspendGame();
        for (int i = 0; i < promptComponents.Count; i++)
        {
            string[] dialogPieces = promptComponents[i].Split(new string[] { " : " }, System.StringSplitOptions.None);
            string speaker = "";
            string dialog = "";
            if (dialogPieces.Length > 1)
            {
                speaker = dialogPieces[0];
                dialog = dialogPieces[1];
            }
            else
                dialog = dialogPieces[0];
            Debug.Log("ABout to displat");
            UIController.instance.dialog.displayDialog(dialog, speaker);
            while (!UIController.instance.dialog.dialogCompleted)
            {
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.25f);
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

        string enteredAnswer = UIController.instance.passwordPrompt.getAnswer();
        if (enteredAnswer == correctAnswer)
        {
            for (int i = 0; i < correctComponents.Count; i++)
            {
                string[] dialogPieces = correctComponents[i].Split(new string[] { " : " }, System.StringSplitOptions.None);
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

            //Cleanup if correct
            func();
        }
        else
        {
            for (int i = 0; i < incorrectComponents.Count; i++)
            {
                string[] dialogPieces = incorrectComponents[i].Split(new string[] { " : " }, System.StringSplitOptions.None);
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
    }
}
