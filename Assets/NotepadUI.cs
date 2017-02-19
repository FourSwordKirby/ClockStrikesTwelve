using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotepadUI : MonoBehaviour {
    public Text notepadText;

    public void openNotepad()
    {
        this.gameObject.SetActive(true);
        notepadText.text = GameManager.instance.notepadText;
    }

    public void closeNotepad()
    {
        this.gameObject.SetActive(false);
        GameManager.instance.notepadText = notepadText.text;
    }
}
