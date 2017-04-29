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

            if (currentDialog == Door3)
            {
                StartCoroutine(UIController.instance.screenfader.FadeOut(0.1f));
                StartCoroutine(TitleFade());
            }
            else
                StartCoroutine(Talk());
        }
    }
    
    private void SetCurrentDialog()
    {
        //TODO: this is just placeholder
        if (!QuestManager.instance.talkedToManager)
        {
            currentDialog = Door1;
        }
        else if (!QuestManager.instance.talkedToManagerPart2)
        {
            currentDialog = Door2;
            QuestManager.instance.talkedToManagerPart2 = true;
        }
        else
            currentDialog = Door3;
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }

    IEnumerator TitleFade()
    {
        List<string> dialogComponents = Dialog.CreateDialogComponents(currentDialog.text);
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

        GameManager.instance.StartSceneTransition("PlayerBedroom");
        QuestManager.instance.introCompleted = true;
    }
}
