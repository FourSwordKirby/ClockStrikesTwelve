using System.Collections;
using UnityEngine;

public class Toilet : Interactable
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

    void Update()
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
        MarkRoomFlushed();
        if (QuestManager.instance.toiletsFlushed >= 4)
        {
            QuestManager.instance.maintenanceRequestCalled = true;
        }

        StartCoroutine(FlushCutscene());

        if(!flushed)
            GameManager.instance.playSound(SoundType.Environment, "toilet");

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
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(text));
    }
}
