using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyDoorTrigger : MonoBehaviour {

    public TextAsset Door1;
    public TextAsset Door2;
    public TextAsset Door3;

    private TextAsset currentDialog;

    public Collider2D largerTriggerBox;
    public Collider2D smallerTriggerBox;

    private void Update()
    {
        if (QuestManager.instance.talkedToManager && !QuestManager.instance.talkedToManagerPart2)
            largerTriggerBox.enabled = true;
        else
            largerTriggerBox.enabled = false;
    }
    
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
            else if (currentDialog == null)
            {
                smallerTriggerBox.enabled = false;
            }
            else
            {
                StartCoroutine(Talk());
            }
        }
    }
    
    private void SetCurrentDialog()
    {
        if (!QuestManager.instance.talkedToManager)
        {
            currentDialog = Door1;
        }
        else if (!QuestManager.instance.talkedToManagerPart2)
        {
            currentDialog = Door2;
            QuestManager.instance.talkedToManagerPart2 = true;
        }
        else if (!QuestManager.instance.leftLobby)
        {
            currentDialog = Door3;
            QuestManager.instance.leftLobby = true;
        }
        else
        {
            currentDialog = null;
        }
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }

    IEnumerator TitleFade()
    {
        if (currentDialog != null)
        {
            yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
        }
        GameManager.instance.StartSceneTransition("PlayerBedroom");
        QuestManager.instance.introCompleted = true;
    }
}
