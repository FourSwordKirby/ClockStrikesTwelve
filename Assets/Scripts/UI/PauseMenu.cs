using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour, ISelectHandler, IDeselectHandler {
    public GameObject correspondingScreen;
    public GameObject[] otherScreens;

	public void OnSelect(BaseEventData eventData)
    {
        correspondingScreen.SetActive(true);
        foreach (GameObject item in otherScreens)
        {
            item.SetActive(false);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {

    }
}
