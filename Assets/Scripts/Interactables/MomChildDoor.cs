using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomChildDoor : Interactable
{
    public TextAsset spookyResponse;
    public GameObject ooze;
    public Collider2D lockCollisionBox;

    private TextAsset currentDialog;

    private void Update()
    {
        if (GameManager.instance.currentTime > GameManager.instance.timeLimit * 0.5f)
        {
            ooze.SetActive(true);
            lockCollisionBox.enabled = true;
        }
        else
        {
            ooze.SetActive(false);
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
        currentDialog = spookyResponse;
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
