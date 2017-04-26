using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : Interactable {
        public TextAsset SpookyText;

        private TextAsset currentDialog;

        public override void Interact()
        {
            SetCurrentDialog();
            StartCoroutine(Talk());
        }

        private void SetCurrentDialog()
        {
            //TODO: this is just placeholder
            if (true)
            {
                currentDialog = SpookyText;
            }
        }

        IEnumerator Talk()
        {
            yield return Dialog.DisplayDialog(Dialog.CreateDialogComponents(currentDialog.text));
        }
    }
