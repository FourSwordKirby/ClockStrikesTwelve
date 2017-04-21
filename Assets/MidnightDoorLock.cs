using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidnightDoorLock : MonoBehaviour {

    public TextAsset warning;
    private TextAsset currentDialog;

    public Collider2D lockCollisionBox;

    private void Update()
    {
        if (GameManager.instance.currentTime > GameManager.instance.timeLimit)
            lockCollisionBox.enabled = true;
        else
            lockCollisionBox.enabled = false;
    }

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            SetCurrentDialog();
            StartCoroutine(Talk());
        }
    }

    private void SetCurrentDialog()
    {
        currentDialog = warning;
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
