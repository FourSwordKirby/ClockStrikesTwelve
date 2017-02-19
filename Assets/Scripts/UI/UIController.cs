using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public DialogUI dialog;
    public ClockUI clock;
    public ScreenFader screenfader;
    public PauseUI pauseScreen;
    public TransferUI transferScreen;
    public ChoiceUI choiceScreen;

    public NotepadUI notepadPrompt;
    public PasswordUI passwordPrompt;

    public static UIController instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
