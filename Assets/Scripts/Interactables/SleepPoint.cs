using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SleepPoint : Interactable {

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
        StartCoroutine(SleepTime());
    }

    IEnumerator SleepTime()
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
            //Replace this with things in the control set
            while (!Controls.confirmInputHeld())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        UIController.instance.choiceScreen.OpenChoices();
        while(!UIController.instance.choiceScreen.choiceMade)
        {
            yield return new WaitForSeconds(0.1f);
        }

        UIController.instance.choiceScreen.CloseChoices();

        float markedTime = GameManager.instance.currentTime;
        if (UIController.instance.choiceScreen.currentChoice == 0)
        {
            if(GameManager.instance.dayPhase < 1)
            {
                while (GameManager.instance.currentTime < GameManager.instance.timeLimit/3)
                {
                    GameManager.instance.currentTime += 4*Time.deltaTime * ((GameManager.instance.timeLimit / 3) - markedTime);
                    yield return new WaitForSeconds(0.1f);
                }
                GameManager.instance.currentTime = (GameManager.instance.timeLimit / 3) - 0.5f;
            }
            else
            {
                yield return Dialog.DisplayDialog(new List<string>() { "Sleeping that late into the next day is irresponsible" });
            }
        }
        if (UIController.instance.choiceScreen.currentChoice == 1)
        {
            if (GameManager.instance.dayPhase < 2)
            {
                while(GameManager.instance.currentTime < 2*GameManager.instance.timeLimit/3)
                {
                    GameManager.instance.currentTime += 4*Time.deltaTime * (2 * GameManager.instance.timeLimit / 3 - markedTime);
                    yield return new WaitForSeconds(0.1f);
                }
                GameManager.instance.currentTime = (2 * GameManager.instance.timeLimit / 3)-0.5f;
            }
            else
            {
                yield return Dialog.DisplayDialog(new List<string>() { "Sleeping that late into the next day is irresponsible" });
            }
        }

        UIController.instance.dialog.closeDialog();
        GameManager.instance.UnsuspendGame();

        if (UIController.instance.choiceScreen.currentChoice == 2)
        {
            StartCoroutine(GameManager.instance.ResetDay());
        }
        yield return null;
    }
}
