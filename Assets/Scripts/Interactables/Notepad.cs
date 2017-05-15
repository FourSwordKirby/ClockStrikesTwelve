using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notepad : Interactable
{
    public TextAsset NotepadPrompt;

    public override void Interact()
    {
        StartCoroutine(SleepTime());
    }

    IEnumerator SleepTime()
    {
        List<string> dialogComponents = Dialog.CreateDialogComponents(NotepadPrompt.text);
        UIController.instance.notepadPrompt.openNotepad();

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
            yield return new WaitForSeconds(0.2f);

            //Replace this with things in the control set
            while (!Controls.confirmInputHeld())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        UIController.instance.dialog.closeDialog();
        UIController.instance.notepadPrompt.closeNotepad();
        GameManager.instance.UnsuspendGame();

        yield return null;
    }
}
