using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
    public int maxHealth;

    public int health { get; private set; }
    public float movementSpeed { get; private set; }
    public Parameters.InputDirection direction { get; set; }

    public float targetingRange { get; private set; } //Determines the max range that the player can target an object
    public GameObject target;

    public List<InventoryItem> items;

    public const int DEFAULT_MAX_HEALTH = 12;
    public const float DEFAULT_SPEED = 1.0f;
    public const float DEFAULT_TARGETING_RANGE = 10.0f;

    public StateMachine<Player> ActionFsm { get; private set; }

    //self references to various components
    //private Collider selfCollider;
    public Animator anim { get; private set; }
    public Rigidbody2D selfBody { get; private set; }
    public CollisionboxManager hitboxManager { get; private set; }
    public List<AudioClip> Sounds { get; private set; }

    public SpriteRenderer emotionSymbol;

    // Interact Button related
    private bool canInteract;
    public Interactable currentInteractable;

    public static Player instance;
    //Used for the initialization of internal, non-object variables
    void Awake()
    {
        //Making the player persist between scenes
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

        maxHealth = DEFAULT_MAX_HEALTH;
        health = maxHealth;

        this.movementSpeed = DEFAULT_SPEED;

        this.target = null;
        this.targetingRange = DEFAULT_TARGETING_RANGE;

        //Initializing components
        anim = this.GetComponent<Animator>();
        selfBody = this.GetComponent<Rigidbody2D>();
        hitboxManager = this.GetComponent<CollisionboxManager>();

        ActionFsm = new StateMachine<Player>(this);
        State<Player> startState = new IdleState(this, this.ActionFsm);
        ActionFsm.InitialState(startState);
    }

    // Use this for initialization of variables that rely on other objects
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        this.ActionFsm.Execute();

        if(this.currentInteractable != null)
        {
            if(Controls.confirmInputDown())
            {
                currentInteractable.Interact();
            }
        }

        if(Controls.pauseInputDown())
        {
            GameManager.instance.TogglePauseMenu();
        }
	}

    void FixedUpdate()
    {
        this.ActionFsm.FixedExecute();
    }

    public IEnumerator ShowSymbol(Symbol interested)
    {
        //Make an animated motion tween or something
        emotionSymbol.enabled = true;
        yield return null;
    }

    public IEnumerator HideSymbol()
    {
        //Make an animated motion tween or something
        emotionSymbol.enabled = false;
        yield return null;
    }
}
