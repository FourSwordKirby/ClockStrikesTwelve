using System.Collections;
using UnityEngine;

public class ShowerToilet : Interactable
{
    public TextAsset FlushResponse;
    public TextAsset AlreadyFlushedResponse;
    
    public Sprite unflushedSprite;
    public Sprite flushedSprite;

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
        QuestManager.instance.showerCompleted = true;
        StartCoroutine(FlushCutscene());
        //Add an item to the list of the player's known words

        flushed = true;
        UpdateSprite();
    }

    private void MarkRoomFlushed()
    {
        string currentRoom = GameManager.instance.GetSceneName();
        QuestManager.instance.FlushToilet(currentRoom);
    }

    IEnumerator FlushCutscene()
    {
        string text = flushed ? AlreadyFlushedResponse.text : FlushResponse.text;
        MarkRoomFlushed();
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(text));
    }
}
