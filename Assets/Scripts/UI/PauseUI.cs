using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour {

    public InventoryUI inventoryUI;

	// Update is called once per frame
	void Update () {
        //Alter this later
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.TogglePause();
        }
	}
}
