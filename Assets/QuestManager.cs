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
    public int toiletsFlushed { get { return toiletRoomsFlushed.Count; } }
    public List<string> toiletRoomsFlushed = new List<string>();
    public bool maintenanceRequestCalled;
    public bool maintenancePosted;
    public bool maintenanceCompleted;


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

    public void RestartQuests()
    {
        introCompleted = false;
        toiletRoomsFlushed = new List<string>();
        changeInMachine = 0;
        maintenanceRequestCalled = false;
        maintenancePosted = false;
    }

    public void FlushToilet(string roomName)
    {
        if(!toiletRoomsFlushed.Contains(roomName))
            toiletRoomsFlushed.Add(roomName);
    }

    public void RequestMaintenance()
    {
        maintenanceRequestCalled = true;
    }

    public void PostMaintenance()
    {
        maintenancePosted = true;
    }

    public void CompleteMaintenanace()
    {
        maintenanceCompleted = true;
    }
}
