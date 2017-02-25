using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Brochure : Interactable
{
    //public InventoryItem inventoryItem;
    public TextAsset Instructions;
    private List<string> dialogComponents;
    bool isBloody = false;
    private SpriteRenderer spriteRenderer;
    public Sprite originalSprite;
    public Sprite bloodySprite;

    void Awake()
    {
        dialogComponents = new List<string>(Instructions.text.Split('\n'));
        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        StartCoroutine(CharmGet());
    }

    IEnumerator CharmGet()
    {
        GameManager.instance.SuspendGame();
        for (int i = 0; i < dialogComponents.Count; i++)
        {
            string[] dialogPieces = dialogComponents[i].Split(new string[] { " : " }, System.StringSplitOptions.None);
            string speaker = "";
            string dialog = "";
            if (dialogPieces[0].EndsWith(".wav")){
                GameManager.instance.playSound(SoundType.Item, dialogPieces[0].Substring(0, dialogPieces[0].Length - 4));
            }
            else{
                if (dialogPieces.Count() > 1)
                {
                    speaker = dialogPieces[0];
                    dialog = dialogPieces[1];
                }
                else
                {
                    dialog = dialogPieces[0];
                }
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
        }
        UIController.instance.dialog.closeDialog();
        GameManager.instance.UnsuspendGame();

        if (!(isBloody)){
            spriteRenderer.sprite = bloodySprite;
            //Sprite bloodySprite = Resources.Load<Sprite>("Assets/Sprites/Objects/BloodyBrochure");
            ////Sprite originalSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            ////Sprite bloodyBrochure = Sprite.Create(originalSprite.texture, originalSprite.rect, originalSprite.pivot);
            //gameObject.GetComponent<SpriteRenderer>().sprite = bloodySprite;
            isBloody = true;
        }


        //We picked up the object, do stuff to it
        //Destroy(this.gameObject);
        //GameManager.instance.playSound(SoundType.Item, "ItemGet");

        //Player.instance.items.Add(inventoryItem);
        Player.instance.currentInteractable = null;
        StartCoroutine(Player.instance.HideSymbol());
        yield return null;
    }
}
