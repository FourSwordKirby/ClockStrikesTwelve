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

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(NotepadPrompt.text));
    }

    IEnumerator SleepTime()
    {
        StartCoroutine(Talk());

        GameManager.instance.SuspendGame();
        UIController.instance.notepadPrompt.openNotepad();
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            if (Controls.cancelInputHeld() || Controls.confirmInputHeld())
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        GameManager.instance.UnsuspendGame();
        UIController.instance.notepadPrompt.closeNotepad();

        yield return null;
    }
}
