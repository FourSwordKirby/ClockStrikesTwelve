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

        if (!QuestManager.instance.tvOff|| QuestManager.instance.writerLocked)
            StartCoroutine(Talk());
        else
        {
            if(Player.instance.conversations.Find(x => x.itemName == "dog") == null)
                Player.instance.conversations.Add(new ConversationItem("dog", "A good idea for a protagonist"));
            StartCoroutine(Prompt(Dialog.CreateDialogComponents(writerPrompt1.text),
                                    "dog",
                                    new List<string>() { "I think..." },
                                    Dialog.CreateDialogComponents(writerWrong1.text)));
            QuestManager.instance.writerLocked = true;
        }
    }

    private void SetCurrentDialog()
    {
        if (!QuestManager.instance.tvOff)
        {
            if (!QuestManager.instance.writerTalkedTo)
            {
                currentDialog = writerIntro;
                QuestManager.instance.writerTalkedTo = true;
            }
            else
                currentDialog = writerIntroRepeat;
        }
        else
        {
            if (QuestManager.instance.ideaCount == 0)
            {
                if (QuestManager.instance.writerCompleted)
                    currentDialog = writerPostQuestPrompt;
                else
                {
                    currentDialog = writerWrong1;
                }
            }
            else if (QuestManager.instance.ideaCount == 1)
            {
                currentDialog = writerWrong2;
                Player.instance.conversations.Add(new ConversationItem("politician", "A good idea for a villan"));
            }
            else if (QuestManager.instance.ideaCount == 2)
            {
                currentDialog = writerWrong3;
                Player.instance.conversations.Add(new ConversationItem("confidence", "A good trait to have"));
            }
            else if (QuestManager.instance.ideaCount == 3)
                currentDialog = writerSuccess;
        }
    }

    void AdvanceStoryProgress()
    {
        print("Here");
        if (QuestManager.instance.ideaCount == 0)
        {
            if (Player.instance.conversations.Find(x => x.itemName == "politician") == null)
                Player.instance.conversations.Add(new ConversationItem("politician", "A good idea for a villan"));
            StartCoroutine(Prompt(Dialog.CreateDialogComponents(writerProtagCorrect1.text),
                                        "politician",
                                        new List<string>() { "Well... " },
                                        Dialog.CreateDialogComponents(writerWrong2.text)));
        }
        if (QuestManager.instance.ideaCount == 1)
        {
            if (Player.instance.conversations.Find(x => x.itemName == "confidence") == null)
                Player.instance.conversations.Add(new ConversationItem("confidence", "A good trait to have"));

            StartCoroutine(Prompt(Dialog.CreateDialogComponents(writerAntagCorrect2.text),
                                    "confidence",
                                    new List<string>() { "!!!" },
                                    Dialog.CreateDialogComponents(writerWrong3.text)));
        }
        if (QuestManager.instance.ideaCount == 2)
        {
            if (Player.instance.conversations.Find(x => x.itemName == "rejection") == null)
                Player.instance.conversations.Add(new ConversationItem("rejection", "The word sends shivers down your spine"));
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