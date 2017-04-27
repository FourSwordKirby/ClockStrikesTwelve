using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BGMController bgm;

    public bool paused;
    public float startingTime;
    public float currentTime;
    public float timeLimit;

    private bool loadingScene = false;

    //Not being used
    //For now we're hacking and keeping tack of the set of stored items in the gameManager
    public List<InventoryItem> chestStoredItems;
    public bool charmTriggered;


    public List<AudioClip> menuSfx;
    public List<AudioClip> environmentSfx;
    public List<AudioClip> itemSfx;

    public DayStartEvent dayStart;
    public DayAfternoonEvent dayAfternoon;
    public DayEveningEvent dayEvening;
    public DayEndEvent dayEnd;

    public int dayPhase; //Used to differentiate between morning, afternoon, and night (different cutscene for phase transitions)
                         // 0 = morning, 1 = afternoon, 2 = evening, 3 = dead of night

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
        //Change to the afternoon
        if (currentTime > timeLimit/3)
            //StartCoroutine(dayAfternoon.DayEnd());
        //Change to evening
        if (currentTime > (2*timeLimit)/3)
            StartCoroutine(dayEvening.DayEnd());
        //Change to dead of night
        if (currentTime > timeLimit)
            StartCoroutine(dayEnd.DayEnd());

        dayPhase = (int)(3 * currentTime) / 3;
    }

    /// <summary>
    /// Used when you're in a cutscene or dialog is happening, basically, stop the player + time and the movements
    /// </summary>
    public void SuspendGame()
    {
        paused = true;
        Player player = Player.instance;
        player.FreezePlayer();
        //Cargo cult programming incoming
        //This is some pretty contrived code you got going on here
        //SuspendState doesn't work :\
        StateMachine<Player> ActionFsm = new StateMachine<Player>(player);
        State<Player> startState = new MovementState(player, player.ActionFsm);
        ActionFsm.InitialState(startState);
        startState.Exit();
        //add other stuff as needed.
        //Probably need a player function to initialize it or something
    }

    /// <summary>
    /// Used to restore normal player controls/the flow of time etc.
    /// </summary>
    public void UnsuspendGame()
    {
        paused = false;
        Player.instance.UnfreezePlayer();
    }

    public void TogglePauseMenu()
    {
        if(!paused)
        {
            GameManager.instance.playSound(SoundType.Menu, "MenuOpen");
            Player.instance.enabled = false;
            UIController.instance.pauseScreen.gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.playSound(SoundType.Menu, "MenuOpen");
            Player.instance.enabled = true;
            UIController.instance.pauseScreen.gameObject.SetActive(false);
        }
        paused = !paused;
    }

    public IEnumerator ResetDay()
    {
        //Setting the flags for the things in the chest
        if (GameManager.instance.chestStoredItems.Find(x => x.designation == ItemDesignation.WeatherCharm) != null)
            GameManager.instance.charmTriggered = true;

        QuestManager.instance.RestartQuests();
        chestStoredItems = new List<InventoryItem>();
        //Do a bunch of other state resets

        //Reset the time
        currentTime = startingTime;


        StartCoroutine(UIController.instance.screenfader.FadeOut());
        StartCoroutine(bgm.FadeTowards(0.0f));
        while (UIController.instance.screenfader.fading)
        {
            yield return new WaitForSeconds(0.1f);
        }

        playSound(SoundType.Environment, "Clock");
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(bgm.FadeTowards(1.0f));
        SceneManager.LoadScene("PlayerBedroom");
        Player.instance.transform.position = new Vector2(0.95f, 0.75f);
        while (UIController.instance.screenfader.fading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        paused = false;
        yield return null;
    }

    public void playSound(SoundType soundType, string soundName, bool startingSound = false)
    {
        Vector3 position;
        AudioClip sound = null;
        if (!startingSound)
            position = CameraControls.instance.transform.position;
        else
            position = Vector3.back * 10.0f;
        if (soundType == SoundType.Menu)
            sound = menuSfx.Find(x => x.name == soundName);
        else if (soundType == SoundType.Environment)
            sound = environmentSfx.Find(x => x.name == soundName);
        else if (soundType == SoundType.Item)
            sound = itemSfx.Find(x => x.name == soundName);

        AudioSource.PlayClipAtPoint(sound, position);
    }

    public void startLoop(string soundName)
    {
        transform.Find("LoopingSounds").Find(soundName).gameObject.SetActive(true);
    }

    public void stopLoop(string soundName)
    {
        transform.Find("LoopingSounds").Find(soundName).gameObject.SetActive(false);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void StartSceneTransition(string sceneName)
    {
        StartCoroutine(transitionRooms(sceneName));
    }

    public IEnumerator transitionRooms(string sceneName)
    {
        //Pretransition things
        Player.instance.FreezePlayer();

        string oldSceneName = GetSceneName();
        GameManager.instance.playSound(SoundType.Environment, "RoomExit");
        StartCoroutine(UIController.instance.screenfader.FadeOut());
        while (UIController.instance.screenfader.fading)
        {
            yield return new WaitForSeconds(0.1f);
        }

        loadingScene = true;
        SceneManager.LoadScene(sceneName);
        yield return null;
        //try to move to the position of the door to this room
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if (spawnPoint.GetComponent<SpawnPoint> ().scene == oldSceneName)
            {
                Player.instance.transform.position = spawnPoint.transform.position;
                break;
            }
        }
        loadingScene = false;

        //Posttransition things
        while (UIController.instance.screenfader.fading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Player.instance.UnfreezePlayer();
        print("hi");
    }
    
    public bool canSwitchRooms() {
        return !loadingScene;
    }
}
