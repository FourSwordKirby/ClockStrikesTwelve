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

    private TextAsset currentDialog;

    void Start()
    {
        InitializeRenderer();
    }

    public override void Interact()
    {
        base.Interact();
        SetCurrentDialog();
        StartCoroutine(Talk());
    }

    private void SetCurrentDialog()
    {
        //TODO: this is just placeholder
        if (true)
        {
            currentDialog = BoothIntro1;
        }
    }

    IEnumerator Talk()
    {
        yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
    }
}
