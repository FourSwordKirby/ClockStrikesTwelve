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

        if (currentDialog == writerIntro || QuestManager.instance.writerLocked)
            StartCoroutine(Talk());
        else
        {
                StartCoroutine(Prompt(Dialog.CreateDialogComponents(writerPrompt1.text),
                                        "dog",
                                        new List<string>() { "That idea is pretty good", "Let me write that down" },
                                        Dialog.CreateDialogComponents(writerWrong1.text)));
                QuestManager.instance.writerLocked = true;
        }
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
            else if (QuestManager.instance.ideaCount == 2)
            {
                currentDialog = writerWrong3;
                Player.instance.conversations.Add(new ConversationItem("confidence", "A good trait to have"));
            }
            else if (QuestManager.instance.ideaCount == 1)
            {
                currentDialog = writerWrong2;
                Player.instance.conversations.Add(new ConversationItem("politician", "A good idea for a villan"));
            }

            else if (QuestManager.instance.ideaCount == 0)
            {
                currentDialog = writerWrong1;
                Player.instance.conversations.Add(new ConversationItem("dog", "A good idea for a protagonist"));
            }
        }
    }

    void AdvanceStoryProgress()
    {
        if (QuestManager.instance.ideaCount == 0)
            StartCoroutine(Prompt(Dialog.CreateDialogComponents(writerProtagCorrect1.text),
                                    "politician",
                                    new List<string>() { "Another good idea!", "You're pretty good at making stories" },
                                    Dialog.CreateDialogComponents(writerWrong2.text)));
        if (QuestManager.instance.ideaCount == 1)
            StartCoroutine(Prompt(Dialog.CreateDialogComponents(writerAntagCorrect2.text),
                                    "confidence",
                                    new List<string>() { "!!!", "What a good theme!", "With this I can finally get to writing" },
                                    Dialog.CreateDialogComponents(writerWrong3.text)));
        if (QuestManager.instance.ideaCount == 2)
        {
            currentDialog = writerThemeCorrect3;
            Player.instance.conversations.Add(new ConversationItem("rejection", "The word sends shivers down your spine"));
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