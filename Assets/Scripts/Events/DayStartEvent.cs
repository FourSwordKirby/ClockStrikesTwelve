using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DayStartEvent : MonoBehaviour {

    public GameObject normalSceneSetup;
    public Vector2 startPosition;
    public TextAsset dialogFile;
    private List<string> dialogComponents;

    void Awake()
    {
        dialogComponents = new List<string> (dialogFile.text.Split('\n')) ;
        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
    }

    void Start()
    {
        if(!QuestManager.instance.introCompleted)
        {
            normalSceneSetup.SetActive(false);
            UIController.instance.screenfader.CutToColor(Color.white);
            StartCoroutine(IntroSequence());
        }
    }

    IEnumerator IntroSequence()
    {
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
            bool wasClicked = Controls.confirmInputHeld();
            bool isClicked = Controls.confirmInputHeld();
            while (!UIController.instance.dialog.dialogCompleted)
            {
                if (isClicked && !wasClicked) {
                    UIController.instance.dialog.setSpeed (DisplaySpeed.immediate);
                }
                wasClicked = isClicked;
                isClicked = Controls.confirmInputHeld();
                yield return new WaitForSeconds(0.1f);
            }
            //Replace this with things in the control set
            while(!(isClicked && !wasClicked))
            {
                wasClicked = isClicked;
                isClicked = Controls.confirmInputHeld();
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(0.5f);

        UIController.instance.dialog.dialogBox.enabled = true;
        UIController.instance.dialog.speakerBox.enabled = true;
        UIController.instance.dialog.closeDialog();
        QuestManager.instance.introCompleted = true;

        GameObject.FindObjectOfType<Player>().transform.position = startPosition;
        StartCoroutine(UIController.instance.screenfader.FadeIn(2.0f));
        GameManager.instance.UnsuspendGame();
        yield return null;
    }
}
