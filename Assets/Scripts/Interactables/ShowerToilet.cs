using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ShowerToilet : Interactable
{
    public TextAsset FlushResponse;
    public TextAsset AlreadyFlushedResponse;
    
    public Sprite unflushedSprite;
    public Sprite flushedSprite;

    public ShowerControls showerControls;

    public bool flushed;

    void Start()
    {
        flushed = QuestManager.instance.IsRoomFlushed(GameManager.instance.GetSceneName());
        UpdateSprite();
    }

    private void Update()
    {
        this.flushed = QuestManager.instance.IsRoomFlushed(GameManager.instance.GetSceneName());
    }

    private void UpdateSprite()
    {
        if (!flushed)
            GetComponent<SpriteRenderer>().sprite = unflushedSprite;
        else
            GetComponent<SpriteRenderer>().sprite = flushedSprite;
    }

    public override void Interact()
    {
        if(!QuestManager.instance.showerCompleted)
            Player.instance.conversations.Add(new ConversationItem("mac", "Supposedly your name"));

        QuestManager.instance.showerCompleted = true;
        StartCoroutine(FlushCutscene());
        //Add an item to the list of the player's known words

        flushed = true;
        UpdateSprite();
    }

    private void MarkRoomFlushed()
    {
        string currentRoom = GameManager.instance.GetSceneName();
        GameManager.instance.playSound(SoundType.Environment, "toilet");
        QuestManager.instance.FlushToilet(currentRoom);
    }

    IEnumerator FlushCutscene()
    {
        string text = flushed ? AlreadyFlushedResponse.text : FlushResponse.text;
        MarkRoomFlushed();
        StartCoroutine(showerControls.OpenCurtain());

        List<string> dialogComponents = Dialog.CreateDialogComponents(text);

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

        StartCoroutine(showerControls.CloseCurtain());
    }
}
