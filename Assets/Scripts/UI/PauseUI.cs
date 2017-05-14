using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {

    public InventoryUI inventoryUI;
    public GameObject lastSelect;
    public EventSystem eventSystem;


	// Update is called once per frame
	void Update () {

        if (Controls.pauseInputDown())
        {
            if (this.gameObject.activeSelf) GameManager.instance.TogglePauseMenu();
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (Controls.getInputDirection() != Parameters.InputDirection.None)
            {
                eventSystem.SetSelectedGameObject(null);
                eventSystem.SetSelectedGameObject(lastSelect, new BaseEventData(eventSystem));
            }
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(lastSelect, new BaseEventData(eventSystem));
    }

    private void OnDisable()
    {
        lastSelect = eventSystem.currentSelectedGameObject;
    }

    public void quitButton()
    {
        Application.Quit();
    }

    public void mainMenuButton()
    {
        Destroy(GameManager.instance.gameObject);
        Destroy(QuestManager.instance.gameObject);
        Destroy(CameraControls.instance.gameObject);
        Destroy(Player.instance.gameObject);
        Destroy(UIController.instance.gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
