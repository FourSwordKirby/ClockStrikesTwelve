using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArcadeMachine : Interactable {
    public Sprite On;
    public Sprite Off;
    private SpriteRenderer srender;
    public TextAsset arcadeText;
    private List<string> dialogComponents;

    private void Awake()
    {
        srender = GetComponent<SpriteRenderer>();
        dialogComponents = new List<string>(arcadeText.text.Split('\n'));
        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
    }

    public override void Interact()
    {
        StartCoroutine(arcade());
    }

    IEnumerator arcade()
    {
        srender.sprite = On;
        yield return Dialog.DisplayDialog(dialogComponents);
        srender.sprite = Off;
    }
}
