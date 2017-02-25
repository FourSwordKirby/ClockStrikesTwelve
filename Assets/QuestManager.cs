using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    //Notepad that people write on between days
    public string notepadText;

    //These are all of the various flags that can be toggled between loops
    //Used for the start of the day event
    public bool introCompleted;

    //Used for the shower guy's quest
    public int toiletsFlushed;
    public bool maintencenceRequestCalled;

    //Used for the mother child quest
    public int changeInMachine;

    public static QuestManager instance;
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

    public void reinitializeQuests()
    {
        introCompleted = false;
        toiletsFlushed = 0;
        changeInMachine = 0;
    }
}
