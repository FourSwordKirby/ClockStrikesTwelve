using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShowerRoomDoor : Interactable
{
    public TextAsset ShowerDoorResponse1;
    public TextAsset ShowerDoorResponse2;
    public TextAsset ShowerDoorResponse3;
    public TextAsset ShowerDoorResponse4;
    public TextAsset ShowerDoorResponse5;
    public TextAsset RequestMaintenence;

    private List<string> dialogComponents;

    void Start()
    {
        if(QuestManager.instance.toiletsFlushed == 0)
            dialogComponents = new List<string>(ShowerDoorResponse1.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 1)
            dialogComponents = new List<string>(ShowerDoorResponse2.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 2)
            dialogComponents = new List<string>(ShowerDoorResponse3.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 3)
            dialogComponents = new List<string>(ShowerDoorResponse4.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 4)
            dialogComponents = new List<string>(ShowerDoorResponse5.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 5)
        {
            dialogComponents = new List<string>(RequestMaintenence.text.Split('\n'));
        }

        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
    }

    public override void Interact()
    {
        StartCoroutine(DoorResponse());
    }

    IEnumerator DoorResponse()
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
            QuestManager.instance.maintencenceRequestCalled = true;
        }

        yield return null;
    }
}

