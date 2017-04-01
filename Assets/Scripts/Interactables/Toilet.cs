using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Toilet : Interactable
{
    public TextAsset ShowerResponse1;
    public TextAsset ShowerResponse2;
    public TextAsset ShowerResponse3;
    public TextAsset ShowerResponse4;
    public TextAsset ShowerResponse5;
    public TextAsset GenericResponse;

    private List<string> dialogComponents;
    private List<string> genericComponents;

    void Start()
    {
        if (QuestManager.instance.toiletsFlushed == 0)
            dialogComponents = new List<string>(ShowerResponse1.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 1)
            dialogComponents = new List<string>(ShowerResponse2.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 2)
            dialogComponents = new List<string>(ShowerResponse3.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 3)
            dialogComponents = new List<string>(ShowerResponse4.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 4)
            dialogComponents = new List<string>(ShowerResponse5.text.Split('\n'));

        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();


        genericComponents = new List<string>(GenericResponse.text.Split('\n'));
        genericComponents = genericComponents.Select(x => x.Trim()).ToList();
        genericComponents = genericComponents.Where(x => x != "").ToList();
    }

    public override void Interact()
    {
        bool flushSuccessful = MarkRoomFlushed();
        if (flushSuccessful)
            StartCoroutine(FlushCutscene());
        else
            StartCoroutine(GenericCutscene());
    }

    private bool MarkRoomFlushed()
    {
        int flushCount = QuestManager.instance.toiletsFlushed;
        string currentRoom = GameManager.instance.GetSceneName();
        QuestManager.instance.FlushToilet(currentRoom);
        return (flushCount != QuestManager.instance.toiletsFlushed);
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

        if (QuestManager.instance.toiletsFlushed == 5)
        {
            QuestManager.instance.maintenanceRequestCalled = true;
        }

        yield return null;
    }

    IEnumerator GenericCutscene()
    {
        GameManager.instance.SuspendGame();
        for (int i = 0; i < dialogComponents.Count; i++)
        {
            string[] dialogPieces = genericComponents[i].Split(new string[] { " : " }, System.StringSplitOptions.None);
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

        if (QuestManager.instance.toiletsFlushed == 5)
        {
            QuestManager.instance.maintenanceRequestCalled = true;
        }

        yield return null;
    }
}
