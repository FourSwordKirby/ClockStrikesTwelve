using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VendingMachine : Interactable {

    public InventoryItem inventoryItem;
    public TextAsset VendingPrompt;
    public TextAsset VendingCorrectText5;
    public TextAsset VendingCorrectText4;
    public TextAsset VendingCorrectText3;
    public TextAsset VendingCorrectText2;
    public TextAsset VendingCorrectText1;
    public TextAsset VendingCorrectText0;
    public TextAsset VendingWrongText;

    private TextAsset currentDialog;

    private List<string> dialogComponents;

    public override void Interact()
    {
        if (QuestManager.instance.changeInMachine == 5)
            StartCoroutine(Prompt(Dialog.CreateDialogComponents(VendingPrompt.text),
                                    "5125",
                                    Dialog.CreateDialogComponents(VendingCorrectText5.text),
                                    Dialog.CreateDialogComponents(VendingWrongText.text)));
        else
        {
            SetCurrentDialog();
            StartCoroutine(Talk());
        }
    }

    private void SetCurrentDialog()
    {
        if (QuestManager.instance.changeInMachine == 4)
        {
            currentDialog = VendingCorrectText4;
            decreaseCoins();
        }
        else if (QuestManager.instance.changeInMachine == 3)
        {
            currentDialog = VendingCorrectText3;
            decreaseCoins();
        }
        else if (QuestManager.instance.changeInMachine == 2)
        {
            currentDialog = VendingCorrectText2;
            decreaseCoins();
        }
        else if (QuestManager.instance.changeInMachine == 1)
        {
            currentDialog = VendingCorrectText1;
            decreaseCoins();
        }
        else if (QuestManager.instance.changeInMachine == 0)
        {
            currentDialog = VendingCorrectText0;
        }
    }

    void decreaseCoins()
    {
        QuestManager.instance.changeInMachine--;
        GameManager.instance.playSound(SoundType.Item, "ItemGet");
        Player.instance.items.Add(inventoryItem);
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
    IEnumerator Prompt(List<string> promptComponents, string correctAnswer, List<string> correctComponents, List<string> incorrectComponents)
    {
        yield return Dialog.DisplayPrompt(promptComponents, correctAnswer, correctComponents, incorrectComponents, decreaseCoins);
    }
}
