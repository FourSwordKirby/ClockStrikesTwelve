using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNPC : NPC
{
    public TextAsset LobbyResponseAntiSocial;
    public TextAsset LobbyHintShower;
    public TextAsset LobbyHintWriter;
    public TextAsset LobbyHintMom;
    public TextAsset BoothIntro1;
    public TextAsset BoothIntro2;

    public RuntimeAnimatorController AnimatorUp;
    public RuntimeAnimatorController AnimatorDown;
    public RuntimeAnimatorController AnimatorLeft;
    public RuntimeAnimatorController AnimatorRight;

    private TextAsset currentDialog;
    
    public override void Interact()
    {
        base.Interact();
        SetFacingAnim();
        SetCurrentDialog();
        StartCoroutine(Talk());
    }

    private void SetFacingAnim()
    {
        Parameters.InputDirection dir = PositionsToDirection(Player.instance.transform.position);
        if (dir == Parameters.InputDirection.N)
            SetAnimator(AnimatorUp);
        if (dir == Parameters.InputDirection.S)
            SetAnimator(AnimatorDown);
        if (dir == Parameters.InputDirection.W)
            SetAnimator(AnimatorLeft);
        if (dir == Parameters.InputDirection.E)
            SetAnimator(AnimatorRight);
    }

    private void SetAnimator(RuntimeAnimatorController source)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = source;
    }

    private void SetCurrentDialog()
    {
        if (!QuestManager.instance.talkedToManager)
        {
            currentDialog = BoothIntro1;
            QuestManager.instance.talkedToManager = true;
        }
        else if (!QuestManager.instance.talkedToManagerPart2)
        {
            currentDialog = BoothIntro2;
        }
        else
        {
            List<TextAsset> potentialDialogs = new List<TextAsset>();
            potentialDialogs.Add(LobbyResponseAntiSocial);
            if(QuestManager.instance.showerCompleted)
                potentialDialogs.Add(LobbyHintShower);
            else if (QuestManager.instance.writerCompleted)
                potentialDialogs.Add(LobbyHintWriter);
            else if (QuestManager.instance.momChildCompleted)
                potentialDialogs.Add(LobbyHintMom);

            currentDialog = potentialDialogs[Random.Range(0, potentialDialogs.Count)];
        }
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
