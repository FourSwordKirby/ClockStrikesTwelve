using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransferPoint : Interactable {

    public TextAsset Instructions;
    private List<string> dialogComponents;

    void Awake()
    {
        dialogComponents = new List<string>(Instructions.text.Split('\n'));
        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
    }

    public override void Interact()
    {
        StartCoroutine(Storage());
    }

    IEnumerator Storage()
    {
        GameManager.instance.paused = true;
        GameManager.instance.player.enabled = false;
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
            while (!UIController.instance.dialog.dialogCompleted)
            {
                yield return new WaitForSeconds(0.1f);
            }
            //Replace this with things in the control set
            while (!Input.GetKey(KeyCode.Mouse0))
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        UIController.instance.transferScreen.OpenTransfer();
        while (!UIController.instance.transferScreen.transferComplete)
        {
            yield return new WaitForSeconds(0.1f);
        }

        UIController.instance.transferScreen.CloseTransfer();
        UIController.instance.dialog.closeDialog();
        GameManager.instance.paused = false;
        GameManager.instance.player.enabled = true;

        yield return null;
    }
}
