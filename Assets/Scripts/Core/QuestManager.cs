using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    //Notepad that people write on between days
    public string notepadText;

    //These are all of the various flags that can be toggled between loops
    //Used for the introduction on the very first day
    public bool talkedToManager;
    public bool talkedToManagerPart2;
    public bool leftLobby;

    //Used for the start of the day event
    public bool introCompleted;

    //Used for the shower guy's quest
    public int toiletsFlushed { get { return toiletRoomsFlushed.Count; } }
    public List<string> toiletRoomsFlushed = new List<string>();
    public bool maintenanceRequestCalled;
    public bool maintenanceCompleted; //Once this is true, the shower guy's room is permanently open
    public bool showerCompleted;

    //Used for the writer's quest
    public bool drinkTaken;
    public bool snoopMet;
    public bool writerTalkedTo;
    public bool tvOff;
    public int ideaCount { get { return ideas.Count; } }
    public List<string> ideas;
    public bool writerLocked;
    public bool writerCompleted;

    //Used for the mother child quest
    public bool changeLockedOut;
    public bool sentHome;
    public int changeInMachine;

    public bool childTalked;
    public bool momTalked;

    public bool momChildCompleted;

    //Used to control locks
    public bool lockClear0;
    public bool lockClear1;
    public bool lockClear2;

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

    private void Update()
    {
        if (GameManager.instance.dayPhase >= 2)
        {
            changeLockedOut = changeInMachine > 0;
        }
    }

    public void RestartQuests()
    {
        toiletRoomsFlushed = new List<string>();
        if (maintenanceRequestCalled)
            maintenanceCompleted = true;
        
        drinkTaken = false;
        ideas = new List<string>();
        writerTalkedTo = false;
        writerLocked = false;

        if (!momChildCompleted)
            changeInMachine = 5;
        childTalked = false;
        momTalked = false;
        QuestManager.instance.sentHome = false;
    }

    public bool IsRoomFlushed(string roomName)
    {
        return toiletRoomsFlushed.Contains(roomName);
    }

    public void FlushToilet(string roomName)
    {
        if (!IsRoomFlushed(roomName))
            toiletRoomsFlushed.Add(roomName);
    }
}
