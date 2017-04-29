using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerRoomTrigger : MonoBehaviour {

    public TextAsset FirstWalkInResponse;

    private TextAsset currentDialog;

    public bool firstWalkIn;

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (firstWalkIn && !QuestManager.instance.showerCompleted)
            {
                SetCurrentDialog();
                StartCoroutine(Talk());
                firstWalkIn = false;
            }
        }
    }
    
    private void SetCurrentDialog()
    {
        currentDialog = FirstWalkInResponse;
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
