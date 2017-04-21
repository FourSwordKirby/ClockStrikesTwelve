using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour {
    private EventSystem eventSystem;
    public string gameScene;
    public GameObject lastselect;
    private GameObject leftoff;
    public GameObject back0;
    public GameObject back1;
    OverlayManager overlay;
    public GameObject menu;
    public GameObject credits;

    public void focusBack(int idx)
    {
        switch (idx)
        {
            case 0:
                lastselect = back0;
                break;
            case 1:
                lastselect = back1;
                break;
            default:
                lastselect = back0;
                break;
        }
    }
            

    private void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        overlay = GameObject.Find("OverlayManager").GetComponent<OverlayManager>();
        //setButtonFocus(lastselect);
        //StartCoroutine("lolwhy");
    }

    IEnumerator lolwhy()
    {
        yield return new WaitForSeconds(0.01f);
        setButtonFocus(lastselect);
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)
                || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                setButtonFocus(lastselect);
            }
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitButtonCall();
        }
    }

    public void setButtonFocus(GameObject o)
    {
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(o, new BaseEventData(eventSystem));
    }

    public void loadGame(string sc)
    {
        gameScene = sc;
        //chnage Scene
        SceneManager.LoadScene(sc);
    }

    public void buttonCall(string button)
    {
        StartCoroutine(transition(button));
    }

    IEnumerator transition(string button)
    {
        yield return StartCoroutine(overlay.fadeIn());
        eventSystem.SetSelectedGameObject(null);
        switch (button){
            case "Credits":
                menu.SetActive(false);
                credits.SetActive(true);
                focusBack(1);
                break;
            case "CreditsBack":
                credits.SetActive(false);
                menu.SetActive(true);
                focusBack(0);
                break;
            default:
                break;
        }
        StartCoroutine(overlay.fadeOut());
    }

    public void quitButtonCall()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else 
        Application.Quit();
#endif

    }
}
