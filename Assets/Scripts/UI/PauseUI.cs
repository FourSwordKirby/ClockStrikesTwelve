using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour {

    public InventoryUI inventoryUI;
    private bool pauseWasPressed = true;

	// Update is called once per frame
	void Update () {
        //Alter this later

        bool pauseNowPressed = Controls.pauseInputDown();
        if (pauseNowPressed && !pauseWasPressed)
        {
            GameManager.instance.TogglePauseMenu();
        }
        pauseWasPressed = pauseNowPressed;
	}
}
