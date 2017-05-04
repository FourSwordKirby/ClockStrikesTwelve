using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotepadUI : MonoBehaviour {
    public Text notepadText;

    public List<string> hints = new List<string>() { "The night life in the arcade is a 5 star experience",
                                                     "I hear parrots are good pets for writers",
                                                     "Watashi Kawaaaaiiii",
                                                     "etc. etc."};

    public void openNotepad()
    {
        this.gameObject.SetActive(true);
        notepadText.text = QuestManager.instance.notepadText;

        if (GameManager.instance.currentTime <= 0.2f)
            notepadText.text += hints[Random.Range(0, hints.Count)];

    }

    public void closeNotepad()
    {
        this.gameObject.SetActive(false);
        QuestManager.instance.notepadText = notepadText.text;
    }
}
