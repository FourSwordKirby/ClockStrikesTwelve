using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyDoorTrigger : MonoBehaviour {

    public TextAsset Door1;
    public TextAsset Door2;
    public TextAsset Door3;

    private TextAsset currentDialog;

    public Collider2D largerTriggerBox;

    private void Update()
    {
        if (QuestManager.instance.talkedToManager && !QuestManager.instance.talkedToManagerPart2)
            largerTriggerBox.enabled = true;
        else
            largerTriggerBox.enabled = false;
    }

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            SetCurrentDialog();

            if (currentDialog == Door2)
                QuestManager.instance.talkedToManagerPart2 = true;

            if (currentDialog == Door3)
                StartCoroutine(UIController.instance.screenfader.FadeOut(0.1f));
            StartCoroutine(Talk());
        }
    }
    
    private void SetCurrentDialog()
    {
        //TODO: this is just placeholder
        if (!QuestManager.instance.talkedToManager)
            currentDialog = Door1;
        else if (!QuestManager.instance.talkedToManagerPart2)
            currentDialog = Door2;
        else
            currentDialog = Door3;
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
