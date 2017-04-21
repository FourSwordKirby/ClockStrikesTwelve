using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ManagerNPC : NPC
{
    public TextAsset GenericResponse;

    private List<string> dialogComponents;

    void Start()
    {
        InitializeRenderer();
        if (QuestManager.instance.toiletsFlushed == 0)
            dialogComponents = new List<string>(GenericResponse.text.Split('\n'));

        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(FlushCutscene());
    }

    IEnumerator FlushCutscene()
    {
        GameManager.instance.SuspendGame();
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

            yield return new WaitForSeconds(0.1f);
            while (!UIController.instance.dialog.dialogCompleted)
            {
                yield return new WaitForSeconds(0.1f);
            }

            while (!Controls.confirmInputHeld())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        UIController.instance.dialog.closeDialog();
        GameManager.instance.UnsuspendGame();

        flushed = true;
        yield return null;
    }
}
