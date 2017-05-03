using System.Collections;
using UnityEngine;

public class TV : Interactable
{
    public bool isOn;
    public GameObject tv;
    private SpriteRenderer spriteRenderer;
    public Sprite offSprite;
    public Sprite onSprite;
    public GameObject weedMusic;

    public TextAsset tvOnDialog;
    public TextAsset tvOffDialog;

    private TextAsset currentDialog;

    void Awake()
    {
        spriteRenderer = tv.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(QuestManager.instance.tvOff)
        {
            spriteRenderer.sprite = offSprite;
            weedMusic.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = onSprite;
            weedMusic.SetActive(true);
        }
    }

    public override void Interact()
    {
        SetCurrentDialog();
        StartCoroutine(Talk());
    }

    private void SetCurrentDialog()
    {
        if (QuestManager.instance.tvOff)
            currentDialog = tvOffDialog;
        else
            currentDialog = tvOnDialog;
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }

    private void ToggleOn()
    {
        if (isOn)
        {
            spriteRenderer.sprite = offSprite;
            isOn = false;
        }
        else
        {
            spriteRenderer.sprite = onSprite;
            isOn = true;
        }
    }
}
