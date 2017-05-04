using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotepadUI : MonoBehaviour {
    public InputField notepadText;

    public List<string> hints = new List<string>() { "The night life in the arcade is a 5 star experience~",
                                                    "The night life in the arcade is a 5 star experience~",
                                                     "I hear parrots are good pets for writers",
                                                     "I hear parrots are good pets for writers",
                                                     "I hear parrots are good pets for writers",
                                                     "Some words are special. You should pause to remember them",
                                                     "5125 ways to avoid paying your taxes",
                                                     "This game is terrible. There's too much time/10",
                                                     "You can practically see the cut content",
                                                     "Just one more game.Just one more game.Just one more game.Just onem ore game. Just one more game. Just one more game.",
                                                      };

    public void openNotepad()
    {
        this.gameObject.SetActive(true);
        notepadText.text = QuestManager.instance.notepadText;

        if (GameManager.instance.currentTime <= 1.0f)
            notepadText.text += "\n" + hints[Random.Range(0, hints.Count)];

    }

    public void closeNotepad()
    {
        this.gameObject.SetActive(false);
        QuestManager.instance.notepadText = notepadText.text;
    }
}
