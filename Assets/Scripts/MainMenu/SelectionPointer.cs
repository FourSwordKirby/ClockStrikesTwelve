using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISubmitHandler {
    EventSystem eventSystem;

    // Use this for initialization
    void Awake () {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventSystem.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventSystem.SetSelectedGameObject(null);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        eventSystem.SetSelectedGameObject(null);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        eventSystem.SetSelectedGameObject(null);
    }
}
