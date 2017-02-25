using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Toilet : Interactable
{
    public override void Interact()
    {
        print("floooooosh");
        MarkRoomFlushed();
    }

    private void MarkRoomFlushed()
    {
        string currentRoom = GameManager.instance.GetSceneName();
        if (!QuestManager.instance.toiletRoomsFlushed.Contains(currentRoom))
        {
            QuestManager.instance.toiletRoomsFlushed.Add(currentRoom);
        }
    }
}
