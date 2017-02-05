using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;

    public bool paused;
    public float startingTime;
    public float currentTime;
    public float timeLimit;

    //It is important to keep track of what items are stored where. 
    //For now we're hacking and keeping tack of the set of stored items in the gameManager
    public List<InventoryItem> chestStoredItems;

    //These are all of the various flags that can be toggled between loops
    public bool introCompleted;
    public bool charmTriggered;



    public List<AudioClip> sfx;

    public static GameManager instance;
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

    void Update()
    {
        if(!paused)
            currentTime += Time.deltaTime;
    }

    public void TogglePause()
    {
        if(!paused)
        {
            player.enabled = false;
            UIController.instance.pauseScreen.gameObject.SetActive(true);
        }
        else
        {
            player.enabled = true;
            UIController.instance.pauseScreen.gameObject.SetActive(false);
        }
        paused = !paused;
    }

    public void PlayerDeath()
    {
        CameraControls.instance.Shake();
    }

    //Returns the closest targetable object within the player's targeting range
    public GameObject getTarget()
    {
        Vector2 startingPosition = player.transform.position;
        //Mobile[] potentialTargets = Object.FindObjectsOfType<Mobile>();

        //Mobile closestTarget = null;
        //foreach (Mobile target in potentialTargets)
        //{
        //    if(target != Player)
        //    {
        //        float distance = Vector2.Distance(Player.transform.position, target.transform.position);
        //        if (distance < Player.targetingRange)
        //            closestTarget = target;
        //    }
        //}

        throw new UnityException("Method not implemented");
        return null;
    }

    public IEnumerator ResetDay()
    {
        introCompleted = false;
        chestStoredItems = new List<InventoryItem>();
        //Do a bunch of other state resets

        //Reset the time
        currentTime = startingTime;


        StartCoroutine(UIController.instance.screenfader.FadeOut());

        while(UIController.instance.screenfader.fading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene(0);
        Debug.Log("Please actually reset the day");
        yield return null;
    }

    public void playSound(string soundName, bool startingSound = false)
    {
    }
}
