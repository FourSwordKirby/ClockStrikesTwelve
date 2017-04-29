using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShowerRoomDoor : Interactable
{
    public List<TextAsset> InitialReponses;
    //public TextAsset RequestMaintenance;
    //public TextAsset RequestMaintenance2;
    public TextAsset PostMaintenance;

    public Collider2D doorEntranceBox;
    public Collider2D lockCollisionBox;

    private TextAsset currentDialog;

    private void Update()
    {
        if (!QuestManager.instance.maintenanceCompleted)
        {
            doorEntranceBox.enabled = false;
            lockCollisionBox.enabled = true;
        }
        else
        {
            doorEntranceBox.enabled = true;
            lockCollisionBox.enabled = false;
        }
    }

    public override void Interact()
    {
        SetCurrentDialog();
        StartCoroutine(Talk());
    }

    private void SetCurrentDialog()
    {
        //TODO: this is just placeholder
        //if (QuestManager.instance.maintenancePosted)
        //{
        //    currentDialog = PostMaintenance;
        //}
        //else if (QuestManager.instance.maintenanceRequestCalled)
        //{
        //    currentDialog = RequestMaintenance2;
        //}
        //else if (QuestManager.instance.toiletsFlushed == 4)
        //{
        //    currentDialog = RequestMaintenance;
        //}
        if (QuestManager.instance.toiletsFlushed == 4)
        {
            currentDialog = PostMaintenance;
        }
        else
        {
            currentDialog = InitialReponses[Random.Range(0, InitialReponses.Count)];
        }
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}

