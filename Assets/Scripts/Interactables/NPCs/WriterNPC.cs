using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriterNPC : NPC
{
    public TextAsset writerIntro;
    public TextAsset writerIntroRepeat;

    public TextAsset writerPostQuestPrompt;
    public TextAsset writerPrompt1;

    public TextAsset writerProtagCorrect1;
    public TextAsset writerWrong1;

    public TextAsset writerAntagCorrect2;
    public TextAsset writerWrong2;

    public TextAsset writerThemeCorrect3;
    public TextAsset writerWrong3;

    public TextAsset writerSuccess;


    private TextAsset currentDialog;

    public override void Interact()
    {
        base.Interact();
        SetCurrentDialog();

        if (currentDialog != writerPrompt1)
            StartCoroutine(Talk());
        else
            StartCoroutine(Prompt(Dialog.CreateDialogComponents(writerPrompt1.text), 
                                    "hi",
                                    Dialog.CreateDialogComponents(writerProtagCorrect1.text),
                                    Dialog.CreateDialogComponents(writerWrong1.text)));
    }

    private void SetCurrentDialog()
    {
        if (QuestManager.instance.tvOff)
        {
            if (QuestManager.instance.ideaCount == 0)
                currentDialog = writerPrompt1;
        }
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }

    IEnumerator Prompt(List<string> promptComponents, string correctAnswer, List<string> correctComponents, List<string> incorrectComponents)
    {
        yield return Dialog.DisplayPrompt(promptComponents, correctAnswer, correctComponents, incorrectComponents);
    }
}