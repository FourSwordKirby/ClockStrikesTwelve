using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour{

    private int maxHealth;
    private float baseKnockdownThreshold;

    public int health { get; private set; }
    public float movementSpeed { get; private set; }
    public float rollSpeed { get; private set; }
    public Parameters.InputDirection direction { get; private set; }
    public float knockdownThreshold { get; private set; }

    public const int DEFAULT_MAX_HEALTH = 12;
    public const float DEFAULT_SPEED = 2.0f;
    public const float DEFAULT_ROLL_SPEED = 6.0f;
    public const float DEFAULT_KNOCKDOWN_THRESHOLD = 1.0f;

    public StateMachine<Enemy> ActionFsm { get; private set; }

	// Use this for initialization
	void Start () {
        maxHealth = DEFAULT_MAX_HEALTH;
        health = maxHealth;
        baseKnockdownThreshold = DEFAULT_KNOCKDOWN_THRESHOLD;
        knockdownThreshold = baseKnockdownThreshold;

        this.movementSpeed = DEFAULT_SPEED;
        this.rollSpeed = DEFAULT_ROLL_SPEED;

        ActionFsm = new StateMachine<Enemy>(this);
        State<Enemy> startState = new TestState(this, ActionFsm);
        ActionFsm.InitialState(startState);
	}
	
	// Update is called once per frame
	void Update () {
        //Controller support works sort of yaaaaay
        //Debug.Log(GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, GamepadInput.GamePad.Index.One).x);
        ActionFsm.Execute();
	}

    void FixedUpdate()
    {
        ActionFsm.FixedExecute();
    }
}
