using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayAfternoonEvent : MonoBehaviour
{
    public TextAsset dialogFile;
    private List<string> dialogComponents;

    void Awake()
    {
        dialogComponents = new List<string>(dialogFile.text.Split('\n'));
        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
    }

    public IEnumerator DayAfternoon()
    {
        StartCoroutine(UIController.instance.screenfader.FadeOut(1.5f));

        UIController.instance.dialog.dialogBox.enabled = false;
        UIController.instance.dialog.speakerBox.enabled = false;

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

            while (!UIController.instance.dialog.dialogCompleted)
            {
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.25f);
            //Replace this with things in the control set
            while (!Controls.confirmInputHeld())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(0.5f);

        UIController.instance.dialog.dialogBox.enabled = true;
        UIController.instance.dialog.speakerBox.enabled = true;
        UIController.instance.dialog.closeDialog();
        
        StartCoroutine(UIController.instance.screenfader.FadeIn(1.5f));
        GameManager.instance.UnsuspendGame();

        yield return null;
    }
}
