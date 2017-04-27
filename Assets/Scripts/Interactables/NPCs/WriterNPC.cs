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

        if (currentDialog= writerPrompt1)
            StartCoroutine(Talk());
        else
            StartCoroutine(Prompt(Dialog.CreateDialogComponents(writerPrompt1.text), 
                                    "dog",
                                    new List<string>(),
                                    Dialog.CreateDialogComponents(writerWrong1.text)));
    }

    private void SetCurrentDialog()
    {
        if (QuestManager.instance.tvOff)
        {
            if (QuestManager.instance.ideaCount == 0)
            {
                if (QuestManager.instance.writerCompleted)
                    currentDialog = writerPostQuestPrompt;
                else
                    currentDialog = writerPrompt1;
            }
            if (QuestManager.instance.ideaCount == 3)
                currentDialog = writerSuccess;
        }
    }

    void AdvanceStoryProgress()
    {
        if (QuestManager.instance.ideaCount == 0)
            StartCoroutine(Prompt(Dialog.CreateDialogComponents(writerProtagCorrect1.text),
                                    "politician",
                                    new List<string>(),
                                    Dialog.CreateDialogComponents(writerWrong2.text)));
        if (QuestManager.instance.ideaCount == 1)
            StartCoroutine(Prompt(Dialog.CreateDialogComponents(writerAntagCorrect2.text),
                                    "confidence",
                                    new List<string>(),
                                    Dialog.CreateDialogComponents(writerWrong3.text)));
        if (QuestManager.instance.ideaCount == 2)
        {
            currentDialog = writerThemeCorrect3;
            StartCoroutine(Talk());
        }
        QuestManager.instance.ideas.Add("idea");
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }

    IEnumerator Prompt(List<string> promptComponents, string correctAnswer, List<string> correctComponents, List<string> incorrectComponents)
    {
        yield return Dialog.DisplayPrompt(promptComponents, correctAnswer, correctComponents, incorrectComponents, AdvanceStoryProgress);
    }
}