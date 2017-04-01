using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Toilet : Interactable
{
    public TextAsset ToiletFlushResponse1;
    public TextAsset ToiletFlushResponse2;
    public TextAsset ToiletFlushResponse3;
    public TextAsset ToiletFlushResponse4;
    public TextAsset ToiletFlushResponse5;
    public TextAsset GenericResponse;

    private List<string> dialogComponents;
    private List<string> genericComponents;

    public Sprite unflushedSprite;
    public Sprite flushedSprite;

    public bool flushed;

    void Start()
    {
        if (QuestManager.instance.toiletsFlushed == 0)
            dialogComponents = new List<string>(ToiletFlushResponse1.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 1)
            dialogComponents = new List<string>(ToiletFlushResponse2.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 2)
            dialogComponents = new List<string>(ToiletFlushResponse3.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 3)
            dialogComponents = new List<string>(ToiletFlushResponse4.text.Split('\n'));
        else if (QuestManager.instance.toiletsFlushed == 4)
            dialogComponents = new List<string>(ToiletFlushResponse5.text.Split('\n'));

        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();


        genericComponents = new List<string>(GenericResponse.text.Split('\n'));
        genericComponents = genericComponents.Select(x => x.Trim()).ToList();
        genericComponents = genericComponents.Where(x => x != "").ToList();
    }

    public void Update()
    {
        if (!flushed)
            GetComponent<SpriteRenderer>().sprite = unflushedSprite;
        else
            GetComponent<SpriteRenderer>().sprite = flushedSprite;
    }

    public override void Interact()
    {
        MarkRoomFlushed();
        if (QuestManager.instance.toiletsFlushed == 4)
        {
            QuestManager.instance.maintenanceRequestCalled = true;
        }

        if (!flushed)
            StartCoroutine(FlushCutscene());
        else
            StartCoroutine(GenericCutscene());
    }

    private void MarkRoomFlushed()
    {
        int flushCount = QuestManager.instance.toiletsFlushed;
        string currentRoom = GameManager.instance.GetSceneName();
        QuestManager.instance.FlushToilet(currentRoom);
    }

    IEnumerator FlushCutscene()
    {
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

            yield return new WaitForSeconds(0.1f);
            while (!UIController.instance.dialog.dialogCompleted)
            {
                yield return new WaitForSeconds(0.1f);
            }

            while (!Controls.confirmInputHeld())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        UIController.instance.dialog.closeDialog();
        GameManager.instance.UnsuspendGame();

        flushed = true;
        yield return null;
    }

    IEnumerator GenericCutscene()
    {
        GameManager.instance.SuspendGame();
        for (int i = 0; i < dialogComponents.Count; i++)
        {
            string[] dialogPieces = genericComponents[i].Split(new string[] { " : " }, System.StringSplitOptions.None);
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

            yield return new WaitForSeconds(0.1f);
            while (!UIController.instance.dialog.dialogCompleted)
            {
                yield return new WaitForSeconds(0.1f);
            }
            while (!Controls.confirmInputHeld())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        UIController.instance.dialog.closeDialog();
        GameManager.instance.UnsuspendGame();

        yield return null;
    }
}
